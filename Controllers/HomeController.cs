using imgstack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace imgstack.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            HomeViewModel model = new HomeViewModel();

            model.Images = _db.Image.Take(20).OrderByDescending(p => p.ID).ToList();
            model.Users = _db.User.Take(10).OrderByDescending(p => p.ID).ToList();

            return View(model);
        }
    }
}