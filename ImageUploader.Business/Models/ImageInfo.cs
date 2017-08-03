using ImageUploader.Foundation.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageUploader.Business.Models
{
    public class ImageInfo
    {
        public IEnumerable<ImageTag> Tags { get; set; }
        public String ImageName { get; set; }
        public int ImageId { get; set; }
        public Guid ImageGuid { get; set; }
        public String Description { get; set; }
    }
}
