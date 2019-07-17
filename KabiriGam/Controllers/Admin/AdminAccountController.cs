using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using IdentitySample.Models;

namespace Cobiax.Controllers.Admin
{
   
    public class AdminAccountController : Controller
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
        KabiriGam.Models.DbIrgam db = new KabiriGam.Models.DbIrgam();
        //[Authorize(Roles = "admin")]
        public ActionResult ResetpassConfirm(string newpass)
        {
            var hash = usermngr.PasswordHasher.HashPassword(newpass);

            var find = db.AspNetUsers.Find(User.Identity.GetUserId());
           
            find.PasswordHash = hash;
            db.SaveChanges();
            TempData["ok"] = "پسورد شما با موفقیت ذخیره شد لطفا پسورد خود را همیشه به یاد داشته باشید";


            return RedirectToAction("Resetpass","AdminAccount");
        }
        //[Authorize(Roles = "admin")]
        public ActionResult Resetpass()
        {

            return View();

        }
        //[Authorize(Roles = "admin")]
        public ActionResult SignOut()
        {
            HttpContext.GetOwinContext().Authentication.SignOut();
            return Redirect("/Home/adminlogin");
        }
        public ActionResult LoginConfirm(LoginViewModel model)
        {
            var user = singinmnger.PasswordSignIn(model.Email,model.Password,model.RememberMe,true);
            if (user == SignInStatus.Success)
            {
                return RedirectToAction("ControlPanel", "AdminControlpanel");
            }
            else
            {
                TempData["not"] = "نام کاربری یا رمز عبور شما اشتباه است ";
                return Redirect("/Home/adminlogin");
            }
        }
    }
}