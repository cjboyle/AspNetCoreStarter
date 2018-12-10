using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreStarter.Infrastructure
{
    public static class ControllerExtenstions
    {
        public static JsonResult RedirectToActionJson<TController>(this TController controller, string action)
            where TController : Controller
        {
            return controller.Json(new
            {
                redirect = controller.Url.Action(action)
            });
        }
    }
}
