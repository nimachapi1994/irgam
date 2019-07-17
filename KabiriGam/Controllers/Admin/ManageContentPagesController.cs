using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KabiriGam.Models;

namespace KabiriGam.Controllers.Admin
{
    public class ManageContentPagesController : Controller
    {
        KabiriGam.Models.DbIrgam db = new DbIrgam();
       public ActionResult PageAboutUs()
        {
            ViewBag.show = db.AboutUsPages.Where(x => x.Id == 1).ToList();
            return View();
        }
       
        public ActionResult PageAboutUsUpdated(AboutUsPage p)
        {
            var find = db.AboutUsPages.Find(1);
            find.PageContent = HttpUtility.HtmlDecode(p.PageContent);
            TempData["success"] = "با موفقیت ویرایش شد";
            db.SaveChanges();
            return Redirect("/ManageContentPages/PageAboutUs");
        }
    }
}