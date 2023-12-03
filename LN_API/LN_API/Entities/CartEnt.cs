using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LN_WEB.Entities
{
    public class CartEnt
    {
        public long IdDishCart { get; set; }
        public long IdDish { get; set; }
        public long IdUsuario { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; }
        public decimal Tax { get; set; }
        public DateTime RegistDate { get; set; }
    }
}