
using LN_WEB.Entities;
using LN_WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LN_WEB.Controllers
{
    public class ReportesController : Controller
    {

        [HttpGet]
        public ActionResult Reportes()
        {
            return View();
        }


    }
}