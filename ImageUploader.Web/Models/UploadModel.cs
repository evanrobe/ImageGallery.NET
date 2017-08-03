using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImageUploader.Web.Models
{
    public class UploadModel
    {
        public String Description { get; set; }
        public List<String> Tags { get; set; }
    }
}