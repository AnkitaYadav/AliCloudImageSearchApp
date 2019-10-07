using Aliyun.OSS;
using System;
using System.Collections.Generic;

namespace ImageSearch
{
    public class OSS
    {
        public static List<string> GetOSSImage()
        {
            string ImageUrl = "http://imagesearchbuckettest.oss-ap-southeast-1.aliyuncs.com/{0}";

            List<string> imageUrls = new List<string>();
            var endpoint = "oss-ap-southeast-1.aliyuncs.com";
            var accessKeyId = "LTAI0YxcZfPRUUjg";
            var accessKeySecret = "2BnIA99n4P5McHbaH5HZ7IrSPCIa4F";
            var bucketName = "imagesearchbuckettest";
            // Create an OSSClient instance.

            var client = new OssClient(endpoint, accessKeyId, accessKeySecret);
            try
            {
                var listObjectsRequest = new ListObjectsRequest(bucketName);
                listObjectsRequest.MaxKeys = 1000;
                listObjectsRequest.Prefix = "Images/";
                // Simply list the objects in a specified bucket. 100 records are returned by default.
                var result = client.ListObjects(listObjectsRequest);
                Console.WriteLine("List objects succeeded");

                foreach (var summary in result.ObjectSummaries)
                {
                    if(summary.Key.Contains("Images/"))
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
