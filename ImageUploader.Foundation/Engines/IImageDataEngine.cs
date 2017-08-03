using ImageUploader.Foundation.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageUploader.Foundation.Engines
{
    public interface IImageDataEngine : IDisposable
    {
        void Insert(ImageData i);
        void Update(ImageData i);
        void Delete(int ImageId);
        ImageData RetrieveById(int Id);
        ImageData RetrieveByGuid(Guid fileGuid);
    }
}
