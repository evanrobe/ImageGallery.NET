using ImageUploader.Data.Engines;
using ImageUploader.Foundation.Engines;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageUploader.Business.IOC
{
    public class NinjectConfig : NinjectModule
    {
        public override void Load()
        {
            Bind<IImageMetaDataEngine>().To<ImageMetaDataEngine>();
            Bind<IImageDataEngine>().To<ImageDataEngine>();
            Bind<IImageTagEngine>().To<ImageTagEngine>();
        }
    }
}
