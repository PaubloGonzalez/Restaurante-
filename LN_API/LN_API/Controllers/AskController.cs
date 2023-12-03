using LN_API.Entities;
using LN_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace LN_API.Controllers
{
    [Authorize]
    public class AskController : ApiController
    {

        [HttpGet]
        [Route("api/CheckAsks")]
        public List<AskEnt> CheckAsks()
        {
            using (var bd = new EL_VARONEntities())
            {
                var datos = (from x in bd.Ask
                             select x).ToList();

                if (datos.Count > 0)
                {
                    var resp = new List<AskEnt>();
                    foreach (var item in datos)
                    {
                        resp.Add(new AskEnt
                        {
                            IdAsk = item.IdAsk,
                            Name = item.Name,
                            Email = item.Email,
                            Phone = item.Phone,
                            TypeAsk = item.TypeAsk,
                            Message = item.Message,
                        });
                    }

                    return resp;
                }
                else
                {
                    return new List<AskEnt>();
                }
            }
        }

        [HttpGet]
        [Route("api/CheckAsk")]
        public AskEnt CheckAsk(long q)
        {
            using (var bd = new EL_VARONEntities())
            {
                var datos = (from x in bd.Ask
                             where x.IdAsk == q
                             select x).FirstOrDefault();

                if (datos != null)
                {

                   
                    AskEnt resp = new AskEnt();
                    resp.IdAsk = datos.IdAsk;
                    resp.Name = datos.Name;
                    resp.Email = datos.Email;
                    resp.Phone = datos.Phone; 
                    resp.TypeAsk = datos.TypeAsk;
                    resp.Message = datos.Message;
                    return resp;
                }
                else
                {
                    return null;
                }
            }
        }




        [HttpDelete]
        [Route("api/RemoveAsk")]
        public int RemoveAsk(long q)
        {
            using (var bd = new EL_VARONEntities())
            {
                var askEncontrado = (from x in bd.Ask
                                      where x.IdAsk == q
                                      select x).FirstOrDefault();

                if (askEncontrado != null)
                {
                    bd.Ask.Remove(askEncontrado);
                    return bd.SaveChanges();
                }

                return 0;
            }
        }


        [HttpPost]
        [Route("api/RegisterAsk")]
        [AllowAnonymous]
        public IHttpActionResult RegisterAsk(AskEnt entidad)
        {
            try
            {
                using (var bd = new EL_VARONEntities())
                {
                    Ask tabla = new Ask();
                    tabla.Name = entidad.Name;
                    tabla.Email = entidad.Email;
                    tabla.Phone = entidad.Phone;
                    tabla.TypeAsk = entidad.TypeAsk;
                    tabla.Message = entidad.Message;
                    tabla.Estado = entidad.Estado;

                    bd.Ask.Add(tabla);

                    int result = bd.SaveChanges();

                    return Ok(result); // Devuelve el resultado como respuesta HTTP 200 OK
                }
            }
            catch (Exception ex)
            {
                // Registra y maneja la excepción
                Console.WriteLine("Error en RegisterAsk: " + ex.Message, ex);
                return InternalServerError(); // Devuelve un error interno del servidor
            }
        }





        [HttpPut]
        [Route("api/UpdateAsk")]
        public void UpdateAsk(AskEnt entidad)
        {
            using (var bd = new EL_VARONEntities())
            {
                var askEncontrado = (from x in bd.Ask
                                      where x.IdAsk == entidad.IdAsk
                                      select x).FirstOrDefault();

                if (askEncontrado != null)
                {
                    askEncontrado.Name = entidad.Name;
                    askEncontrado.Email = entidad.Email;
                    askEncontrado.Phone = entidad.Phone;
                    askEncontrado.TypeAsk = entidad.TypeAsk;
                    askEncontrado.Message = entidad.Message;

                    bd.SaveChanges();
                }

            }
        }

    }
}
