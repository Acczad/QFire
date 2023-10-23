using System;
using Microsoft.Extensions.DependencyInjection;
using QFire.Abstraction.Analyzer;
using QFire.Abstraction.Caching;
using QFire.Abstraction.Configuration;
using QFire.Abstraction.Core;
using QFire.Abstraction.Message;
using QFire.Abstraction.MessageBroker;
using QFire.Abstraction.MessageRepository;
using QFire.Abstraction.Serialization;
using QFire.Caching.Redis;
using QFire.Core.Analyzer;
using QFire.Core.Worker;
using QFire.Core;
using QFire.MessageRepository;
using QFire.Serialization;
using QFire.Core.Queue;

namespace QFire.InversionOfControl
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureQFire<T>(this IServiceCollection services
             , Type rabbitMqImplementation
             , Action<QFireConfiguration> configActions
             , Type qFireRedisCacheProvider = null) where T : QFireMessage
        {
            
                var configurations = new QFireConfiguration();
                configActions(configurations);

                services.AddSingleton(configurations);
                services.AddSingleton(typeof(IQFireAnalyzer<T>), typeof(QFireAnalyzer<T>));
                services.AddSingleton(typeof(IQFireRepository<T>), typeof(QFireMessageRepository<T>));
                services.AddSingleton(typeof(IQFireInMemoryRepository<T>), typeof(QFireInMemoryMessageRepository<T>));
                services.AddSingleton(typeof(IQFireInRedisRepository<T>), typeof(QFireRedisMessageRepository<T>));
                
                if (qFireRedisCacheProvider==null)
                    services.AddSingleton(typeof(IQFireCache), typeof(QFireRedisCacheProvider));
                else
                    services.AddSingleton(typeof(IQFireCache), qFireRedisCacheProvider);

                services.AddSingleton(typeof(IQFire<T>), typeof(QFire<T>));
                services.AddSingleton(typeof(IMessageKeyGenerator), typeof(MessageKeyGenerator));
                services.AddSingleton(typeof(IMessagePackSerializer), typeof(QFireMessagePackSerializer));
                services.AddTransient(typeof(IQFireBaseMessageBroker), rabbitMqImplementation);
                services.AddTransient(typeof(IQFireWorkerService), typeof(QFireWorkerService<T>));
                services.AddHostedService<QFireBackgroundMessageLoaderService<T>>();
                services.AddHostedService<QFireBackgroundCoordinatorService<T>>();

                return services;
            }
        }
    }


