using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Razor;

namespace AspNetCoreStarter.Infrastructure.StartupUtils
{
    public class FeatureViewLocationExpander : IViewLocationExpander
    {
        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (viewLocations == null)
                throw new ArgumentNullException(nameof(viewLocations));

            var descriptor = context.ActionContext.ActionDescriptor as ControllerActionDescriptor;
            string featureName = descriptor?.Properties["feature"] as string;
            foreach (var location in viewLocations)
            {
                yield return location.Replace("{3}", featureName);
            }
        }

        public void PopulateValues(ViewLocationExpanderContext context)
        {
            // see: https://stackoverflow.com/questions/36802661/what-is-iviewlocationexpander-populatevalues-for-in-asp-net-core-mvc
            context.Values["action_displayname"] = context.ActionContext.ActionDescriptor.DisplayName;
        }
    }
}