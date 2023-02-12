using QFire.InversionOfControl;
using QFire.MessageBroker.RabbitMq;

namespace QFire.WebAPI;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
    }

    public IServiceProvider ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();

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
                ));

        return services.BuildServiceProvider();
    }

    public void Configure(IApplicationBuilder app)
    {
        app.UseRouting();
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}