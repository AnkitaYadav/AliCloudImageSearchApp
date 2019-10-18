using System;
using System.Collections.Generic;
using System.Text;

namespace ImageSearch
{
   static class ImageSearchConstant
   {
        //Common Attributes 
        public const string AccessKeyId = "LTAI0YxcZfPRUUjg";
        public const string Secret = "2BnIA99n4P5McHbaH5HZ7IrSPCIa4F";

        // OSS Related Attribute
        public const string RegionId = "oss-eu-central-1";
        public const string OSSEndPointName = "oss-eu-central-1.aliyuncs.com";
        public const string Product = "imagesearchbucketgeremanyregion";
        public const string Domain = "imagesearchbucketgeremanyregion.oss-eu-central-1.aliyuncs.com";
        public const string BucketName = "imagesearchbucketgeremanyregion";
        public const string OSSfolderPrefix = "ImageSearch/";
        public const int MaxRecordsCount = 1000;
        public const string ImageUrl = "http://imagesearchbucketgeremanyregion.oss-eu-central-1.aliyuncs.com/{0}";
        public const string OSSImageFolderPath = "ImageSearch/{0}";//OSS Folder Path where need to create Image


        // Local path for 
        public const string OssMetaTextFilePath = @"D:\AliCloud\MetaDataFile\incrementText{0}.text";
        public const string OssMetaFilePath =@"D:\AliCloud\MetaDataFile\increment.meta";

        //Image Search console Info
        public const string ImageSearchProductName = "ImageSearch";
        public const string ImageSearchRegionId = "eu-central-1";
        public const string ImageSearchEndpoint = "imagesearch.eu-central-1.aliyuncs.com";
        public const string InstanceName = "imagesearchdemo";
        public const string ImageSearchUrl = "http://imagesearchbucketgeremanyregion.oss-eu-central-1.aliyuncs.com/ImageSearch/{0}";
    }
}
