using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DynamicDI.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace DynamicDI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEnumerable<IService> _services;
        private readonly HomeOptions _options;

        public HomeController(ILogger<HomeController> logger, IEnumerable<IService> services,
            IOptionsMonitor<HomeOptions> optionsAccessor)
        {
            _logger = logger;
            _services = services;
            _options = optionsAccessor.CurrentValue;
        }

        public IActionResult Index()
        {
            var service = _services.First(s => s.GetType().Name == _options.ServiceMode);
            var message = service.GetMessage(_options.Name);
            ViewData["Message"] = message;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
