using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreStarter.Features.Default
{
    // Omit RouteAttribute to allow base url redirect
    public class DefaultController : Controller
    {
        // GET: Default/[Index]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
