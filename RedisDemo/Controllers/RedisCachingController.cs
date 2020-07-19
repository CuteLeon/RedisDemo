using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;

namespace RedisDemo.Controllers
{
    public class RedisCachingController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDistributedCache cache;

        public RedisCachingController(
            ILogger<HomeController> logger,
            IDistributedCache cache)
        {
            _logger = logger;
            this.cache = cache;
        }

        public IActionResult Index()
        {
            //cache.GetAsync();
            //cache.GetStringAsync();
            //cache.RefreshAsync();
            //cache.RemoveAsync();
            //cache.SetAsync();
            //cache.SetStringAsync();
            return View();
        }
    }
}
