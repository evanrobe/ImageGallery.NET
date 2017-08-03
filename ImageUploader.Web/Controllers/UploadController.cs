using ImageUploader.Foundation.Messages;
using ImageUploader.Business.Managers;
using ImageUploader.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace ImageUploader.Web.Controllers
{
    public class UploadController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var model = new UploadModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(UploadModel model, HttpPostedFileBase file)
        {
            var man = new ImageManager();
            var md = new ImageMetaData();
            var tags = new List<ImageTag>();

            if (file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                md.FileName = fileName;
                md.ContentType = file.ContentType;
                md.Description = model.Description;
                foreach (var tag in model.Tags)
                {
                    ImageTag t = new ImageTag();

                    t.TagName = tag;
                    tags.Add(t);
                }

                man.SaveImage(md , tags , file.InputStream);
            }

            return RedirectToAction("Index" , "Home");
        }
    }
}