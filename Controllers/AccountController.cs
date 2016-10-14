using imgstack.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace imgstack.Controllers
{
    public class AccountController : BaseController
    {
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User newuser = new User();

                newuser.FirstName = model.FirstName;
                newuser.LastName = model.LastName;
                newuser.Alias = model.Alias;
                newuser.Email = model.Email;
                newuser.Password = model.Password;
                newuser.Type = 1;
                newuser.Deleted = false;

                string directory = Server.MapPath("/content/img/profiles/");

                if (model.upimg != null && model.upimg.ContentLength > 0)
                {
                    var fileExt = Path.GetExtension(model.upimg.FileName).Substring(1);
                    var fileName = string.Format(@"{0}." + fileExt, Guid.NewGuid());
                    model.upimg.SaveAs(Path.Combine(directory, fileName));

                    newuser.Picture = Path.GetFileNameWithoutExtension(fileName) + "." + fileExt;

                    _db.User.Add(newuser);
                    _db.SaveChanges();
                }

                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model, string ReturnUrl = "")
        {
            User user = _db.User.Where(p => p.Alias.Equals(model.Alias) && p.Password.Equals(model.Password)).FirstOrDefault();

            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(user.ID.ToString(), model.RememberLogin);
                Session["user"] = user;

                if (Url.IsLocalUrl(ReturnUrl))
                {
                    return Redirect(ReturnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            return View();
        }

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }
    }
}