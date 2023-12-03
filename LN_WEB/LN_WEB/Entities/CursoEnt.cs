using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LN_WEB.Entities
{
    public class CursoEnt
    {
        public long IdCurso { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public string Imagen { get; set; }
        public string Instructor { get; set; }
        public string Video { get; set; }
    }
}