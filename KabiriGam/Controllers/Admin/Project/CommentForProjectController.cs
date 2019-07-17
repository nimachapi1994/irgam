using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KabiriGam.Models;
namespace KabiriGam.Controllers.Admin.Project
{
    public class CommentForProjectController : Controller
    {
        KabiriGam.Models.DbIrgam db = new Models.DbIrgam();
        //public ActionResult InsertComment(int id,CommentsForProject c)
        //{
        //    db.InsertOneCommentForOneProject(c.Name, c.Email, c.Txt, id, DateTime.Now);
        //    db.SaveChanges();
        //    return View();
        //}
        public ActionResult ShowAllComments(int id)
        {
            Session["id"] = id;
            var d = db.Projects.Find(id);

            ViewBag.showName = d.Name;
            return View(db.SelectAllOneProjectComment(id).OrderByDescending(x => x.date1).ToList());
            
        }
        public ActionResult NotConfirmCommentByAdmin(int id)
        {
            int id1 = (int)Session["id"];
            var d = db.CommentsForProjects.Find(id);
            d.IsShow = false;
            db.SaveChanges();

            return Redirect($"ShowAllComments?id={id1}");
        }
        public ActionResult ConfirmCommentByAdmin(int id)
        {
            //int id1 = (int)Session["id"];
            //var d= db.CommentsForProjects.Find(id);
            //d.IsShow = true;
            //db.SaveChanges();
            //Session.Remove("id");
            //return Redirect($"ShowAllComments?id={id1}");
            int id1 = (int)Session["id"];
            var d = db.CommentsForProjects.Find(id);
            d.IsShow = true;
            db.SaveChanges();

         return Redirect($"ShowAllComments?id={id1}");
        }
        public ActionResult DeleteComment(int id)
        {
            int id1 = (int)Session["id"];
            db.DeleteOneProjectComment(id);
            db.SaveChanges();
            Session.Remove("id");
            return Redirect($"ShowAllComments?id={id1}");
        }
    }
}