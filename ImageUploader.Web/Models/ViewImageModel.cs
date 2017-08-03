using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImageUploader.Web.Models
{
    public class ViewImageModel
    {
        public String FileName { get; set; }
        public String Tags { get; set; }
        public Guid FileGuid { get; set; }
    }
}