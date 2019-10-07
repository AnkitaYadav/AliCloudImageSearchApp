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
        private static string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [HttpGet("[action]")]
        public ActionResult WeatherForecasts()
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


        [HttpGet]
        public ActionResult CreateMetaFile()
        {
            ImageSearchAPI imageSearch = new ImageSearchAPI();
            return Ok();
        }
     
        public class WeatherForecast
        {
            public string DateFormatted { get; set; }
            public int TemperatureC { get; set; }
            public string Summary { get; set; }

            public int TemperatureF
            {
                get
                {
                    return 32 + (int)(TemperatureC / 0.5556);
                }
            }
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
    
