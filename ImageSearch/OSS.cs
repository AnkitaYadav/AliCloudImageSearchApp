using Aliyun.OSS;
using System;
using System.Collections.Generic;

namespace ImageSearch
{
    public class OSS
    {
        public static List<string> GetOSSImage()
        {
            string ImageUrl = ImageSearchConstant.ImageUrl; ;

            List<string> imageUrls = new List<string>();
            var endpoint = ImageSearchConstant.OSSEndPointName; //"oss-ap-southeast-1.aliyuncs.com";
            var accessKeyId = ImageSearchConstant.AccessKeyId;//"LTAI0YxcZfPRUUjg";
            var accessKeySecret = ImageSearchConstant.Secret;//"2BnIA99n4P5McHbaH5HZ7IrSPCIa4F";
            var bucketName = ImageSearchConstant.BucketName; //"imagesearchbuckettest";
            // Create an OSSClient instance.

            var client = new OssClient(endpoint, accessKeyId, accessKeySecret);
            try
            {
                var listObjectsRequest = new ListObjectsRequest(bucketName);
                listObjectsRequest.MaxKeys = ImageSearchConstant.MaxRecordsCount;
                listObjectsRequest.Prefix = ImageSearchConstant.OSSfolderPrefix;
                // Simply list the objects in a specified bucket. 100 records are returned by default.
                var result = client.ListObjects(listObjectsRequest);
                Console.WriteLine("List objects succeeded");

                foreach (var summary in result.ObjectSummaries)
                {
                    //if(summary.Key.Contains("Images/"))
                    imageUrls.Add(string.Format(ImageUrl, summary.Key));
                    Console.WriteLine("File name:{0}", summary.Key);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("List objects failed. {0}", ex.Message);
            }
            Console.WriteLine("Hello World!");

            return imageUrls;

        }
    }
}
