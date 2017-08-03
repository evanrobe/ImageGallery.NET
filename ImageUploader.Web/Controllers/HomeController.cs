using ImageUpdater.Foundation.Messages;
using ImageUploader.Business.Managers;
using ImageUploader.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImageUploader.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(String tagName)
        {
            var man = new ImageManager();
            var model = new HomeModel();

            if (String.IsNullOrWhiteSpace(tagName))
                model.Images = man.RetrieveAll();
            else
                model.Images = man.RetrieveByPartialTagName(tagName);

            return View(model);
        }




    }
}