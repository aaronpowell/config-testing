using Microsoft.AspNet.Mvc;
using Microsoft.Framework.ConfigurationModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConfigTesting
{
    public class HomeController : Controller
    {
        private readonly IConfiguration configuration;

        public HomeController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IActionResult Index()
        {
            var field = configuration.GetType()
                .GetField("_sources", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            var sources = field.GetValue(configuration) as IList<IConfigurationSource>;

            var values = sources
                .OfType<BaseConfigurationSource>()
                .SelectMany(x => x.Data);

            return View(values);
        }
    }
}