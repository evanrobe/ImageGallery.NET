﻿using ImageUpdater.Foundation.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageUpdater.Foundation.Engines
{
    public interface IImageTagEngine
    {
        void Insert(ImageTag imageTag);
        void Delete(int Id);
        IEnumerable<ImageTag> RetrieveByImageId(int imageId);
    }
}
