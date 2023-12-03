using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Web.Mvc;
using LN_WEB.Models.Paypal_Transaction;
using LN_WEB.Models.Paypal_Order;
using LN_WEB.Entities;
using System.Configuration;
using System.Web.WebPages;

namespace LN_WEB.Controllers
{
    public class PaypalController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

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
                }

            }


            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public async Task<JsonResult> Paypal(decimal total)
        {
            bool status = false;
            string respuesta = string.Empty;

            using (var client = new HttpClient())
            {
               
                var userName = "Ac4o9Ls_j3QbJCO1jl8rGDMFDZ6V0mAEG7exPjxYIMcEoFSlS9qqCzW877gRxKLRiakmGdvFMOkCOzsI";
                var passwd = "EJMvVLT303jt8i_Paf9KqkNbVy72gKLqlho3qVj2F27DUJ8R8iCrMA7xm_ZQdEeNbwIYjhqJDmvunhD8";

                client.BaseAddress = new Uri("https://api-m.sandbox.paypal.com");

                var authToken = Encoding.ASCII.GetBytes($"{userName}:{passwd}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authToken));

                // Crear la unidad de compra para PayPal
                var purchaseUnit = new Models.Paypal_Order.PurchaseUnit()
                {
                    amount = new Models.Paypal_Order.Amount()
                    {
                        currency_code = "USD",
                        value = total.ToString() 
                    },
                    description = "Total del carrito" //
                };

               
                var orden = new PaypalOrder()
                {
                    intent = "CAPTURE",
                    purchase_units = new List<Models.Paypal_Order.PurchaseUnit> { purchaseUnit },
                    application_context = new ApplicationContext()
                    {
                        brand_name = "Mi Tienda",
                        landing_page = "NO_PREFERENCE",
                        user_action = "PAY_NOW",
                        return_url = "https://localhost:44328/Dish/About",
                        cancel_url = "https://localhost:44328/Dish/CheckPayment"
                    }
                };

                var json = JsonConvert.SerializeObject(orden);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync("/v2/checkout/orders", data);

                status = response.IsSuccessStatusCode;

                if (status)
                {
                    respuesta = response.Content.ReadAsStringAsync().Result;
                    
                }
               
            }

            return Json(new { status = status, respuesta = respuesta }, JsonRequestBehavior.AllowGet);
        }


        




    }

}
