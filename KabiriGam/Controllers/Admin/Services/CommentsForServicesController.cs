using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KabiriGam.Controllers.Admin.Services
{
    public class CommentsForServicesController : Controller
    {
        KabiriGam.Models.DbIrgam db = new Models.DbIrgam();
      
        public ActionResult ShowAllComments(int id)
        {
            Session["id"] = id;
            var d = db.Services.Find(id);

            ViewBag.showName = d.Name;
            return View(db.SelectAllCommentForOneServices(id).OrderByDescending(x => x.Date).ToList());

        }
        public ActionResult NotConfirmCommentByAdmin(int id)
        {
            int id1 = (int)Session["id"];
            var d = db.CommentsForServices.Find(id);
            d.IsShow = false;
            db.SaveChanges();
       
            return Redirect($"ShowAllComments?id={id1}");
        }
        public ActionResult ConfirmCommentByAdmin(int id)
        {

            int id1 = (int)Session["id"];
            var d = db.CommentsForServices.Find(id);
            d.IsShow = true;
            db.SaveChanges();
          
            return Redirect($"ShowAllComments?id={id1}");
        }
        public ActionResult DeleteComment(int id)
        {
            int id1 = (int)Session["id"];
            db.DeleteOneServiceComment(id);
            db.SaveChanges();
            Session.Remove("id");
            return Redirect($"ShowAllComments?id={id1}");
        }
    }
}