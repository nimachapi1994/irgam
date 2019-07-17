using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KabiriGam.Models;

namespace KabiriGam.Controllers
{
    public class ServicesController : Controller
    {
        KabiriGam.Models.DbIrgam db = new Models.DbIrgam();
        public ActionResult ManagmentServices()
        {
            return View();
        }
        public ActionResult AddService(Service ser, HttpPostedFileBase img)
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
            db.InsertOneService(ser.Name, DateTime.Now, HttpUtility.HtmlDecode(ser.Txt), b,ser.ShortTxt);
            db.SaveChanges();
            TempData["Success"] = "سرویس مورد نظر با موفقیت ذخیره شد";
            return RedirectToAction("ManagmentServices");
        }
        public ActionResult ShowAllServices()
        {
            ViewBag.showAllServices = db.SelectAllServices().OrderByDescending(x=>x.date).ToList();
            return View();
        }
        public ActionResult Deleteservices(int id)
        {
            db.DeleteOneService(id);
            db.SaveChanges();
            TempData["msgsuccess"] = "عملیات حذف با موفقیت انجام شد";
            return RedirectToAction("ShowAllServices");
        }
        public ActionResult Editservices(int id)
        {
            Session["id"] = id;

            ViewBag.showService = db.SelectOneService(id).ToList();
            return View();
        }
        public ActionResult EditservicesConfirm(Service ser, HttpPostedFileBase img)
        {
            byte[] b = { };
            var id = (int)Session["id"];
            var findid = db.Services.Find(id);
            if (img == null)
            {
                b = findid.Pic;
            }
            else
            {
                b = new byte[img.ContentLength];
                img.InputStream.Read(b, 0, b.Length);
            }
            findid.date = DateTime.Now;
            findid.Name = ser.Name;
            findid.Pic = b;
            findid.ShortTxt = ser.ShortTxt;
            findid.Txt = HttpUtility.HtmlDecode(ser.Txt);
            db.SaveChanges();
            TempData["Success"] = "خدمت مورد نظر با موفقیت ویرایش شد";
            return RedirectToAction("ShowAllServices");
        }
    }
}