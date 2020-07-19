using System;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace RedisDemo.RedisExtension
{
    public static class StackExchangeRedisExtension
    {
        public static void AddStackExchangeRedis(this IServiceCollection services, Func<ConfigurationOptions> configFunc)
        {
            var options = configFunc?.Invoke() ??
                throw new ArgumentNullException(nameof(configFunc));
            var redis = ConnectionMultiplexer.Connect(options);
            var database = redis.GetDatabase();
            services.AddSingleton<IDatabase>(database);
        }
    }
}
