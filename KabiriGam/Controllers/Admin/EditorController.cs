using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cobiax.Controllers.Admin
{
    //[Authorize(Roles = "admin")]
    public class EditorController : Controller
    {
        public void Upload(HttpPostedFileBase Upload, string CKEditorFuncNUM)
        {
            string[] ExStr = { "image/jpg", "image/jpeg", "image/png" };

            if (!ExStr.Contains(Upload.ContentType))
            {
                string sc1 = @"<script>window.parent.CKEDITOR.tools.callFunction(" + CKEditorFuncNUM + ",\"" + "قالب فایل اشتباه است" + "\");</script>";
                Response.Write(sc1);

                Response.End();

                return;
            }

            if (Upload.ContentLength > 3145728)
            {
                string sc1 = @"<script>window.parent.CKEDITOR.tools.callFunction(" + CKEditorFuncNUM + ",\"" + "حجم فایل نباید بیشتر از 3 مگابایت باشد" + "\");</script>";
                Response.Write(sc1);

                Response.End();

                return;
            }

            string FileName = Upload.FileName;
            string Address = Server.MapPath("~/Uploads/") + FileName;

            Upload.SaveAs(Address);

            string Url = "http://www.irgam.com/Uploads/" + FileName;
          

            string sc = @"<script>window.parent.CKEDITOR.tools.callFunction(" + CKEditorFuncNUM + ",\"" + Url + "\");</script>";
            Response.Write(sc);

            Response.End();
        }


    }
}