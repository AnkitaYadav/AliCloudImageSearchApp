using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImageSearch;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserInterface.Controllers
{
    [Route("api/[controller]")]
    public class ImageController : Controller
    {
        [HttpGet]
        public ActionResult GetAllImages()
        {
            OSS oSS = new OSS();
            List<string> image = OSS.GetOSSImage();
            return Ok(image);
        }
    }
}
