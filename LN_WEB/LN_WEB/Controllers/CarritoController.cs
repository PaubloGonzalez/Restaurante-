using LN_WEB.Entities;
using LN_WEB.Models;
using OpenAI_API.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace LN_WEB.Controllers
{
    public class CarritoController : Controller
    {
        private readonly DishModel model = new DishModel();

        [HttpGet]
        public ActionResult AddDishCart(long q)
        {
            CartEnt entidad = new CartEnt();
            entidad.IdDish = q;
            entidad.IdUsuario = long.Parse(Session["IdUsuario"].ToString());
            entidad.RegistDate = DateTime.Now;

            var resp = model.AddDishCart(entidad);
            UpdateTotals();

            if (resp > 0)
                return RedirectToAction("Menu", "Dish");
            else
            {
                ViewBag.MsjPantalla = "El plato ya ha sido comprado o añadido a tu carrito de compras";
                var dishes = model.CheckDishes();
                return View("../Dish/Menu", dishes);
            }
        }

        


        [HttpPost]
        public async Task<ActionResult> RemoveFromCart(long itemId)
        {
            try
            {
                // Llama al método RemoveFromCart en tu modelo para enviar la solicitud a la API
                int result =  model.RemoveDishCart(itemId);

                if (result > 0)
                {
                    // Actualiza los totales en la sesión
                    UpdateTotals();

                    // Devuelve una respuesta JSON indicando éxito
                    return Json(new { success = true });
                }
                else
                {
                    // Devuelve una respuesta JSON indicando que no se pudo eliminar el elemento
                    return Json(new { success = false, error = "No se pudo eliminar el elemento del carrito." });
                }
            }
            catch (Exception ex)
            {
                // Loguea el error o maneja según tu necesidad
                return Json(new { success = false, error = ex.Message });
            }
        }


        private void UpdateTotals()
        {
            var CurrentCart = model.CheckCart(long.Parse(Session["IdUsuario"].ToString()));
            Session["Amount"] = CurrentCart.Count();
            Session["SubTotal"] = CurrentCart.Sum(x => x.Price);
            Session["Total"] = CurrentCart.Sum(x => x.Price) + (CurrentCart.Sum(x => x.Price) * 0.13M);
        }

       

    }
}
