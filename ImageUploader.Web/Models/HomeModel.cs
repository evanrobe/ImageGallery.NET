using ImageUploader.Foundation.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImageUploader.Web.Models
{
    public class HomeModel
    {
        public IEnumerable<ImageMetaData> Images { get; set; }
    }
}