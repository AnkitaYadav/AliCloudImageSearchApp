using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImageSearch;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ImageSearchAPI.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowAtUIOrigin")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        // GET api/values
        [HttpGet]
        public ActionResult GetAllImages()
        {
            OSS oSS = new OSS();
            List<string> image  = OSS.GetOSSImage();
            return Ok(image);
        }

       
    }
}
