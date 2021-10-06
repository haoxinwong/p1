using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Controllers
{
    public class CustomerHomeController : Controller
    {
        // GET: CustomerHomeController
        public ActionResult Index()
        {
            return View();
        }

        // GET: CustomerHomeController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CustomerHomeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CustomerHomeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomerHomeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CustomerHomeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomerHomeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CustomerHomeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
