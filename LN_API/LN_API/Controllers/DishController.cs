using LN_API.Entities;
using LN_API.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LN_API.Controllers
{
    [Authorize]
    public class DishController : ApiController
    {

        [HttpGet]
        [Route("api/CheckDishes")]
        public List<DishEnt> CheckDishes()
        {
            using (var bd = new EL_VARONEntities())
            {
                var datos = (from x in bd.Dish
                             select x).ToList();

                if (datos.Count > 0)
                {
                    var resp = new List<DishEnt>();
                    foreach (var item in datos)
                    {
                        resp.Add(new DishEnt
                        {
                            IdDish = item.IdDish,
                            Name = item.Name,
                            Ingredients = item.Ingredients,
                            Price = item.Price,
                            Image = item.Image,
                            
                        });
                    }

                    return resp;
                }
                else
                {
                    return new List<DishEnt>();
                }
            }
        }

        [HttpGet]
        [Route("api/CheckDish")]
        public DishEnt CheckDish(long q)
        {
            using (var bd = new EL_VARONEntities())
            {
                var datos = (from x in bd.Dish
                             where x.IdDish == q
                             select x).FirstOrDefault();

                if (datos != null)
                {
                    DishEnt resp = new DishEnt();
                    resp.IdDish = datos.IdDish;
                    resp.Name = datos.Name;
                    resp.Ingredients = datos.Ingredients;
                    resp.Price = datos.Price; 
                    resp.Image = datos.Image;
                    return resp;
                }
                else
                {
                    return null;
                }
            }
        }

        [HttpGet]
        [Route("api/CheckCart")]
        public List<CartEnt> CheckCart(long q)
        {
            using (var bd = new EL_VARONEntities())
            {
                var datos = (from x in bd.DishCart
                             join y in bd.Dish on x.IdDish equals y.IdDish
                             where x.IdUsuario == q
                             select new {
                                 x.IdDishCart,
                                 x.IdDish,
                                 x.IdUsuario,
                                 x.RegistDate,
                                 y.Price,
                                 y.Name,
                             }).ToList();

                if (datos.Count > 0)
                {
                    var resp = new List<CartEnt>();
                    foreach (var item in datos)
                    {
                        resp.Add(new CartEnt
                        {
                            IdDishCart = item.IdDishCart,
                            IdDish = item.IdDish,
                            IdUsuario = item.IdUsuario,
                            RegistDate = item.RegistDate,
                            Price = item.Price,
                            Tax = item.Price * 0.13M,
                            Name = item.Name
                        });
                    }

                    return resp;
                }
                else
                {
                    return new List<CartEnt>();
                }
            }
        }

        [HttpGet]
        [Route("api/CheckMyDishes")]
        public List<CartEnt> CheckMyDishes(long q)
        {
            using (var bd = new EL_VARONEntities())
            {
                var datos = (from x in bd.DishUsuario
                             join y in bd.Dish on x.IdDish equals y.IdDish
                             where x.IdUsuario == q
                             select new
                             {
                                 x.IdDishUsuario,
                                 x.IdDish,
                                 x.IdUsuario,
                                 x.RegistDate,
                                 x.PricePaid,
                                 y.Name
                             }).ToList();

                if (datos.Count > 0)
                {
                    var resp = new List<CartEnt>();
                    foreach (var item in datos)
                    {
                        resp.Add(new CartEnt
                        {
                            IdDishCart = item.IdDishUsuario,
                            IdDish = item.IdDish,
                            IdUsuario = item.IdUsuario,
                            RegistDate = item.RegistDate,
                            Price = item.PricePaid,
                            Tax = item.PricePaid * 0.13M,
                            Name = item.Name
                        });
                    }

                    return resp;
                }
                else
                {
                    return new List<CartEnt>();
                }
            }
        }

        [HttpPost]
        [Route("api/AddDishCart")]
        public int AddDishCart(CartEnt entidad)
        {
            using (var bd = new EL_VARONEntities())
            {
                DishCart tabla = new DishCart();
                tabla.IdDish = entidad.IdDish;
                tabla.IdUsuario = entidad.IdUsuario;
                tabla.RegistDate = entidad.RegistDate;

                bd.DishCart.Add(tabla);
                return bd.SaveChanges();
            }
        }



        [HttpDelete]
        [Route("api/RemoveDish")]
        public int RemoveDish(long q)
        {
            using (var bd = new EL_VARONEntities())
            {
                var dishEncontrado = (from x in bd.Dish
                                      where x.IdDish == q
                                      select x).FirstOrDefault();

                if (dishEncontrado != null)
                {
                    bd.Dish.Remove(dishEncontrado);
                    return bd.SaveChanges();
                }

                return 0;
            }
        }

        [HttpDelete]
        [Route("api/RemoveDishCart")]
        public int RemoveDishCart(long q)
        {
            using (var bd = new EL_VARONEntities())
            {
                var dishEncontrado = (from x in bd.DishCart
                                       where x.IdDishCart == q
                                       select x).FirstOrDefault();

                if (dishEncontrado != null)
                {
                    bd.DishCart.Remove(dishEncontrado);
                    return bd.SaveChanges();
                }

                return 0;                
            }
        }


        [HttpDelete]
        [Route("api/RemoveAllDishCart")]
        public int RemoveAllDishCart(long userId)
        {
            using (var bd = new EL_VARONEntities())
            {
                var dishesEncontrados = bd.DishCart.Where(x => x.IdUsuario == userId).ToList();

                if (dishesEncontrados.Count > 0)
                {
                    bd.DishCart.RemoveRange(dishesEncontrados);
                    return bd.SaveChanges();
                }

                return 0;
            }
        }





        [HttpPost]
        [Route("api/ConfirmPaymentCart")]
        public int ConfirmPaymentCart(CartEnt entidad)
        {
            using (var bd = new EL_VARONEntities())
            {
                //buscar el carrito para pasarlo a la tabla de los dish del usuario
                var datos = (from cc in bd.DishCart
                             join c in bd.Dish on cc.IdDish equals c.IdDish
                             where cc.IdUsuario == entidad.IdUsuario
                             select new
                             {
                                 cc.IdDish,
                                 cc.IdUsuario,
                                 c.Price
                             }).ToList();

                if (datos.Count > 0)
                {
                    foreach (var item in datos)
                    {
                        DishUsuario cu = new DishUsuario();
                        cu.IdUsuario = item.IdUsuario;
                        cu.IdDish = item.IdDish;
                        cu.RegistDate = DateTime.Now;
                        cu.PricePaid = item.Price;

                        bd.DishUsuario.Add(cu);
                    }

                    //buscar el carrito para limpiarlo
                    var cartActual = (from cc in bd.DishCart
                                         where cc.IdUsuario == entidad.IdUsuario
                                        select cc).ToList();

                    foreach (var item in cartActual)
                    {
                        bd.DishCart.Remove(item);
                    }

                    return bd.SaveChanges();
                }

                return 0;

            }
        }


        [HttpPost]
        [Route("api/RegisterDish")]
        public IHttpActionResult RegisterDish(DishEnt entidad)
        {
            try
            {
                if (entidad == null || string.IsNullOrWhiteSpace(entidad.Name) || entidad.Price <= 0)
                {
                    return BadRequest("Los datos del plato son inválidos o están incompletos.");
                }

                using (var bd = new EL_VARONEntities())
                {
                    Dish tabla = new Dish();
                    tabla.Name = entidad.Name;
                    tabla.Price = entidad.Price;
                    tabla.Image = entidad.Image;

                    bd.Dish.Add(tabla);
                    bd.SaveChanges();

                    return Ok(tabla.IdDish);
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }



        [HttpPut]
        [Route("api/UpdatePathDish")]
        public void UpdatePathDish(DishEnt entidad)
        {
            using (var bd = new EL_VARONEntities())
            {
                var dishEncontrado = (from x in bd.Dish
                                       where x.IdDish == entidad.IdDish
                                       select x).FirstOrDefault();

                if (dishEncontrado != null)
                {
                    dishEncontrado.Image = entidad.Image;
                    bd.SaveChanges();
                }

            }
        }

        [HttpPut]
        [Route("api/UpdateDish")]
        public void UpdateDish(DishEnt entidad)
        {
            using (var bd = new EL_VARONEntities())
            {
                var dishEncontrado = (from x in bd.Dish
                                      where x.IdDish == entidad.IdDish
                                      select x).FirstOrDefault();

                if (dishEncontrado != null)
                {
                    dishEncontrado.Name = entidad.Name;
                    dishEncontrado.Price = entidad.Price;
                    bd.SaveChanges();
                }

            }
        }

    }
}
