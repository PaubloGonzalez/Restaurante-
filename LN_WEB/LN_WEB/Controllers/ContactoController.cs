
using LN_WEB.Entities;
using LN_WEB.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http.Json;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Microsoft.Extensions.Logging;

namespace LN_WEB.Controllers
{
    public class ContactoController : Controller
    {
        AskModel model = new AskModel();



        [HttpGet]
        public ActionResult Contacto()
        {
            return View();
        }


        [HttpPost]
        public ActionResult RegisterAsk(AskEnt entidad)
        {
            try
            {
                // Validaciones del modelo
                if (ModelState.IsValid)
                {
                   
                    entidad.Estado = true;

                    var resp = model.RegisterAsk(entidad);

                    if (resp > 0)
                        return RedirectToAction("Contacto", "Contacto");
                    else
                    {
                        ViewBag.MsjPantalla = "No se ha podido registrar su información";
                        return View("Contacto");
                    }
                }
                else
                {
                    // Si hay errores de validación, vuelve a la vista de registro con los mensajes de error
                    return View("Contacto", entidad);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en Registrar Consulta: " + ex.Message);
                return RedirectToAction("Error", "Home");
            }
        }


        [HttpGet]
        public ActionResult CheckAsks()
        {
            var datos = model.CheckAsks();
            return View(datos);
        }


        [HttpPost]
        public ActionResult RemoveAsk(long q)
        {
            var resp = model.RemoveAsk(q);

            return Json("OK", JsonRequestBehavior.AllowGet);
        }


    }
}