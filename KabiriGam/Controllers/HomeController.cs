using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using IdentitySample.Models;
using System.Globalization;

namespace KabiriGam.Controllers
{
    public class HomeController : Controller
    {
        public ApplicationRoleManager rolemngr
        {
            get
            {
                return HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
        }

        public ApplicationUserManager usermngr
        {
            get
            {
                return HttpContext.GetOwinContext().Get<ApplicationUserManager>();
            }
        }
        public ApplicationSignInManager singinmnger
        {
            get
            {
                return HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }

        }
        KabiriGam.Models.DbIrgam db = new Models.DbIrgam();

        public ActionResult contactusconfirm(KabiriGam.Models.Contactu cu)
        {
            db.Contactus.Add(new Models.Contactu
            {
                NameAndFamily = cu.NameAndFamily,
                Txt = cu.Txt,
                Date = DateTime.Now,
                Email = cu.Email
              ,
                PhoneNumber = cu.PhoneNumber,
                Subject = cu.Subject
            });
            db.SaveChanges();
            TempData["Successok"] = ".پیغام شما با موفقیت ارسال شد در اولین فرصت پاسخگوی شما هستیم , با تشکر";

            return RedirectToAction("ContactUs");
        }
        public ActionResult ContactUs()
        {
            return View();
        }
        //show All
        public ActionResult ShowAllServices()
        {
            ViewBag.Show = db.SelectAllServices().OrderByDescending(x => x.date).ToList();
            return View();
        }
        public ActionResult ShowAllBlogs()
        {
            ViewBag.Show = db.SelectAllContents().OrderByDescending(x => x.Date).ToList();
            return View();
        }
        public ActionResult ShowAllProjects()
        {
            ViewBag.Show = db.SelectAllProjects().OrderByDescending(x => x.Date).ToList();
            return View();
        }
        //show One
        public ActionResult showOneService(int id)
        {
            Session["Serviceid"] = id;
            ViewBag.show = db.SelectOneService(id).ToList();
            return View();
        }

        public ActionResult showOneBlog(int id)
        {
            Session["blogid"] = id;
            ViewBag.show = db.SelectOneContent(id).ToList();
            return View();
        }
        public ActionResult showOneProject(int id)
        {
            Session["projectid"] = id;
            ViewBag.show = db.SelectOneProject(id).ToList();

            return View();
        }
        //SendComment
        public ActionResult GetOneCommentForBlog(string Name, string email, string txt)
        {
            int id
            = (int)Session["blogid"];
            db.CommentsForContents.Add(new Models.CommentsForContent
            {
                Content_Id = id,
                date = DateTime.Now,
                Email = email,
                IsShow = false,
                Name = Name,
                Txt = txt
            });
            db.SaveChanges();
            Session.Remove("blogid");
            TempData["ok"] = "دیدگاه شما با موفقیت ذخیره شد پس از بررسی در سایت نشان داده خواهد شد.";

            return Redirect($"showOneBlog?id={id}");
        }
        public ActionResult GetOneCommentForService(string Name, string email, string txt)
        {
            int id
            = (int)Session["Serviceid"];
            db.CommentsForServices.Add(new Models.CommentsForService
            {
                Id = 1,
                Name = Name,
                Email = email,
                Txt = txt,
                Date = DateTime.Now,
                IsShow = false
                ,
                Services_Id = id
            });
            db.SaveChanges();
            Session.Remove("Serviceid");
            TempData["ok"] = "دیدگاه شما با موفقیت ذخیره شد پس از بررسی در سایت نشان داده خواهد شد.";

            return Redirect($"showOneService?id={id}");
        }
        public ActionResult GetOneCommentForProject(string Name, string email, string txt)
        {
            int id
            = (int)Session["projectid"];
            db.CommentsForProjects.Add(new Models.CommentsForProject
            {
                Name = Name,
                Email = email,
                Txt = txt,
                date1 = DateTime.Now,
                IsShow = false
                ,
                Project_Id = id
            });
            db.SaveChanges();
            Session.Remove("projectid");
            TempData["ok"] = "دیدگاه شما با موفقیت ذخیره شد پس از بررسی در سایت نشان داده خواهد شد.";
            return Redirect($"showOneProject?id={id}");
        }
        public ActionResult Login()
        {

            return View();
        }
        public ActionResult LoginConfirm(LoginViewModel model)
        {

            return View();
        }
        public ActionResult AboutUs()
        {
            ViewBag.show = db.AboutUsPages.ToList();
            return View();
        }
        public ActionResult Questions()
        {
            return View();
        }
        public static string EngToPer(DateTime dt)
        {
            PersianCalendar pcal = new PersianCalendar();
            string datetime = $"{pcal.GetYear(dt)}/{pcal.GetMonth(dt)}/{pcal.GetDayOfMonth(dt)}";
            return datetime;
        }
        public ActionResult AdminLogin()
        {

            return View();

        }
        public ActionResult Index()
        {
            //string role1 = "admin";
            //if (rolemngr.RoleExists(role1) == false)
            //{
            //    IdentityRole role = new IdentityRole("admin");
            //    rolemngr.Create(role);
            //}

            ViewBag.ShowServices6 = db.SelectAllServices().OrderByDescending(x => x.date).Take(6).ToList();
            ViewBag.ShowProjects6 = db.SelectAllProjects().OrderByDescending(x => x.Date).Take(6).ToList();
            ViewBag.ShowBlogs6 = db.SelectAllContents().OrderByDescending(x => x.Date).Take(3).ToList();
            ViewBag.ShowSlider = db.Selectslider().OrderByDescending(x => x.InsertDate).ToList();

            var user = usermngr.FindByEmail("irgam1396@gmail.com");
            if (user == null)
            {
                ApplicationUser admin = new ApplicationUser()
                {
                    UserName = "irgam1396@gmail.com",
                    PhoneNumber = "09178134699",
                    Email = "irgam1396@gmail.com",
                    EmailConfirmed = true,

                };
                var usersuccess = usermngr.Create(admin, "irgam1396@gmail.com");
                if (usersuccess.Succeeded == true)
                {
                    usermngr.AddToRole(admin.Id, "admin");

                }
            }
            return View();
        }
    }
}