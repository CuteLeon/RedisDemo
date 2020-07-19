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

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet("{controller}/GetAsync")]
        public async Task<byte[]> GetAsync(string key)
        {
            this._logger.LogDebug($"Get Redis key={key}...");
            return await cache.GetAsync(key);
        }

        [HttpGet("{controller}/GetStringAsync")]
        public async Task<string> GetStringAsync(string key)
        {
            this._logger.LogDebug($"Get Redis String key={key}...");
            return await cache.GetStringAsync(key);
        }

        [HttpGet("{controller}/RefreshAsync")]
        public async Task<IActionResult> RefreshAsync(string key)
        {
            this._logger.LogDebug($"Refresh Redis key={key}...");
            await cache.RefreshAsync(key);
            return this.Ok($"Key = {key} has been refreshed.");
        }

        [HttpGet("{controller}/RemoveAsync")]
        public async Task<IActionResult> RemoveAsync(string key)
        {
            this._logger.LogDebug($"Remove Redis key={key}...");
            await cache.RemoveAsync(key);
            return this.Ok($"Key = {key} has been removed.");
        }

        // TODO: 未能使用 Postman Post byte[] 类型的数据，因此暂时使用Base64编码传输
        [HttpPost("{controller}/SetAsync")]
        public async Task<IActionResult> SetAsync([FromForm] string key, [FromForm] string value)
        {
            this._logger.LogDebug($"Set Redis key={key} as value={value?.Length}bytes...");
            await cache.SetAsync(key, Convert.FromBase64String(value));
            return this.Ok($"Key = {key} has been setted.");
        }

        [HttpPost("{controller}/SetStringAsync")]
        public async Task<IActionResult> SetStringAsync(string key, string value)
        {
            this._logger.LogDebug($"Set Redis String key={key} as value=\"{value}\"");
            await cache.SetStringAsync(key, value);
            return this.Ok($"Key = {key} has been setted.");
        }
    }
}
