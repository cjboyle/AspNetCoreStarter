using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreStarter.Features.Actor
{
    public class ActorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}