using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using UsingMultipleImplementationsOfAnInterface.Enums;
using UsingMultipleImplementationsOfAnInterface.Models;
using UsingMultipleImplementationsOfAnInterface.Services;

namespace UsingMultipleImplementationsOfAnInterface.Controllers
{

    public delegate ICustomLogger ServiceResolver(ServiceType serviceType);

    public class HomeController : Controller
    {
        private readonly ICustomLogger _filLlogger;
        private readonly ICustomLogger _dbLogger;
        private readonly ICustomLogger _eventLogger;              

        //Use an IEnumerable collection of service instances
        /*public HomeController(IEnumerable<ICustomLogger> loggers)
        {
            int i = 0;
            var logs = new ICustomLogger[3];
            foreach(var logger in loggers)
            {
                logs[i] = logger;
                i++;
            }
            _filLlogger = logs[0];            
            _eventLogger = logs[1];
            _dbLogger = logs[2];
        }*/

        //Use a delegate to retrieve a specific service instance
        public HomeController(ServiceResolver serviceResolver)
        {            
            _filLlogger = serviceResolver(ServiceType.FileLogger);
            _eventLogger = serviceResolver(ServiceType.EventLogger);
            _dbLogger = serviceResolver(ServiceType.DbLogger);
        }

        //Another possible solution is using a generic type parameter on the interface.

        public IActionResult Index()
        {
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
