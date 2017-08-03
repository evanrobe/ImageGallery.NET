using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using ImageUpdater.Foundation.Messages;
using ImageUploader.Business.Managers;
using System.Collections.Generic;

namespace Tests.ImageUploader.Business
{
    [TestClass]
    public class TestImageManager
    {
        /// <summary>
        /// This method will actually test out the insert and retrieve to make sure that the save
        /// works.
        /// </summary>
        [TestMethod]
        public void TestSaveImage()
        {
            ImageManager man = new ImageManager();
            var imageContent = "Corrupt Image";
            var data = new MemoryStream(System.Text.ASCIIEncoding.ASCII.GetBytes(imageContent));
            var imd = new ImageMetaData() { };
            var tags = new List<ImageTag>();
            var success = false;

            imd.FileName = "TestName.jpg";
            imd.ContentType = "test content type";
            man.SaveImage(imd , tags , data);

            foreach(var image in man.RetrieveAll())
            {
                if(image.ID == imd.ID)
                {
                    using (var fs = man.GetImageData(image.ImageGUID).Data)
                    {
                        var text = (new StreamReader(fs)).ReadToEnd();
                        if (imageContent == text)
                            success = true;
                    }

                    break;
                }
            }
            
            //will be set to true if the image was saved correctly.
            Assert.IsTrue(success);

            man.Delete(imd.ImageGUID);

            success = true;
            foreach (var image in man.RetrieveAll())
            {
                if (image.ID == imd.ID)
                {
                    //should not be there any more.
                    success = false;
                    break;
                }
            }

            //will be set to true if the image was deleted correctly.
            Assert.IsTrue(success);
        }
    }
}
