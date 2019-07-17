using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KabiriGam.Models;
namespace KabiriGam.Controllers
{
    public class ContactUsManagmentController : Controller
    {
        KabiriGam.Models.DbIrgam db = new Models.DbIrgam();
        public ActionResult DeleteContactUs(int id)
        {
            var find = db.Contactus.Find(id);
            if (find.Id != null)
            {
                db.Contactus.Remove(db.Contactus.Find(find.Id));


            }
            db.SaveChanges();
            TempData["msgsuccess"] = "با موفقیت حذف شد";
            return RedirectToAction("ManageContactUs");
        }
        public ActionResult ManageContactUs()
        {
            ViewBag.ShowAllContacts = db.SelectAllOneContacts().OrderByDescending(x => x.Date).ToList();
            return View();
        }
        public ActionResult ShowOneContact(int id)
        {
            TempData["kkk"] = db.Contactus.Where(x => x.Id == id).Select(x => x.Txt).ToList();
            return View(db.Contactus.Where(x => x.Id == id).ToList());
        }
    }
}