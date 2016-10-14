using imgstack.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace imgstack.Controllers
{
    public class ImageController : BaseController
    {
        // GET: Image
        [Route("img/{filename}-{ID}")]
        public ActionResult Index(int ID, string filename)
        {
            ImageViewModel model = new ImageViewModel();
            model.Image = _db.Image.Where(p => p.Filename == filename).FirstOrDefault();
            model.Stack = _db.Stack.Where(p => p.ID == model.Image.FK_Stack).FirstOrDefault();
            model.Comments = _db.Comment.Where(p => p.FK_Image == ID).ToList();
            model.User = _db.User.Where(p => p.ID == model.Image.FK_Creator).FirstOrDefault();

            if (ModelState.IsValid)
            {
                return View(model);
            }

            return View();
        }

        [Authorize]
        [Route("img/redirecttoupload/{ID}")]
        public ActionResult RedirectToUpload(int ID)
        {
            int userID = int.Parse(System.Web.HttpContext.Current.User.Identity.Name);
            Session["StackID"] = ID;

            Stack stack = _db.Stack.Where(p => p.ID == ID).FirstOrDefault();

            if (userID != stack.FK_Creator)
            {
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Upload", "Image");
        }

        [Authorize]
        [Route("img/upload")]
        public ActionResult Upload(ImageUploadViewModel model)
        {
            int userID = int.Parse(System.Web.HttpContext.Current.User.Identity.Name);

            if (Session["StackID"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            model.Stacks = _db.Stack.Where(p => p.FK_Creator == userID).ToList();
            model.FK_Stack = int.Parse(Session["StackID"].ToString());

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddComment(ImageViewModel model)
        {
            ImageViewModel CommentData = Session["CommentData"] as ImageViewModel;
            int iStackID = CommentData.Stack.ID;
            int iImageID = CommentData.Image.ID;
            int iUserID = int.Parse(System.Web.HttpContext.Current.User.Identity.Name);

            string actionlink = "/img/" + CommentData.Image.Filename + "-" + iImageID;

            if (iUserID > 0 && model.NewComment != "")
            {
                Comment comment = new Comment();

                comment.Text = model.NewComment;
                comment.FK_Stack = iStackID;
                comment.FK_Image = iImageID;
                comment.DateCreated = DateTime.Now;
                comment.Deleted = false;
                comment.FK_User = iUserID;

                _db.Comment.Add(comment);
                _db.SaveChanges();
            }

            return Redirect(actionlink);
        }

        [Authorize]
        [Route("img/uploadpost")]
        [HttpPost]
        public ActionResult UploadPost(ImageUploadViewModel VmImg)
        {
            imgstack.Models.Image newimg = new imgstack.Models.Image();

            int userID = int.Parse(System.Web.HttpContext.Current.User.Identity.Name);
            User user = _db.User.Where(p => p.ID == userID).FirstOrDefault();

            newimg.Name = VmImg.Name;
            newimg.FK_Stack = VmImg.FK_Stack;
            newimg.FK_Creator = user.ID;
            newimg.DateCreated = DateTime.Now;
            newimg.Deleted = false;

            string directory = Server.MapPath("/content/img/userimages/");

            if (VmImg.upimg != null && VmImg.upimg.ContentLength > 0)
            {
                var fileExt = Path.GetExtension(VmImg.upimg.FileName).Substring(1);
                newimg.Filetype = fileExt;

                var fileName = string.Format(@"{0}." + fileExt, Guid.NewGuid());
                VmImg.upimg.SaveAs(Path.Combine(directory, fileName));

                newimg.Filename = Path.GetFileNameWithoutExtension(fileName);

                _db.Image.Add(newimg);
                _db.SaveChanges();
            }

            return RedirectToAction(VmImg.FK_Stack.ToString(), "stack");
        }

        public ActionResult DeleteComment(int ID)
        {
            int userID = int.Parse(System.Web.HttpContext.Current.User.Identity.Name);

            Comment comment = new Comment();
            imgstack.Models.Image image = new imgstack.Models.Image();

            comment = _db.Comment.Where(p => p.ID == ID).FirstOrDefault();
            image = _db.Image.Where(p => p.ID == comment.FK_Image).FirstOrDefault();

            if (image != null)
            {
                string actionlink = "/img/" + image.Filename + "-" + image.ID;

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

