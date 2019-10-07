using Aliyun.Acs.Core;
using Aliyun.Acs.Core.Profile;
using Aliyun.Acs.ImageSearch.Model.V20190325;
using Aliyun.OSS;
using Aliyun.OSS.Common;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace ImageSearch
{
    public class ImageSearchAPI
    {
     
        private StringBuilder metaFile = new StringBuilder();
        public void FilterImage(string filePath)
        {
            IClientProfile profile = DefaultProfile.GetProfile("ap-southeast-1", "LTAI0YxcZfPRUUjg", "2BnIA99n4P5McHbaH5HZ7IrSPCIa4F");
            DefaultProfile.AddEndpoint("imagesearch.ap-southeast-1.aliyuncs.com", "ap-southeast-1", "ImageSearch", "imagesearch.ap-southeast-1.aliyuncs.com");
            IAcsClient client = new DefaultAcsClient(profile);
            // Add an image.
            AddImageRequest addRequest = new AddImageRequest();
            addRequest.InstanceName = "teamimagesearch";
            addRequest.ProductId = "test";
            addRequest.PicName = "test";
            byte[] img = System.IO.File.ReadAllBytes("filePath");
            string pic = Convert.ToBase64String(img);
            addRequest.PicContent = pic;
            //AddImageResponse addResponse = client.GetAcsResponse(addRequest);
            //Console.WriteLine(addResponse.RequestId);
            //Search for an image.
            SearchImageRequest searchRequest = new SearchImageRequest();

            searchRequest.InstanceName = "teamimagesearch";
            //searchRequest.Type = "SearchByName";
            //searchRequest.ProductId = "test";
            //searchRequest.PicName = "test";
            searchRequest.PicContent = pic;
            SearchImageResponse searchResponse = client.GetAcsResponse(searchRequest);

          
            Console.WriteLine(searchResponse);
            Console.WriteLine(searchResponse.RequestId);

            Console.ReadLine();
            // Delete an image.
            //DeleteImageRequest request = new DeleteImageRequest();
            //deleteRequest.InstanceName = "demo";
            //deleteRequest.ProductId = "test";
            //DeleteImageResponse deleteResponse = client.GetAcsResponse(deleteRequest);
            //Console.WriteLine(deleteResponse.RequestId);
        }

        public List<string> FilterImageWithBinaryData(string imagewithBase64Encoded)
        {
            string ImageUrl = "http://imagesearchbuckettest.oss-ap-southeast-1.aliyuncs.com/images/{0}";
            List<string> filteredImages = new List<string>();
            IClientProfile profile = DefaultProfile.GetProfile("ap-southeast-1", "LTAI0YxcZfPRUUjg", "2BnIA99n4P5McHbaH5HZ7IrSPCIa4F");
            DefaultProfile.AddEndpoint("imagesearch.ap-southeast-1.aliyuncs.com", "ap-southeast-1", "ImageSearch", "imagesearch.ap-southeast-1.aliyuncs.com");
            IAcsClient client = new DefaultAcsClient(profile);
            // Add an image.
            AddImageRequest addRequest = new AddImageRequest();
            addRequest.InstanceName = "teamimagesearch";
            addRequest.ProductId = "test";
            addRequest.PicName = "test";
           // byte[] img = System.IO.File.ReadAllBytes("filePath");
            string pic = imagewithBase64Encoded;
            addRequest.PicContent = pic;
            //AddImageResponse addResponse = client.GetAcsResponse(addRequest);
            //Console.WriteLine(addResponse.RequestId);
            //Search for an image.
            SearchImageRequest searchRequest = new SearchImageRequest();



            searchRequest.InstanceName = "teamimagesearch";
            //searchRequest.Type = "SearchByName";
            //searchRequest.ProductId = "test";
            //searchRequest.PicName = "test";
            searchRequest.PicContent = pic;
            SearchImageResponse searchResponse = client.GetAcsResponse(searchRequest);

            Console.WriteLine(searchResponse);
            Console.WriteLine(searchResponse.RequestId);
            foreach (var response in searchResponse.Auctions)
            {
                filteredImages.Add(string.Format(ImageUrl, response.PicName));
            }


           // PutObjectFromString();
            //PutObjectWithDir();
            //PutObjectWithHeader();
            // Delete an image.
            //DeleteImageRequest request = new DeleteImageRequest();
            //deleteRequest.InstanceName = "demo";
            //deleteRequest.ProductId = "test";
            //DeleteImageResponse deleteResponse = client.GetAcsResponse(deleteRequest);
            //Console.WriteLine(deleteResponse.RequestId);
            return filteredImages;
        }

        public void CreateMetaFile(string bucketName, StringBuilder text)
        {
         

            string metaText = Convert.ToString(text);
            string timeStamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            string path = @"D:\AliCloud\MetaDataFile\increment_"+ timeStamp+ ".meta";

            string path1 = @"D:\AliCloud\MetaDataFile\incrementText"+ timeStamp+".text";
            string ossMetaPath = @"D:\AliCloud\MetaDataFile\increment.meta";
            //string path = @"D:\MyTest.txt";
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            if (File.Exists(ossMetaPath))
            {
                File.Delete(path);
            }
            if (File.Exists(path1))
            {
                File.Delete(path1);
            }
            using (FileStream fs = File.Create(path1))
            {
                Byte[] info = new UTF8Encoding(true).GetBytes(metaText);
                // Add some information to the file.
                fs.Write(info, 0, info.Length);
            }
            using (FileStream fs = File.Create(path))
            {
                Byte[] info = new UTF8Encoding(true).GetBytes(metaText);
                // Add some information to the file.
                fs.Write(info, 0, info.Length);
            }
            using (FileStream fs = File.Create(ossMetaPath))
            {
                Byte[] info = new UTF8Encoding(true).GetBytes(metaText);
                // Add some information to the file.
                fs.Write(info, 0, info.Length);
            }

            string fileName = Path.GetFileName(ossMetaPath);
            string key = string.Format("Test/{0}", fileName);
            UploadFileInOss(bucketName, ossMetaPath, key);
        }

        public  void FindFiles(string path)
        {
            foreach (var item in Directory.GetFiles(path))
            {
                Console.WriteLine(item);
            }

            foreach (var item in Directory.GetDirectories(path))
            {
                FindFiles(item);
            }

        }
        public static void PutObjectFromString(string bucketName= "imagesearchbuckettest")
        {
            const string key = "PutObjectFromString";
            const string str = "Aliyun OSS SDK for C#";

            try
            {
                 OssClient client = new OssClient("oss-ap-southeast-1.aliyuncs.com", "LTAI0YxcZfPRUUjg", "2BnIA99n4P5McHbaH5HZ7IrSPCIa4F");
                byte[] binaryData = Encoding.ASCII.GetBytes(str);
                var stream = new MemoryStream(binaryData);

                client.PutObject(bucketName, key, stream);
                Console.WriteLine("Put object:{0} succeeded", key);
            }
            catch (OssException ex)
            {
                Console.WriteLine("Failed with error code: {0}; Error info: {1}. \nRequestID:{2}\tHostID:{3}",
                    ex.ErrorCode, ex.Message, ex.RequestId, ex.HostId);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed with error info: {0}", ex.Message);
            }
        }

        public  void PutObjectWithDir(string categoryId= "88888888")
        {
            string bucketName = "imagesearchbuckettest";
            StringBuilder metaFileText = new StringBuilder();
            foreach (var file in Directory.GetFiles(@"D:\AliCloud\Images\Bags"))
            {
                Console.WriteLine(file);
                string fileName = Path.GetFileName(file);
                string key = string.Format("Images/{0}", fileName);

                CreateMetaDataTxt(metaFileText, fileName, categoryId);
                UploadFileInOss(bucketName, file, key);
            }
            CreateMetaFile(bucketName,metaFileText);
        }

        public void PutObjectWithUrls(IList<IFormFile> formFiles, string categoryId)
        {

            string bucketName = "imagesearchbuckettest";
            StringBuilder metaFileText = new StringBuilder();
            var temppath = Path.GetTempPath();
            foreach (var file in formFiles)
            {
                var fileToUpload = string.Format("{0}{1}", temppath, file.FileName);

                var imagePath = string.Format("{0}{1}", "D:/", file.FileName);
                Console.WriteLine(file);
                string fileName = file.FileName; //Path.GetFileName(file);
                string key = string.Format("Test/{0}", fileName);

                Image image = Image.FromStream(file.OpenReadStream(), true, true);
                ResizeAndSaveImage(fileToUpload, image);

                CreateMetaDataTxt(metaFileText, fileName, categoryId);

                UploadFileInOss(bucketName, fileToUpload, key);


                File.Delete(fileToUpload);
            }
            CreateMetaFile(bucketName,metaFileText);
        }

        private static void ResizeAndSaveImage(string fileToUpload, Image image)
        {
            int newWidth = 0;
            int newHeight = 0;

            bool isWidthUpdated = false;
            bool isHeightUpdated = false;
            var imageWidth = image.Size.Width;
            var imageHeight = image.Size.Height;

            if (imageWidth < 250)
            {
                newWidth = 250;
                isWidthUpdated = true;
            }
            else if (imageWidth > 1250)
            {
                newWidth = 1250;
                isWidthUpdated = true;
            }
            else
            {
                newWidth = imageWidth;
                isWidthUpdated = false;
            }

            if (imageHeight < 250)
            {
                newHeight = 250;
                isHeightUpdated = true;
            }
            else if (imageHeight > 1250)
            {
                newHeight = 1250;
                isHeightUpdated = true;
            }
            else
            {
                newHeight = imageHeight;
                isHeightUpdated = false;
            }

            if (isWidthUpdated || isHeightUpdated)
            {
                var newImage = ResizeImage(image,newWidth, newHeight);
                newImage.Save(fileToUpload);
            }

            else
            {
                image.Save(fileToUpload);
            }
        }

        private  void UploadFileInOss(string bucketName, string file, string key)
        {
            try
            {
                OssClient client = new OssClient("oss-ap-southeast-1.aliyuncs.com", "LTAI0YxcZfPRUUjg", "2BnIA99n4P5McHbaH5HZ7IrSPCIa4F");
                client.PutObject(bucketName, key, file);
            
                client.SetObjectAcl(bucketName, key, CannedAccessControlList.PublicReadWrite);
                Console.WriteLine("Put object:{0} succeeded", key);
            }
            catch (OssException ex)
            {
                Console.WriteLine("Failed with error code: {0}; Error info: {1}. \nRequestID:{2}\tHostID:{3}",
                    ex.ErrorCode, ex.Message, ex.RequestId, ex.HostId);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed with error info: {0}", ex.Message);
            }
        }

        private static void CreateMetaDataTxt(StringBuilder metaFileText, string fileName, string categoryId)
        {
            IncrementMetaFile incrementMetaFile = new IncrementMetaFile()
            {
                item_id = 100 + new Random().Next(1, 100000).ToString(),
                cust_content = "k1:v1,k2:v2,k3:v3",
                Operator = "ADD",
                pic_list = new List<string> { fileName }.ToArray(),
                cat_id =Convert.ToInt32(categoryId)

            };
            string jsonIncementextFile = JsonConvert.SerializeObject(incrementMetaFile);
            metaFileText.Append(jsonIncementextFile);
        }

      
        public void PutObjectWithHeader(string bucketName= "imagesearchbuckettest")
        {
            const string key = "oss-ap-southeast-1.aliyuncs.com/imagesearchbuckettest/newfolder";
            string fileToUpload = @"D:/AliCloud/Images/Dresses/TestDress.JPG";
            try
            {
                OssClient client = new OssClient("oss-ap-southeast-1.aliyuncs.com", "LTAI0YxcZfPRUUjg", "2BnIA99n4P5McHbaH5HZ7IrSPCIa4F");
                using (var content = File.Open(fileToUpload, FileMode.Open))
                {
                    var metadata = new ObjectMetadata();

                    metadata.ContentLength = content.Length;
                    metadata.ContentType = "image/jpeg";

                    //metadata.UserMetadata.Add("github-account", "qiyuewuyi");

                    client.PutObject(bucketName, key, content, metadata);

                    Console.WriteLine("Put object:{0} succeeded", key);
                }
            }
            catch (OssException ex)
            {
                Console.WriteLine("Failed with error code: {0}; Error info: {1}. \nRequestID:{2}\tHostID:{3}",
                    ex.ErrorCode, ex.Message, ex.RequestId, ex.HostId);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed with error info: {0}", ex.Message);
            }
        }

        public static void PutLocalFileBySignedUrl(string bucketName, string key,string fileToUpload)
        {
            OssClient client = new OssClient("oss-ap-southeast-1.aliyuncs.com", "LTAI0YxcZfPRUUjg", "2BnIA99n4P5McHbaH5HZ7IrSPCIa4F");

            try
            {
                // Step1: Genereates url signature
                var process = "image/resize,m_fixed,w_100,h_100";
                var request = new GeneratePresignedUriRequest(bucketName, key, SignHttpMethod.Put);
                var req = new GeneratePresignedUriRequest(bucketName, key, SignHttpMethod.Post)
                {
                    Process = process
                };
                //Dictionary<string, string> StyleParams = new Dictionary<string, string>();
                //StyleParams.Add(, );
                //request.QueryParams = StyleParams;
                var signedUrl = client.GeneratePresignedUri(request);

                // Step2: Prepares for filepath to be uploaded and sends out this request.
                client.PutObject(signedUrl, fileToUpload);

                Console.WriteLine("Put object by signatrue succeeded.");
            }
            catch (OssException ex)
            {
                Console.WriteLine("Failed with error code: {0}; Error info: {1}. \nRequestID:{2}\tHostID:{3}",
                    ex.ErrorCode, ex.Message, ex.RequestId, ex.HostId);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed with error info: {0}", ex.Message);
            }
        }

        public static System.Drawing.Image ScaleImage(System.Drawing.Image image, int maxHeight)
        {
            var ratio = (double)maxHeight / image.Height;
            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);
            var newImage = new Bitmap(newWidth, newHeight);
            using (var g = Graphics.FromImage(newImage))
            {
                g.DrawImage(image, 0, 0, newWidth, newHeight);
            }
            return newImage;
        }
        public static Image Resize(Image image, int newWidth, int maxHeight, bool onlyResizeIfWider)
        {
            if (onlyResizeIfWider && image.Width <= newWidth) newWidth = image.Width;

            var newHeight = image.Height * newWidth / image.Width;
            if (newHeight > maxHeight)
            {
                // Resize with height instead  
                newWidth = image.Width * maxHeight / image.Height;
                newHeight = maxHeight;
            }

            var res = new Bitmap(newWidth, newHeight);



            return res;
        }

        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
    }
}
