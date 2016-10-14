using imgstack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace imgstack.Controllers
{
    public class UserController : BaseController
    {
        // GET: User
        [Route("user/{alias}")]
        public ActionResult Index(string alias)
        {
            ProfileViewModel model = new ProfileViewModel();

            model.User = _db.User.Where(p => p.Alias.ToLower() == alias.ToLower()).FirstOrDefault();
            if (model.User == null)
            {
                return RedirectToAction("Index", "Home");
            }
            model.Stacks = _db.Stack.Where(p => p.FK_Creator == model.User.ID).ToList();



            return View(model);
        }
    }
}