using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.Hosting;
using QFire.Abstraction.Analyzer;
using QFire.Abstraction.Configuration;
using QFire.Abstraction.Message;
using System;
using QFire.Abstraction.Core;
using Microsoft.Extensions.DependencyInjection;

namespace QFire.Core.Worker
{
    public class QFireBackgroundCoordinatorService<T> : BackgroundService where T : QFireMessage
    {
        private readonly IQFireAnalyzer<T> qFireAnalyzer;
        private readonly IServiceProvider serviceProvider;
        private readonly QFireConfiguration qFireConfiguration;
        public QFireBackgroundCoordinatorService(
            IQFireAnalyzer<T> qFireAnalyzer,
            IServiceProvider serviceProvider,
            QFireConfiguration qFireConfiguration)
        {
            this.qFireAnalyzer=qFireAnalyzer;
            this.serviceProvider=serviceProvider;
            this.qFireConfiguration=qFireConfiguration;
        }

        protected override async Task ExecuteAsync(CancellationToken cToken)
        {
            while (!cToken.IsCancellationRequested)
            {
                if (!qFireAnalyzer.IsMaxWorkerThreadExceed())
                {
                    if (qFireAnalyzer.IsQueueSizeIncreasing())
                    {
                        IQFireWorkerService worker = serviceProvider.GetService<IQFireWorkerService>();
                        TaskFactory taskFactory = new TaskFactory();
                        _=taskFactory.StartNew(() =>
                                    worker.ConsumeAsync()
                                    , cToken
                                    , TaskCreationOptions.LongRunning
                                    , TaskScheduler.Default);
                    }

                    await Task.Delay(qFireConfiguration.MaxDeleyInWorkerCreation*1000, cToken);
                }
            }
        }
    }
}
