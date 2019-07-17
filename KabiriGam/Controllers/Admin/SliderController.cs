using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using KabiriGam.Models;
namespace KabiriGam.Controllers
{
    public class SliderController : Controller
    {
        KabiriGam.Models.DbIrgam db = new Models.DbIrgam();
        public ActionResult Slider()
        {
            ViewBag.showallslider = db.Selectslider().OrderByDescending(x => x.InsertDate).ToList();
         
            return View();
        }

        public ActionResult InsertSliderConfirm(HttpPostedFileBase img)
        {
            byte[] b = new byte[img.ContentLength];
            img.InputStream.Read(b, 0, b.Length);
            db.Sliders.Add(new KabiriGam.Models.Slider { Pic = b,InsertDate=DateTime.Now });
            db.SaveChanges();
            return RedirectToAction("slider");
        }


        public ActionResult DeleteSlider(int id)
        {
            db.Sliders.Remove(db.Sliders.Find(id));
            db.SaveChanges();
            return RedirectToAction("slider");
        }
    }
}