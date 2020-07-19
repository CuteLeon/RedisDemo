using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace RedisDemo.Controllers
{
    public class RedisStackExchangeController : Controller
    {
        private readonly ILogger<RedisStackExchangeController> _logger;
        private readonly IDatabase database;

        public RedisStackExchangeController(
            ILogger<RedisStackExchangeController> logger,
            IDatabase database)
        {
            _logger = logger;
            this.database = database;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }


        [HttpGet("{controller}/KeyExistsAsync")]
        public async Task<IActionResult> KeyExistsAsync(string key)
        {
            this._logger.LogDebug($"Is Redis key={key} exists...");
            var result = await database.KeyExistsAsync(key);
            return this.Ok(result);
        }

        [HttpGet("{controller}/GetAsync")]
        public async Task<string> GetAsync(string key)
        {
            this._logger.LogDebug($"Get Redis key={key}...");
            var result = await database.StringGetAsync(key);
            return result;
        }

        [HttpGet("{controller}/KeyDeleteAsync")]
        public async Task<IActionResult> KeyDeleteAsync(string key)
        {
            this._logger.LogDebug($"Delete Redis key={key}...");
            var result = await database.KeyDeleteAsync(key);
            if (result)
                return this.Ok($"Key = {key} has been deleted.");
            else
                return this.BadRequest($"Delete Key = {key} failed.");
        }

        [HttpPost("{controller}/StringSetAsync")]
        public async Task<IActionResult> StringSetAsync([FromForm] string key, [FromForm] string value)
        {
            this._logger.LogDebug($"Set Redis key={key} as value={value}...");
            var result = await database.StringSetAsync(key, value);
            if (result)
                return this.Ok($"Key = {key} has been setted.");
            else
                return this.BadRequest($"Set Key = {key} failed.");
        }

        [HttpGet("{controller}/StringIncrementAsync")]
        public async Task<IActionResult> StringIncrementAsync(string key)
        {
            this._logger.LogDebug($"Increment Redis String key={key}...");
            var result = await database.StringIncrementAsync(key);
            return this.Ok($"Key = {key} has been increment to {result}.");
        }

        [HttpGet("{controller}/StringDecrementAsync")]
        public async Task<IActionResult> StringDecrementAsync(string key)
        {
            this._logger.LogDebug($"Decrement Redis String key={key}...");
            var result = await database.StringDecrementAsync(key);
            return this.Ok($"Key = {key} has been decrement to {result}.");
        }
    }
}
