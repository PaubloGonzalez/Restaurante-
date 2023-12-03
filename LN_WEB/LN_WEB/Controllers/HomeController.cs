
using LN_WEB.Entities;
using LN_WEB.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace LN_WEB.Controllers
{
    public class HomeController : Controller
    {
        UsuarioModel model = new UsuarioModel();

        [HttpGet]
        public ActionResult About()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult IniciarSesion(UsuarioEnt entidad)
        {
            var resp = model.IniciarSesion(entidad);

            if (resp != null)
            {
                Session["IdUsuario"] = resp.IdUsuario;
                Session["CorreoUsuario"] = resp.CorreoElectronico;
                Session["NombreUsuario"] = resp.Nombre;
                Session["NombreRolUsuario"] = resp.NombreRol;
                Session["RolUsuario"] = resp.IdRol;
                Session["Token"] = resp.Token;
                return RedirectToAction("Inicio", "Home");
            }
            else
            {
                ViewBag.MsjPantalla = "No se ha podido validar su información";
                return View("Login");
            }
        }




        [HttpGet]
        public ActionResult Registro()
        {
            return View();
        }


        //[HttpPost]
        //public ActionResult RegistrarUsuario(HttpPostedFileBase ImageUsuario,  UsuarioEnt entidad)
        //{
        //    entidad.IdRol = 2;
        //    entidad.Estado = true;
        //    var IdUsuario = model.RegistrarUsuario(entidad);
        //    var resp = model.RegistrarUsuario(entidad);

        //    //Guardar la imagen
        //    string extension = Path.GetExtension(Path.GetFileName(ImageUsuario.FileName));
        //    string ruta = ConfigurationManager.AppSettings["pathSaveImages"] + IdUsuario + extension;
        //    ImageUsuario.SaveAs(ruta);


        //    entidad.Image = ConfigurationManager.AppSettings["pathSaveDataBase"] + IdUsuario + extension;
        //    model.UpdatePathUsuario(entidad);

        //    if (resp > 0)
        //        return RedirectToAction("Login", "Home");
        //    else
        //    {
        //        ViewBag.MsjPantalla = "No se ha podido registrar su información";
        //        return View("Registro");
        //    }
        //}


        [HttpPost]
        public ActionResult RegistrarUsuario(UsuarioEnt entidad)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    entidad.IdRol = 2;
                    entidad.Estado = true;

                    var resp = model.RegistrarUsuario(entidad);

                    if (resp > 0)
                        return RedirectToAction("Login", "Home");
                    else
                    {
                        ViewBag.MsjPantalla = "Ya existe un usuario con ese correo o número de cedula";
                        return View("Registro");
                    }
                }
                else
                {
                    return View("Registro", entidad);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en RegistrarUsuario: " + ex.Message);
                return RedirectToAction("Error", "Home");
            }
        }



        public ActionResult Error(Exception exception)
        {
            // Puedes registrar el error en un log aquí si es necesario
            Console.WriteLine("Error: " + exception.Message);

            // Pasar el modelo de error a la vista
            var model = new ErrorViewModel
            {
                ErrorMessage = "¡Ups, algo salió mal! Por favor, inténtalo de nuevo más tarde."
            };

            return View("Error", model);
        }


        [HttpGet]
        public ActionResult Recuperar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RecuperarClave(UsuarioEnt entidad)
        {
            var resp = model.RecuperarClave(entidad);

            if (resp)
                return RedirectToAction("Login", "Home");
            else
            {
                ViewBag.MsjPantalla = "No se ha podido recuperar su acceso";
                return View("Recuperar");
            }

        }

      

        [HttpGet]
        public ActionResult Inicio()
        {

            return View();
        }

 

       



        [HttpGet]
        public ActionResult CerrarSesion()
        {
            Session.Clear();
            return RedirectToAction("Login", "Home");
        }


        [HttpPost]
        public ActionResult CambiarClave(UsuarioEnt entidad)
        {
            entidad.CorreoElectronico = Session["CorreoUsuario"].ToString();
            entidad.IdUsuario = long.Parse(Session["IdUsuario"].ToString());
            var respValidarClave = model.IniciarSesion(entidad);

            if (respValidarClave == null)
            {
                ViewBag.MsjPantalla = "La actual no coincide con su registro en la base de datos";
                return View("Cambiar");
            }

            if (entidad.ContrasennaNueva != entidad.ConfirmarContrasennaNueva)
            {
                ViewBag.MsjPantalla = "Las nueva contraseña no coincide con su confirmación";
                return View("Cambiar");
            }

            var respCambiarClave = model.CambiarClave(entidad);

            if (respCambiarClave > 0)
                return RedirectToAction("Inicio", "Home");
            else
            {
                ViewBag.MsjPantalla = "No se ha podido cambiar su contraseña actual";
                return View("Cambiar");
            }

        }

  

    }
}