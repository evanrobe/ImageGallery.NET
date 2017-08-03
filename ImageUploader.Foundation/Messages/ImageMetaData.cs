using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ImageUploader.Foundation.Messages
{
    public class ImageMetaData
    {
        public virtual int ID { get; set; }
        public virtual Guid ImageGUID { get; set; }
        public virtual String Description { get; set; }
        public virtual String FileName { get; set; }
        public virtual String ContentType { get; set; }
    }
}
