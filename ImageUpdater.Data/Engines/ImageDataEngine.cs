using ImageUpdater.Foundation.Engines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ImageUpdater.Foundation.Messages;
using System.IO;

namespace ImageUpdater.Data.Engines
{
    public class ImageDataEngine : IImageDataEngine
    {
        public void Delete(int ImageId)
        {
            var imageDir = System.Configuration.ConfigurationManager.AppSettings["imageStorageLocation"].ToString();
            var imageName = imageDir + "/" + ImageId.ToString();

            if (File.Exists(imageName))
                File.Delete(imageName);
        }

        public void Dispose()
        {

        }

        public void Insert(ImageData i)
        {
            var imageDir = System.Configuration.ConfigurationManager.AppSettings["imageStorageLocation"].ToString();

            if (!Directory.Exists(imageDir))
                Directory.CreateDirectory(imageDir);

            using (FileStream fs = new FileStream(imageDir + "/" + i.ID.ToString(), FileMode.Create))
            {
                i.Data.CopyTo(fs);
            }
        }

        public ImageData RetrieveById(int Id)
        {
            ImageMetaDataEngine e = new ImageMetaDataEngine();
            var imageDir = System.Configuration.ConfigurationManager.AppSettings["imageStorageLocation"].ToString();
            var ret = new ImageData();

            var md = e.RetrieveById(Id);

            FileStream fs = new FileStream(imageDir + "/" + Id.ToString(), FileMode.Open);

            ret.Data = fs;
            ret.ID = Id;
            ret.FileName = md.FileName;
            ret.ContentType = md.ContentType;
            ret.ImageGUID = md.ImageGUID;

            return ret;
        }

        public ImageData RetrieveByGuid(Guid imageGuid)
        {
            ImageMetaDataEngine e = new ImageMetaDataEngine();
            var imageDir = System.Configuration.ConfigurationManager.AppSettings["imageStorageLocation"].ToString();
            var ret = new ImageData();
            var md = e.RetrieveByGuid(imageGuid);
            var Id = md.ID;

            FileStream fs = new FileStream(imageDir + "/" + Id.ToString(), FileMode.Open);

            ret.Data = fs;
            ret.ID = Id;
            ret.FileName = md.FileName;
            ret.ContentType = md.ContentType;
            ret.ImageGUID = md.ImageGUID;

            return ret;
        }

        public void Update(ImageData i)
        {
            throw new NotImplementedException();
        }
    }
}
