using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LN_API.Entities
{
    public class DishEnt
    {
        public long IdDish { get; set; }
        public string Name { get; set; }
        public string Ingredients { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
    }
}