using LN_WEB.Entities;
using LN_WEB.Models;
using LN_WEB.Models.Paypal_Transaction;
using Newtonsoft.Json;
using OpenAI_API.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace LN_WEB.Controllers
{
    public class DishController : Controller
    {
        DishModel model = new DishModel();



        public async Task<ActionResult> About()
        {

            //id de la autorizacion para obtener el dinero
            string token = Request.QueryString["token"];

            bool status = false;


            using (var client = new HttpClient())
            {


                var userName = "Ac4o9Ls_j3QbJCO1jl8rGDMFDZ6V0mAEG7exPjxYIMcEoFSlS9qqCzW877gRxKLRiakmGdvFMOkCOzsI";
                var passwd = "EJMvVLT303jt8i_Paf9KqkNbVy72gKLqlho3qVj2F27DUJ8R8iCrMA7xm_ZQdEeNbwIYjhqJDmvunhD8";

                client.BaseAddress = new Uri("https://api-m.sandbox.paypal.com");

                var authToken = Encoding.ASCII.GetBytes($"{userName}:{passwd}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authToken));

                var data = new StringContent("{}", Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync($"/v2/checkout/orders/{token}/capture", data);


                status = response.IsSuccessStatusCode;

                ViewData["Status"] = status;
                if (status)
                {
                    var jsonRespuesta = response.Content.ReadAsStringAsync().Result;

                    PaypalTransaction objeto = JsonConvert.DeserializeObject<PaypalTransaction>(jsonRespuesta);

                    ViewData["IdTransaccion"] = objeto.purchase_units[0].payments.captures[0].id;

                    RemoveAllFromCart();


                }

            }


            return View();
        }

        [HttpPost]
        public ActionResult RemoveAllFromCart()
        {
            long userId = long.Parse(Session["IdUsuario"].ToString()); // Obtener el IdUsuario de la sesión

            using (var client = new HttpClient())
            {
                string token = ControllerContext.HttpContext.Session["Token"].ToString();
                string url = ConfigurationManager.AppSettings["urlApi"].ToString() + "api/RemoveAllDishCart?userId=" + userId;

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                HttpResponseMessage resp = client.DeleteAsync(url).Result;

                if (resp.IsSuccessStatusCode)
                {
                    // Redirigir o realizar otras acciones después de la eliminación exitosa
                    return RedirectToAction("Menu", "Dish");
                }

                // Manejar el caso en que la eliminación no sea exitosa
                ViewBag.ErrorMessage = "Error al eliminar elementos del carrito. Por favor, inténtelo de nuevo.";
                return View("NombreDeTuVista");
            }
        }


        [HttpGet]
        public ActionResult Menu()
        {
            UpdateTotals();

            var resp = model.CheckDishes();
            return View(resp);
        }


    

        //[HttpPost]
        //public ActionResult RemoveDish(long q)
        //{
        //    var resp = model.RemoveDish(q);
        //    UpdateTotalsDish();

        //    return Json("OK", JsonRequestBehavior.AllowGet);
        //}

        private void UpdateTotalsDish()
        {
            var CurrentDish = model.CheckDish(long.Parse(Session["IdUsuario"].ToString()));
        
        }






        private void UpdateTotals()
        {
            var CurrentCart = model.CheckCart(long.Parse(Session["IdUsuario"].ToString()));
            Session["Amount"] = CurrentCart.Count();
            Session["SubTotal"] = CurrentCart.Sum(x => x.Price);
            Session["Total"] = CurrentCart.Sum(x => x.Price) + (CurrentCart.Sum(x => x.Price) * 0.13M);
        }

        [HttpGet]
        public ActionResult CheckPayment()
        {
            var date = model.CheckCart(long.Parse(Session["IdUsuario"].ToString()));
            return View(date); 
        }

        [HttpPost]
        public ActionResult ConfirmPaymentCart()
        {
            CartEnt entidad = new CartEnt();
            entidad.IdUsuario = long.Parse(Session["IdUsuario"].ToString());

            model.ConfirmPaymentCart(entidad);
            return RedirectToAction("Menu", "Dish");
        }

        [HttpGet]
        public ActionResult CheckMyDishes()
        {
            var datos = model.CheckMyDishes(long.Parse(Session["IdUsuario"].ToString()));
            return View(datos);
        }


        [HttpGet]
        public ActionResult CheckDishes()
        {
            var datos = model.CheckDishes();
            return View(datos);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddDish(HttpPostedFileBase ImageDish, DishEnt entidad)
        {
            try
            {
                if (ImageDish == null || ImageDish.ContentLength == 0)
                {
                    ModelState.AddModelError("ImageDish", "Debe seleccionar una imagen.");
                }

                if (ModelState.IsValid)
                {
                    // Registro de plato
                    entidad.Image = string.Empty;
                    var IdDish = model.RegisterDish(entidad);

                    // Guardar la imagen
                    if (ImageDish != null && ImageDish.ContentLength > 0)
                    {
                        string extension = Path.GetExtension(Path.GetFileName(ImageDish.FileName));
                        string path = ConfigurationManager.AppSettings["pathSaveImages"] + IdDish + extension;
                        ImageDish.SaveAs(path);

                        entidad.IdDish = IdDish;
                        entidad.Image = ConfigurationManager.AppSettings["pathSaveDataBase"] + IdDish + extension;
                        model.UpdatePathDish(entidad);
                    }

                    return RedirectToAction("CheckDishes", "Dish");
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción, puedes imprimir en la consola o agregar registros a un log.
                Console.WriteLine("Error en AddDish: " + ex.Message);
                // Puedes redirigir a una página de error o mostrar un mensaje al usuario.
                return RedirectToAction("Error", "Home");
            }

            return View("Add", entidad); // Vuelve a mostrar el formulario con los mensajes de error.
        }

        [HttpGet]
        public ActionResult Edit(long q)
        {
            var datos = model.CheckDish(q);
            return View(datos);
        }

        [HttpPost]
        public ActionResult UpdateDish(HttpPostedFileBase ImageDish, DishEnt entidad)
        {
            try
            {
                if (ImageDish != null && ImageDish.ContentLength > 0)
                {
                    string extension = Path.GetExtension(Path.GetFileName(ImageDish.FileName));
                    string ruta = ConfigurationManager.AppSettings["pathSaveImages"] + entidad.IdDish + extension;
                    ImageDish.SaveAs(ruta);

                    entidad.Image = ConfigurationManager.AppSettings["pathSaveDataBase"] + entidad.IdDish + extension;
                }

                if (ModelState.IsValid)
                {
                    System.IO.File.Delete(ConfigurationManager.AppSettings["pathDeleteImages"] + entidad.Image);

                    // Registro de plato
                    entidad.Image = string.Empty;
                    model.UpdateDish(entidad);

                    // Actualizar la imagen en la base de datos
                    model.UpdatePathDish(entidad);

                    return RedirectToAction("CheckDish", "Dish");
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción, puedes imprimir en la consola o agregar registros a un log.
                Console.WriteLine("Error en UpdateDish: " + ex.Message);
                // Puedes redirigir a una página de error o mostrar un mensaje al usuario.
                return RedirectToAction("Error", "Home");
            }

            return View("Edit", entidad); // Vuelve a mostrar el formulario con los mensajes de error.
        }

    }
}