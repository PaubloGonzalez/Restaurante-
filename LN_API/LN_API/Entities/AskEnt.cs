using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LN_API.Entities
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