using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Controllers
{
    public class AdminHomeController : Controller
    {
        // GET: AdminHomeController
        public ActionResult Index()
        {
            return View();
        }

        
    }
}
