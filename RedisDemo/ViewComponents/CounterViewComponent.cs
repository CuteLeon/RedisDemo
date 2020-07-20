using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;
using Microsoft.AspNetCore.Server.Kestrel.Core.Features;
using RedisDemo.ViewModels;

namespace RedisDemo.ViewComponents
{
    public class CounterViewComponent : ViewComponent
    {
        private readonly IDatabase database;

        public CounterViewComponent(IDatabase database)
        {
            this.database = database;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var connectId = this.HttpContext.Connection.Id;
            var streamId = this.HttpContext.Features.Get<IHttp2StreamIdFeature>().StreamId;

            await database.StringSetAsync(connectId, 0, TimeSpan.FromMinutes(10), When.NotExists);
            await database.StringIncrementAsync(connectId);
            var count = int.Parse(await database.StringGetAsync(connectId));

            return this.View(new CounterViewModel(connectId, streamId, count));
        }
    }
}
