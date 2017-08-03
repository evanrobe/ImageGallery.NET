using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ImageUploader.Foundation.Messages
{
    public class ImageData : ImageMetaData
    {
        public virtual Stream Data { get; set; }
    }
}
