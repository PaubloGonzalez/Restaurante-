using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LN_WEB.Entities
{
    public class CarritoEnt
    {
        public long IdCursoCarrito { get; set; }
        public long IdCurso{ get; set; }
        public long IdUsuario{ get; set; }
        public DateTime FechaRegistro{ get; set; }
        public decimal Precio { get; set; }
        public decimal Impuesto { get; set; }
        public string Nombre { get; set; }
        public string Instructor { get; set; }
    }
}