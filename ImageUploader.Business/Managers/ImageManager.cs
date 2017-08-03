using ImageUploader.Foundation.Engines;
using ImageUploader.Foundation.Messages;
using ImageUploader.Business.Models;
using Ninject;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ImageUploader.Business.Managers
{
    public class ImageManager : IDisposable
    {
        IImageMetaDataEngine _imde;
        IImageDataEngine _ide;
        IImageTagEngine _ite;

        public ImageManager()
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());

            _imde = kernel.Get<IImageMetaDataEngine>();
            _ide = kernel.Get<IImageDataEngine>();
            _ite = kernel.Get<IImageTagEngine>();
        }

        /// <summary>
        /// Saves a new image into the system.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="imageData"></param>
        public void SaveImage(ImageMetaData i , List<ImageTag> tags , Stream imageData)
        {
            ImageData id = new ImageData();

            i.ImageGUID = Guid.NewGuid();

            _imde.Insert(i);
            id.ID = i.ID;
            id.Data = imageData;
            _ide.Insert(id);

            foreach(var tag in tags)
            {
                if (!String.IsNullOrWhiteSpace(tag.TagName))
                {
                    //make sure the image is linked correctly.
                    tag.ImageId = i.ID;
                    _ite.Insert(tag);
                }
            }
        }

        public IEnumerable<ImageMetaData> RetrieveAll()
        {
            return _imde.RetrieveAll();
        }

        public IEnumerable<ImageMetaData> RetrieveByPartialTagName(String tagName)
        {
            return _imde.RetrieveByPartialTagName(tagName);
        }

        public ImageInfo RetrieveByGuid(Guid fileGuid)
        {
            var info = new ImageInfo();
            var md = _imde.RetrieveByGuid(fileGuid);

            info.ImageGuid = md.ImageGUID;
            info.ImageId = md.ID;
            info.Tags = _ite.RetrieveByImageId(info.ImageId);
            info.ImageName = md.FileName;

            return info;
        }

        public ImageData GetImageData(Guid fileGuid)
        {
            return _ide.RetrieveByGuid(fileGuid);
        }

        public void Delete(Guid fileGuid)
        {
            var md = _imde.RetrieveByGuid(fileGuid);

            //delete the tags
            foreach(var it in this._ite.RetrieveByImageId(md.ID))
            {
                _ite.Delete(it.ID);
            }

            //delete the content
            _ide.Delete(md.ID);

            //delete the image metadata
            _imde.Delete(md.ID);

        }

        public void Dispose()
        {
            _imde.Dispose();
            _ide.Dispose();
            _ite.Dispose();
        }
    }
}
