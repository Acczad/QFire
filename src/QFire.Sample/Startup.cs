using QFire.InversionOfControl;
using QFire.MessageBroker.RabbitMq;
using QFire.MessageBroker.RabbitMq.Contract;

namespace QFire.WebAPI;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
    }

    public IServiceProvider ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddSingleton<IQFireRabbitMqFactory, QFireRabbitMqFactory>();

        services
            .ConfigureQFire<TestMessage>(typeof(RabbitMqMessageBroker),
                option => option.WithRedis(
                option =>
                option.SetMaxWorkerThread(10)
                .SetCacheAbbrivation("tst")
                .SetMaxDeleyInWorkerCreation(4)
                .SetMaxWorkerRetryBeforeTerminate(3)
                .SetMaxMessageCacheTime(24*36000)
                .SetRedisCnnString("127.0.0.1")
                .SetRabbitMqHost("127.0.0.1")
                .SetRabbitMqPassword("password")
                .SetRabbitMqUserName("username")
                ));


        return services.BuildServiceProvider();
    }

    public void Configure(IApplicationBuilder app)
    {
        app.UseRouting();
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}