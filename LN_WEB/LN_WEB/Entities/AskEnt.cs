using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LN_WEB.Entities
{
    public class AskEnt
    {
        public long IdAsk { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string TypeAsk { get; set; }
        public string Message { get; set; }
        public bool Estado { get; set; }
    }
}