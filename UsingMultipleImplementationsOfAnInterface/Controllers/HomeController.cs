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
        private readonly IEnumerable<ICustomLogger> _loggers;

        private readonly ICustomLogger _filLlogger;
        private readonly ICustomLogger _dbLogger;
        private readonly ICustomLogger _eventLogger;              

        //Use an IEnumerable collection of service instances
        public HomeController(IEnumerable<ICustomLogger> loggers)
        {
            _loggers = loggers;   //Then we can use foreach logger in _loggers to call methods in each of the logger service...
            
            _filLlogger = _loggers.Where(l => l.GetType() == typeof(FileLogger)).SingleOrDefault();     
            _eventLogger = _loggers.Where(l => l.GetType() == typeof(EventLogger)).SingleOrDefault();
            _dbLogger = _loggers.Where(l => l.GetType() == typeof(DbLogger)).SingleOrDefault();
        }

        //Use a delegate to retrieve a specific service instance
        /*public HomeController(ServiceResolver serviceResolver)
        {            
            _filLlogger = serviceResolver(ServiceType.FileLogger);
            _eventLogger = serviceResolver(ServiceType.EventLogger);
            _dbLogger = serviceResolver(ServiceType.DbLogger);
        }*/

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
