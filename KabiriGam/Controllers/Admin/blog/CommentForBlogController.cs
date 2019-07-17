using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KabiriGam.Controllers.Admin.blog
{
    public class CommentForBlogController : Controller
    {
        KabiriGam.Models.DbIrgam db = new Models.DbIrgam();
        public ActionResult ShowAllComments(int id)
        {

            Session["id"] = id;
            var d = db.Contents.Find(id);

            ViewBag.showName = d.Name;
            return View(db.SelectAllOneContentComment(id).OrderByDescending(x => x.date).ToList());

        }
        public ActionResult NotConfirmCommentByAdmin(int id)
        {
            int id1 = (int)Session["id"];
            var d = db.CommentsForContents.Find(id);
            d.IsShow = false;
            db.SaveChanges();

            //Session.Remove("id");
            return Redirect($"ShowAllComments?id={id1}");
        }
        public ActionResult ConfirmCommentByAdmin(int id)
        {

            int id1 = (int)Session["id"];
            var d = db.CommentsForContents.Find(id);
            d.IsShow = true;
            db.SaveChanges();

            //Session.Remove("id");
            return Redirect($"ShowAllComments?id={id1}");
        }
        public ActionResult DeleteComment(int id)
        {
            int id1 = (int)Session["id"];
            db.DeleteOneContentCommen(id);
            db.SaveChanges();
            Session.Remove("id");
            return Redirect($"ShowAllComments?id={id1}");
        }
    }
}