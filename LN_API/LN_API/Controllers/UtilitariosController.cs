using LN_API.Entities;
using LN_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LN_API.Controllers
{
    [Authorize]
    public class UtilitariosController : ApiController
    {

        [HttpPost]
        [Route("api/RegistrarBitacora")]
        public int RegistrarBitacora(BitacoraEnt entidad)
        {
            using (var bd = new EL_VARONEntities())
            {
                Bitacora tabla = new Bitacora();
                tabla.FechaHora = entidad.FechaHora;
                tabla.Mensaje = entidad.Mensaje;
                tabla.Origen = entidad.Origen;
                tabla.IdUsuario = entidad.IdUsuario;
                tabla.DireccionIP = entidad.DireccionIP;

                bd.Bitacora.Add(tabla);
                return bd.SaveChanges();
            }
        }

    }
}
