using ImageUpdater.Foundation.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageUpdater.Foundation.Engines
{
    public interface IImageMetaDataEngine : IDisposable
    {
        void Insert(ImageMetaData i);
        void Update(ImageMetaData i);
        void Delete(int ImageId);
        IEnumerable<ImageMetaData> RetrieveAll();
        IEnumerable<ImageMetaData> RetrieveByPartialTagName(string tagName);
        ImageMetaData RetrieveByGuid(Guid fileGuid);
    }
}
