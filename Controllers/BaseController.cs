using imgstack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace imgstack.Controllers
{
    public class BaseController : Controller
    {
        public DataContext _db = new DataContext();

        public BaseController()
        {
            if (System.Web.HttpContext.Current.Request.IsAuthenticated)
            {
                if (System.Web.HttpContext.Current.Session["user"] == null)
                {
                    int userID = int.Parse(System.Web.HttpContext.Current.User.Identity.Name);
                    System.Web.HttpContext.Current.Session["user"] = _db.User.Where(p => p.ID == userID).FirstOrDefault();
                }
            }
        }
    }
}