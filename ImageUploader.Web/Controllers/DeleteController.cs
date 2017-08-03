using ImageUploader.Business.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImageUploader.Web.Controllers
{
    public class DeleteController : Controller
    {
        // GET: Delete
        public ActionResult Index(string fileId)
        {
            var man = new ImageManager();

            man.Delete(Guid.Parse(fileId));

            return new RedirectResult("/");
        }
    }
}