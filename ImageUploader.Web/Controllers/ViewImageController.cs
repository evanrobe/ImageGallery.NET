using ImageUploader.Business.Managers;
using ImageUploader.Business.Models;
using ImageUploader.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImageUploader.Web.Controllers
{
    public class ViewImageController : Controller
    {
        // GET: ViewImage
        public ActionResult Index(String fileId)
        {
            var man = new ImageManager();
            var ii = man.RetrieveByGuid(Guid.Parse(fileId));
            var model = new ViewImageModel();
            var tagNames = new List<String>();

            foreach(var t in ii.Tags)
            {
                tagNames.Add(t.TagName);
            }

            model.FileGuid = ii.ImageGuid;
            model.Tags = String.Join(" , ", tagNames);
            model.FileName = ii.ImageName;

            return View(model);
        }
    }
}