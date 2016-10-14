using imgstack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace imgstack.Controllers
{
    public class StackController : BaseController
    {
        // GET: Gallery
        [Route("stack/{ID:int}")]
        public ActionResult Index(int Id)
        {
            if (Id < 1)
            {
                return RedirectToAction("Index", "Home");
            }

            StackViewModel model = new StackViewModel();

            model.Stack = _db.Stack.Where(p => p.ID == Id).FirstOrDefault();
            model.User = _db.User.Where(p => p.ID == model.Stack.FK_Creator).FirstOrDefault();
            model.Comments = _db.Comment.Where(p => p.FK_Stack == Id && p.FK_Image == null).ToList();

            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        [Route("stacks")]
        public ActionResult Stacks(int? qty)
        {
            List<Stack> StackList = new List<Stack>();

            if (qty.HasValue)
            {
                StackList = _db.Stack.Take(qty.Value).ToList();
            }
            else
            {
                StackList = _db.Stack.ToList();
            }

            return View(StackList);
        }

        public ActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(StackCreateViewModel model)
        {
            int iUserID = int.Parse(System.Web.HttpContext.Current.User.Identity.Name);
            User user = _db.User.Where(p => p.ID == iUserID).FirstOrDefault();

            if (ModelState.IsValid)
            {
                Stack newstack = new Stack();

                newstack.Name = model.Name;
                newstack.Public = model.Public;
                newstack.FK_Creator = user.ID;
                newstack.DateCreated = DateTime.Now;

                _db.Stack.Add(newstack);
                _db.SaveChanges();

                return RedirectToAction(user.Alias, "User");
            }

            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddComment(StackViewModel model)
        {
            StackViewModel CommentData = Session["CommentData"] as StackViewModel;
            int iStackID = CommentData.Stack.ID;
            int iUserID = int.Parse(System.Web.HttpContext.Current.User.Identity.Name);

            if (iUserID < 0 && model.NewComment == "")
            {
                return RedirectToAction(iStackID.ToString(), "Stack");
            }

            Comment comment = new Comment();

            comment.Text = model.NewComment;
            comment.FK_Stack = iStackID;
            comment.DateCreated = DateTime.Now;
            comment.Deleted = false;
            comment.FK_User = iUserID;

            _db.Comment.Add(comment);
            _db.SaveChanges();

            return RedirectToAction(iStackID.ToString(), "Stack");
        }

        public ActionResult DeleteComment(int ID)
        {
            int userID = int.Parse(System.Web.HttpContext.Current.User.Identity.Name);

            Comment comment = new Comment();
            Stack stack = new Stack();

            comment = _db.Comment.Where(p => p.ID == ID).FirstOrDefault();
            stack = _db.Stack.Where(p => p.ID == comment.FK_Stack).FirstOrDefault();

            if (stack != null)
            {
                string actionlink = "/stack/" + stack.ID;

                if (userID == comment.FK_User)
                {
                    _db.Comment.Remove(comment);
                    _db.SaveChanges();
                }

                return Redirect(actionlink);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}