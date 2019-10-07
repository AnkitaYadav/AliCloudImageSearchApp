using System;
using System.IO;
using Aliyun.Acs.Core;
using Aliyun.Acs.Core.Profile;
using Aliyun.Acs.ImageSearch.Model.V20190325;
using ImageSearch;

namespace Test
{
   public class Demo
    {
        static void Main(string[] args)
        {

            OSS oSS = new OSS();
            var images = OSS.GetOSSImage();

            //IClientProfile profile = DefaultProfile.GetProfile("ap-southeast-1", "LTAI0YxcZfPRUUjg", "2BnIA99n4P5McHbaH5HZ7IrSPCIa4F");
            //DefaultProfile.AddEndpoint("imagesearch.ap-southeast-1.aliyuncs.com", "ap-southeast-1", "ImageSearch", "imagesearch.ap-southeast-1.aliyuncs.com");
            //IAcsClient client = new DefaultAcsClient(profile);
            //// Add an image.
            //AddImageRequest addRequest = new AddImageRequest();
            //addRequest.InstanceName = "teamimagesearch";
            //addRequest.ProductId = "test";
            //addRequest.PicName = "test";
            //byte[] img = System.IO.File.ReadAllBytes("D:/AliCloud/Images/Category/3/HandBag1.jpg");
            //string pic = Convert.ToBase64String(img);
            //addRequest.PicContent = pic;
            ////AddImageResponse addResponse = client.GetAcsResponse(addRequest);
            ////Console.WriteLine(addResponse.RequestId);
            ////Search for an image.
            //SearchImageRequest searchRequest = new SearchImageRequest();


           
            //searchRequest.InstanceName = "teamimagesearch";
            ////searchRequest.Type = "SearchByName";
            ////searchRequest.ProductId = "test";
            ////searchRequest.PicName = "test";
            //searchRequest.PicContent = pic;
            //SearchImageResponse searchResponse = client.GetAcsResponse(searchRequest);

            //Console.WriteLine(searchResponse);
            //Console.WriteLine(searchResponse.RequestId);

            //Console.ReadLine();
            // Delete an image.
            //DeleteImageRequest request = new DeleteImageRequest();
            //deleteRequest.InstanceName = "demo";
            //deleteRequest.ProductId = "test";
            //DeleteImageResponse deleteResponse = client.GetAcsResponse(deleteRequest);
            //Console.WriteLine(deleteResponse.RequestId);
        }
    }
}