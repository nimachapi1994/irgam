using KabiriGam.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HamiZarf.Controllers
{
    //[Authorize(Roles = "admin")]
    public class SocialNetworksManagmentController : Controller
    {
        DbIrgam db = new DbIrgam();
        public ActionResult AddSocialNetwork(SocialNetwork s)
        {
            var find = db.SocialNetworks.Find(1);
            find.soroush = s.soroush;

            find.Instagram = s.Instagram;
            find.Tlg = s.Tlg;
            find.LinkedIn = s.LinkedIn;
            find.aparat = s.aparat;
            find.GooglePlus = s.GooglePlus;
            db.SaveChanges();
            TempData["msgSocialNeteworks"] = "اطلاعات با موفقیت ذخیره گردید";
            return RedirectToAction("SocialNetworksManagment");
        }
        public ActionResult SocialNetworksManagment()
        {

            ViewBag.showallsocialnetwoks = db.SocialNetworks.ToList();
            return View();
        }
    }
}