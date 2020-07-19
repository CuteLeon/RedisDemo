using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace RedisDemo.Controllers
{
    public class RedisStackExchangeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
