using System;
using System.Collections.Generic;
using System.Text;

namespace ImageSearch
{
   static class ImageSearchConstant
   {
        //ImageSearch credentail and configuration settings
        //public const string RegionId = "ap-southeast-1";
        //public const string AccessKeyId = "LTAI0YxcZfPRUUjg";
        //public const string Secret= "2BnIA99n4P5McHbaH5HZ7IrSPCIa4F";
        //public const string EndPointName = "imagesearch.ap-southeast-1.aliyuncs.com";
        //public const string Product = "ImageSearch";
        //public const string Domain = "imagesearch.ap-southeast-1.aliyuncs.com";

        public const string RegionId = "oss-eu-central-1";
        public const string AccessKeyId = "LTAI0YxcZfPRUUjg";
        public const string Secret = "2BnIA99n4P5McHbaH5HZ7IrSPCIa4F";
        public const string EndPointName = "oss-eu-central-1.aliyuncs.com";
        public const string Product = "imagesearchbucketgeremanyregion";
        public const string Domain = "imagesearchbucketgeremanyregion.oss-eu-central-1.aliyuncs.com";
        public const string BucketName = "imagesearchbucketgeremanyregion";
        // Local path for 
        public const string OssMetaTextFilePath = @"D:\AliCloud\MetaDataFile\incrementText{0}.text";
        public const string OssMetaFilePath =@"D:\AliCloud\MetaDataFile\increment.meta";

        //Image Oss Bucket URL

        public const string ImageUrl = "http://imagesearchbucketgeremanyregion.oss-eu-central-1.aliyuncs.com/{0}";

        //OSS Folder Path where need to create Image

        public const string OSSImageFolderPath = "ImageSearch/{0}";

        public const string text = "{{\"operator\":\"ADD\",\"item_id\":\"{0}\", \"cat_id\":{1}, \"cust_content\":\"k1:v1,k2:v2,k3:v3\", \"pic_list\":[\"{2}\"]}}";
        public const string OssMetaTextString = "{\"operator\":\"ADD\",\"item_id\":\"{0}\", \"cat_id\":{1}, \"cust_content\":\"k1:v1,k2:v2,k3:v3\", \"pic_list\":[\"{2}\"]}";


        //Image Search console Info


        public const string InstanceName = "imagesearchdemo";
        public const string ImageSearchUrl = "http://imagesearchbucketgeremanyregion.oss-eu-central-1.aliyuncs.com/ImageSearch/{0}";
    }
}
