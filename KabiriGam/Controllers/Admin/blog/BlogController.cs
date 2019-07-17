using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KabiriGam.Models;
namespace KabiriGam.Controllers
{
    public class BlogController : Controller
    {
        KabiriGam.Models.DbIrgam db = new Models.DbIrgam();
        public ActionResult BlogManagment()
        {
            return View();
        }
        public ActionResult AddBlog(Content c,HttpPostedFileBase img)
        {
            byte[] b = { };
            if (img==null)
            {
                b = System.IO.File.ReadAllBytes(Server.MapPath("~/Img/NotHavePic/NotHavePic.png"));
            }
            else
            {
                b = new byte[img.ContentLength];
                img.InputStream.Read(b, 0, b.Length);
            }
            db.insertOneContent(c.Name, HttpUtility.HtmlDecode(c.Txt), DateTime.Now, b,c.ShortTxt);
            
            db.SaveChanges();
            TempData["Success1"] = "وبلاگ مورد نظر با موفقیت ذخیره شد";
            return RedirectToAction("BlogManagment", "blog");
        }
        public ActionResult Showallblogs()
        {
            return View(db.SelectAllContents().OrderByDescending(x=>x.Date).ToList());
        }
        public ActionResult DeleteBlog(int id)
        {
            db.DeleteOneContent(id);
            //db.CommentsForContents.Find(db.CommentsForContents.Where(x => x.Content_Id == id).ToList());
            db.SaveChanges();
            return RedirectToAction("Showallblogs");
        }
        public ActionResult EditeBlog(int id)
        {
            Session["id"] = id;
            ViewBag.Showediteblog = db.SelectOneContent(id);
            return View();
        }
        public ActionResult EditeBlogConfirm(Content c,HttpPostedFileBase img)
        {
            int id = (int)Session["id"];
            var findid = db.Contents.Find(id);
                 byte[] b = { };
            if (img == null)
            {
                b = findid.pic;
            }
            else
            {
                b = new byte[img.ContentLength];
                img.InputStream.Read(b, 0, b.Length);
            }
            db.UpdateOneContent(c.Name, HttpUtility.HtmlDecode(c.Txt), DateTime.Now, b, id,c.ShortTxt);
            Session.Remove("id");
            db.SaveChanges();
            TempData["Success"] = "وبلاگ مورد نظر با موفقیت ویرایش شد";
            return RedirectToAction("Showallblogs");
        }
    }
}