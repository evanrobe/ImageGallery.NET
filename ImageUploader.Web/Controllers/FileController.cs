using ImageUploader.Business.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace ImageUploader.Web.Controllers
{
    public class FileController : Controller
    {
        // GET: File
        public ActionResult Index(String fileId)
        {
            var man = new ImageManager();
            var i = man.GetImageData(Guid.Parse(fileId));

            return new FileStreamResult(i.Data , i.ContentType);
        }
    }
}