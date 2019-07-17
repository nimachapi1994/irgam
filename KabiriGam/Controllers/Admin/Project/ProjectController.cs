using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KabiriGam.Models;

namespace KabiriGam.Controllers
{
    public class ProjectController : Controller
    {
        KabiriGam.Models.DbIrgam db = new Models.DbIrgam();
        public ActionResult ManagmentProject()
        {
            return View();
        }
        public ActionResult AddProject(Project pro, HttpPostedFileBase img)
        {
            byte[] b = { };

            if (img == null)
            {
                b = System.IO.File.ReadAllBytes(Server.MapPath("~/Img/NotHavePic/NotHavePic.png"));
            }
            else
            {
                b = new byte[img.ContentLength];
                img.InputStream.Read(b, 0, b.Length);
            }
            db.InsertOneProject(pro.Name, HttpUtility.HtmlDecode(pro.Txt), b);
            db.SaveChanges();
            TempData["Success"] = "پروژه مورد نظر با موفقیت ذخیره شد";
            return RedirectToAction("ManagmentProject");
        }
        public ActionResult ShowAllProjects()
        {
            ViewBag.showAllProjects = db.SelectAllProjects().OrderByDescending(x => x.Date).ToList();
            return View();
        }
        public ActionResult DeleteProjects(int id)
        {
            db.DeleteOneProject(id);
            db.SaveChanges();
            TempData["msgsuccess"] = "عملیات حذف با موفقیت انجام شد";
            return RedirectToAction("ShowAllProjects");
        }
        public ActionResult EditProjects(int id)
        {
            Session["id"] = id;

            ViewBag.showproject = db.SelectOneProject(id).ToList();
            return View();
        }
        public ActionResult EditProjectConfirm(Project pro, HttpPostedFileBase img)
        {
            byte[] b = { };
            var id = (int)Session["id"];
            //var findid = db.Projects.Remove(db.Projects.Find(id));
            var findid = db.Projects.Find(id);
            if (img == null)
            {
                b = findid.logo;
            }
            else
            {
                b = new byte[img.ContentLength];
                img.InputStream.Read(b, 0, b.Length);
            }
            db.UpdateOneProject(pro.Name, HttpUtility.HtmlDecode(pro.Txt), b, id);
            db.SaveChanges();
            Session.Remove("id");
            TempData["Success1"] = "پروژه مورد نظر با موفقیت ویرایش شد";
            return RedirectToAction("ShowAllProjects");
        }
    }
}