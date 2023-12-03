using LN_WEB.Entities;
using LN_WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LN_WEB.Controllers
{
    public class UsuarioController : Controller
    {
        UsuarioModel model = new UsuarioModel();



        [HttpGet]
        public ActionResult ConsultaUsuarios()
        {
            var resp = model.ConsultaUsuarios();
            return View(resp);
        }

        [HttpGet]
        public ActionResult Perfil()
        {
            return View();
        }


        [HttpGet]
        public ActionResult CambiarEstado(long q)
        {
            UsuarioEnt entidad = new UsuarioEnt();
            entidad.IdUsuario = q;

            var resp = model.CambiarEstado(entidad);

            if (resp > 0)
                return RedirectToAction("ConsultaUsuarios", "Usuario");
            else
            {
                ViewBag.MsjPantalla = "No se ha podido actualizar el estado del usuario";
                return View("ConsultaUsuarios");
            }
        }

        [HttpGet]
        public ActionResult Editar(long q)
        {
            var resp = model.ConsultaUsuario(q);
            var respRoles = model.ConsultaRoles();

            var roles = new List<SelectListItem>();
            foreach (var item in respRoles)
            {
                roles.Add(new SelectListItem { Value = item.IdRol.ToString(), Text = item.NombreRol.ToString() });
            }

            ViewBag.ComboRoles = roles;
            return View(resp);
        }

        [HttpPost]
        public ActionResult EditarUsuario(UsuarioEnt entidad)
        {
            var resp = model.EditarUsuario(entidad);

            if (resp > 0)
                return RedirectToAction("ConsultaUsuarios", "Usuario");
            else
            {
                ViewBag.MsjPantalla = "No se ha podido actualizar la información del usuario";
                return View("ConsultaUsuarios");
            }
        }

        [HttpPost]
        public ActionResult RemoverUsuario(long q)
        {
            var resp = model.RemoverUsuario(q);
            ActualizarTotales();

            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        private void ActualizarTotales()
        {
            var usuarioActual = model.ConsultaUsuario(long.Parse(Session["IdUsuario"].ToString()));
           
        }
    }
}