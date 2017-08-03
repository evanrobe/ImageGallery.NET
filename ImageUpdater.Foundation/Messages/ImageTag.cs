using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageUpdater.Foundation.Messages
{
    public class ImageTag
    {
        public int ID { get; set; }
        public String TagName { get; set; }
        public int ImageId { get; set; }
        public virtual Guid roImageGUID { get; set; }
        public virtual String roFileName { get; set; }
        public virtual String roContentType { get; set; }
    }
}
