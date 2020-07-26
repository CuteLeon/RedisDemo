using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace RedisDemo.RedisExtension
{
    public static class StackExchangeRedisExtension
    {
        private static IReadOnlyDictionary<string, IDatabase> redisDatabases;
        private static object lockSeed = new object();

        /// <summary>
        /// Asp.Net Core 的默认DI容器无法设置别名，使用AddSingleton的重载方法Func来实现别名功能
        /// </summary>
        public static void AddStackExchangeRedis(
            this IServiceCollection services,
            Func<ConfigurationOptions> configFunc,
            IDictionary<int, string> databaseNames = null)
        {
            if (redisDatabases != null) throw new InvalidOperationException("Redis databases have been initialzed.");
            lock (lockSeed)
            {
                if (redisDatabases != null) throw new InvalidOperationException("Redis databases have been initialzed.");

                var options = configFunc?.Invoke() ??
                    throw new ArgumentNullException(nameof(configFunc));

                var redis = ConnectionMultiplexer.Connect(options);
                services.AddSingleton<IConnectionMultiplexer>(redis);

                if (databaseNames?.Count > 0)
                {
                    IDictionary<string, IDatabase> databases = databaseNames
                        .Select(pair => (pair.Value, redis.GetDatabase(pair.Key)))
                        .ToDictionary(tuple => tuple.Item1, tuple => tuple.Item2);
                    redisDatabases = new ReadOnlyDictionary<string, IDatabase>(databases);

                    services.AddSingleton(provider => new Func<string, IDatabase>(name => redisDatabases.GetValueOrDefault(name)));
                }
                else
                {
                    var database = redis.GetDatabase();
                    services.AddSingleton(database);
                }
            }
        }
    }
}
