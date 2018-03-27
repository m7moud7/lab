using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Labyrinth.Abstracts;
using Labyrinth.Services;
using Labyrinth.Model;
using Labyrinth.BackEnd.App_Start;
using System.Drawing;
using System.IO;

namespace Labyrinth.BackEnd.Controllers
{
    public class AttachmentController : BaseController
    {
        private readonly IAttachment _Attachment;
        public AttachmentController()
        {
            _Attachment = new AttachmentServices();
        }

        // GET: Attachment
        public ActionResult AddFeaturedImage()
        {
            return View(_Attachment.GetSettings("Images", ""));
        }

        public ActionResult AddFeaturedImageArticle()
        {
            return View(_Attachment.GetSettings("Images", ""));
        }

        [HttpPost]
        public JsonResult Add(FormCollection frm)
        {
            int counter = 0;
            long NewId = 0;
            var DateTimePath = DateTime.Now;
            AttachmentVM ViewModel = new AttachmentVM();
            string MSG = "";
            foreach (var key in frm.AllKeys.Where(k => k.Contains("Attach")).ToList())
            {
                try
                {
                    if (counter == 0)
                    {
                        ViewModel.ALtImage = frm["Caption"];
                        ViewModel.Caption = frm["Caption"];
                        ViewModel.CUser = uservm.ID;
                        ViewModel.CDate = DateTimePath;
                        ViewModel.Path = uservm.ID.ToString() + uservm.Username.GetHashCode().ToString().Replace("-", "") + DateTimePath.ToString("yyyyMMddhhmmssms") + ".jpg";
                        NewId = _Attachment.Save(ViewModel);
                        MSG = "\\Images\\" + DateTimePath.Year + "\\" + DateTimePath.Month + "\\" + "Large" + "\\" + ViewModel.Path;
                    }

                    if (NewId > 0)
                    {
                        var KeyPath = frm[frm.AllKeys.Where(k => k.Contains("SetKey_" + counter)).FirstOrDefault()];
                        var OriginalPath = frm[frm.AllKeys.Where(k => k.Contains("Val1_" + counter)).FirstOrDefault()];

                        var FolderYear = Server.MapPath("\\Images\\" + DateTimePath.Year);
                        if (!Directory.Exists(FolderYear))
                            Directory.CreateDirectory(FolderYear);

                        var FolderMonth = Server.MapPath("\\Images\\" + DateTimePath.Year + "\\" + DateTimePath.Month);
                        if (!Directory.Exists(FolderMonth))
                            Directory.CreateDirectory(FolderMonth);

                        var FolderSetKey = Server.MapPath("\\Images\\" + DateTimePath.Year + "\\" + DateTimePath.Month + "\\" + KeyPath);
                        if (!Directory.Exists(FolderSetKey))
                            Directory.CreateDirectory(FolderSetKey);

                        var FolderOriginal = Server.MapPath("\\Images\\" + DateTimePath.Year + "\\" + DateTimePath.Month + "\\" + KeyPath + "\\" + OriginalPath);
                        if (!Directory.Exists(FolderOriginal))
                            Directory.CreateDirectory(FolderOriginal);

                        string Original = frm["Main"].Replace("data:image/jpeg;base64,", String.Empty).Replace("data:image/png;base64,", String.Empty);
                        using (MemoryStream ms1 = new MemoryStream(Convert.FromBase64String(Original)))
                        {
                            using (Bitmap bm1 = new Bitmap(ms1))
                            {
                                bm1.Save(Server.MapPath("\\Images\\" + DateTimePath.Year + "\\" + DateTimePath.Month + "\\" + KeyPath + "\\" + OriginalPath + "\\" + ViewModel.Path));
                            }
                        }

                        string ImageSave = frm[key].Replace("data:image/jpeg;base64,", String.Empty).Replace("data:image/png;base64,", String.Empty);
                        using (MemoryStream ms2 = new MemoryStream(Convert.FromBase64String(ImageSave)))
                        {
                            using (Bitmap bm2 = new Bitmap(ms2))
                            {
                                bm2.Save(Server.MapPath("\\Images\\" + DateTimePath.Year + "\\" + DateTimePath.Month + "\\" + KeyPath + "\\" + ViewModel.Path));
                            }
                        }
                    }
                }
                catch
                {

                }
                counter++;
            }
            return Json(MSG);
        }

        //[HttpPost]
        public JsonResult Addtest(AttachmentOrderVM viewmodel /*FormCollection array*/)
        {
            //  var dsd = array["images[]"];
            // var value1 = "";
            //  foreach (var key in array.AllKeys)
            //   {
            //    value1 = array[key];
            // etc.
            //}
            //  var jgjhgjgkjgkjg = value1;

            var jgjhgjgkjgkjg1 = "";

            //var value2 = "";
            //foreach (var key in array.Keys)
            //{
            //     value2 = array[key.ToString()];
            //    // etc.
            //}
            //System.Drawing.ImageConverter converter = new System.Drawing.ImageConverter();
            //Image img = (Image)converter.ConvertFrom(array);

            using (MemoryStream ms = new MemoryStream(Convert.FromBase64String("")))
            {
                using (Bitmap bm2 = new Bitmap(ms))
                {
                    bm2.Save(System.Web.HttpContext.Current.Server.MapPath("\\Userimage\\" + "1" + ".jpg"));
                }
            }
            string dsdsaas = "";


            return this.Json("Done Hanaa");
        }
    }
}