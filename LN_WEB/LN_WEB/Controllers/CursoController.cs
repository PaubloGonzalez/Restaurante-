using LN_WEB.Entities;
using LN_WEB.Models;
using OpenAI_API.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LN_WEB.Controllers
{
    public class CursoController : Controller
    {
        CursoModel model = new CursoModel();

        [HttpGet]
        public ActionResult AgregarCursoCarrito(long q)
        { 
            CarritoEnt entidad = new CarritoEnt();
            entidad.IdCurso = q;
            entidad.IdUsuario = long.Parse(Session["IdUsuario"].ToString());
            entidad.FechaRegistro = DateTime.Now;

            var resp = model.AgregarCursoCarrito(entidad);
            ActualizarTotales();

            if (resp > 0)
                return RedirectToAction("Inicio", "Home");
            else
            {
                ViewBag.MsjPantalla = "El curso ya fue comprado o agregado a su carrito de compras";
                var cursos = model.ConsultaCursos();
                return View("../Home/Inicio", cursos);
            }
        }

        [HttpPost]
        public ActionResult RemoverCursoCarrito(long q)
        {
            var resp = model.RemoverCursoCarrito(q);
            ActualizarTotales();

            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        private void ActualizarTotales()
        {
            var carritoActual = model.consultarCarrito(long.Parse(Session["IdUsuario"].ToString()));
            Session["Cantidad"] = carritoActual.Count();
            Session["SubTotal"] = carritoActual.Sum(x => x.Precio);
            Session["Total"] = carritoActual.Sum(x => x.Precio) + (carritoActual.Sum(x => x.Precio) * 0.13M);
        }

        [HttpGet]
        public ActionResult ConsultaPago()
        {
            var datos = model.consultarCarrito(long.Parse(Session["IdUsuario"].ToString()));
            return View(datos);
        }

        [HttpPost]
        public ActionResult ConfirmarPagoCarrito()
        {
            CarritoEnt entidad = new CarritoEnt();
            entidad.IdUsuario = long.Parse(Session["IdUsuario"].ToString());

            model.ConfirmarPagoCarrito(entidad);
            return RedirectToAction("Inicio", "Home");
        }

        [HttpGet]
        public ActionResult ConsultarMisCursos()
        {
            var datos = model.consultarMisCursos(long.Parse(Session["IdUsuario"].ToString()));
            return View(datos);
        }


        [HttpGet]
        public ActionResult ConsultaCursos()
        {
            var datos = model.ConsultaCursos();
            return View(datos);
        }

        [HttpGet]
        public ActionResult Agregar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AgregarCurso(HttpPostedFileBase ImagenCurso, CursoEnt entidad)
        {
            //Registro de curso
            entidad.Imagen = string.Empty;
            var IdCurso = model.RegistrarCurso(entidad);

            //Guardar la imagen
            string extension = Path.GetExtension(Path.GetFileName(ImagenCurso.FileName));
            string ruta = ConfigurationManager.AppSettings["rutaGuardarImagenes"] + IdCurso + extension;
            ImagenCurso.SaveAs(ruta);

            entidad.IdCurso = IdCurso;
            entidad.Imagen = ConfigurationManager.AppSettings["rutaGuardarBaseDatos"] + IdCurso + extension;
            model.ActualizarRutaCurso(entidad);

            return RedirectToAction("ConsultaCursos", "Curso");
        }

        [HttpGet]
        public ActionResult Editar(long q)
        {
            var datos = model.ConsultaCurso(q);
            return View(datos);
        }

        [HttpPost]
        public ActionResult ActualizarCurso(HttpPostedFileBase ImagenCurso, CursoEnt entidad)
        {
            System.IO.File.Delete(ConfigurationManager.AppSettings["rutaEliminarImagenes"] + entidad.Imagen);

            //Registro de curso
            entidad.Imagen = string.Empty;
            model.ActualizarCurso(entidad);

            //Guardar la imagen
            string extension = Path.GetExtension(Path.GetFileName(ImagenCurso.FileName));
            string ruta = ConfigurationManager.AppSettings["rutaGuardarImagenes"] + entidad.IdCurso + extension;
            ImagenCurso.SaveAs(ruta);

            entidad.Imagen = ConfigurationManager.AppSettings["rutaGuardarBaseDatos"] + entidad.IdCurso + extension;
            model.ActualizarRutaCurso(entidad);

            return RedirectToAction("ConsultaCursos", "Curso");
        }        

    }
}