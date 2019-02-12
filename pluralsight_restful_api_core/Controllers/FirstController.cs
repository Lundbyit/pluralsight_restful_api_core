using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pluralsight_restful_api_core.Controllers
{
    [Route("api/test")]
    public class FirstController : Controller
    {

        public IActionResult Index()
        {
            return Ok("funkar");
        }
    }
}
