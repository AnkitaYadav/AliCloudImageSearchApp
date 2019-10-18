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
        public List<string> FilterImageWithBinaryData(string imagewithBase64Encoded)
        {
            string ImageUrl = ImageSearchConstant.ImageSearchUrl;
            List<string> filteredImages = new List<string>();
            IClientProfile profile = DefaultProfile.GetProfile(ImageSearchConstant.ImageSearchRegionId, ImageSearchConstant.AccessKeyId, ImageSearchConstant.Secret);
            DefaultProfile.AddEndpoint(ImageSearchConstant.ImageSearchEndpoint, ImageSearchConstant.ImageSearchRegionId, ImageSearchConstant.ImageSearchProductName, ImageSearchConstant.ImageSearchEndpoint);
            IAcsClient client = new DefaultAcsClient(profile);
           
            string pic = string.Empty;
            byte[] image = Convert.FromBase64String(imagewithBase64Encoded);
            using (var ms = new MemoryStream(image))
            {
                Image img = Image.FromStream(ms);
                pic = ResizeFilterImage(img);
            }
            
            SearchImageRequest searchRequest = new SearchImageRequest();
            searchRequest.InstanceName = ImageSearchConstant.InstanceName;
            searchRequest.PicContent = pic;
            searchRequest.Num = 100;

            SearchImageResponse searchResponse = client.GetAcsResponse(searchRequest);
            Console.WriteLine(searchResponse);
            Console.WriteLine(searchResponse.RequestId);
            foreach (var response in searchResponse.Auctions)
            {
                filteredImages.Add(string.Format(ImageUrl, response.PicName));
            }

            return filteredImages;
        }
        public void CreateMetaFile(string bucketName, StringBuilder text)
        {
         
            string metaText = Convert.ToString(text);
            string timeStamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            string ossMetaTextPath =string.Format(ImageSearchConstant.OssMetaTextFilePath,timeStamp);
            string ossMetaPath = ImageSearchConstant.OssMetaFilePath;
         
            if (File.Exists(ossMetaPath))
            {
                File.Delete(ossMetaPath);
            }
            if (File.Exists(ossMetaTextPath))
            {
                File.Delete(ossMetaTextPath);
            }
            using (FileStream fs = File.Create(ossMetaTextPath))
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
            string key = string.Format(ImageSearchConstant.OSSImageFolderPath, fileName);
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
        public void PutObjectWithUrls(IList<IFormFile> formFiles, string categoryId)
        {

            string bucketName = ImageSearchConstant.BucketName;
            StringBuilder metaFileText = new StringBuilder();
            var temppath = Path.GetTempPath();
            List<IncrementMetaFile> metafileTextData = new List<IncrementMetaFile>();
            foreach (var file in formFiles)
            {
                var fileToUpload = string.Format("{0}{1}", temppath, file.FileName);

                var imagePath = string.Format("{0}{1}", "D:/", file.FileName);
                Console.WriteLine(file);
                string fileName = file.FileName; //Path.GetFileName(file);
                string key = string.Format(ImageSearchConstant.OSSImageFolderPath, fileName);

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
        private static string ResizeFilterImage( Image image)
        {

            string encodedImageData = string.Empty;
            Image newImage = image;
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
                newImage = ResizeImage(image, newWidth, newHeight);
            }
            using (MemoryStream ms = new MemoryStream())
            {
                // Convert Image to byte[]
                newImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] imageBytes = ms.ToArray();

                // Convert byte[] to Base64 String
                encodedImageData = Convert.ToBase64String(imageBytes);
               
            }
           
            return encodedImageData;

        }
        private  void UploadFileInOss(string bucketName, string file, string key)
        {
            try
            {
                OssClient client = new OssClient(ImageSearchConstant.OSSEndPointName, ImageSearchConstant.AccessKeyId, ImageSearchConstant.Secret);
   
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
                Operator = "ADD",
                item_id = new Random().Next(1, 100000).ToString(),
                cat_id = Convert.ToInt32(categoryId),
                cust_content = "k1:v1,k2:v2,k3:v3",
                pic_list = new List<string> { fileName }.ToArray()

            };

            string jsonIncementextFile = JsonConvert.SerializeObject(incrementMetaFile);

            string modify = jsonIncementextFile.Insert(jsonIncementextFile.IndexOf("cat_id") - 1," ");
            string modify1 = modify.Insert(modify.IndexOf("cust_content") - 1, " ");
            string finalString = modify1.Insert(modify1.IndexOf("pic_list") - 1, " ")+Environment.NewLine;
            metaFileText.Append(finalString);
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
