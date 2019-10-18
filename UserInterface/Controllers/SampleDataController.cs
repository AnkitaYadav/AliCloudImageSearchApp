using System.Collections.Generic;
using System.IO;
using ImageSearch;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace UserInterface.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {

        [HttpGet("[action]")]
        public ActionResult GetOssImages()
        {
            OSS oSS = new OSS();
            List<string> image = OSS.GetOSSImage();
            return Ok(image);
        }

        [HttpPost("[action]")]
        [Route("filterImage")]
        public ActionResult FilterImage(string imageString)
        {
            ImageSearchAPI imageSearch = new ImageSearchAPI();
            var images = imageSearch.FilterImageWithBinaryData(imageString);
            return Ok(images);
        }
       
        [HttpPost]
        [Route("uploadfile/{catrgoryId}")]
        public ActionResult UploadFile(IList<IFormFile> images,string catrgoryId = "88888888")

        {
            ImageSearchAPI imageSearchAPI = new ImageSearchAPI();
            imageSearchAPI.PutObjectWithUrls(images, catrgoryId);
            return Ok("File Uploaded Succesfully");
        }

    

    }

}
    
