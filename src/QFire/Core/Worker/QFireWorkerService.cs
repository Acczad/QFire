using QFire.Abstraction.Analyzer;
using QFire.Abstraction.Configuration;
using QFire.Abstraction.Core;
using QFire.Abstraction.Message;
using QFire.Abstraction.MessageBroker;
using QFire.Abstraction.MessageRepository;
using System.Threading;
using System.Threading.Tasks;

namespace QFire.Core.Worker
{
    public class QFireWorkerService<T> : IQFireWorkerService where T : QFireMessage
    {
        private readonly IQFireRepository<T> qFireRepository;
        private readonly IQFireAnalyzer<T> qFireAnalyzer;
        private readonly IBaseMessageBroker baseMessageBroker;
        private readonly QFireConfiguration qFireConfiguration;
        private int retryCount = 0;
        public QFireWorkerService(
             IQFireRepository<T> qFireRepository
            , IQFireAnalyzer<T> qFireAnalyzer
            , IBaseMessageBroker baseMessageBroker
            , QFireConfiguration qFireConfiguration
            )
        {
            this.qFireRepository=qFireRepository;
            this.qFireAnalyzer=qFireAnalyzer;
            this.baseMessageBroker=baseMessageBroker;
            this.qFireConfiguration=qFireConfiguration;
        }
        public async Task ConsumeAsync()
        {
            qFireAnalyzer.WorkerThreadCreated();

            while (true)
            {
                if (retryCount >= qFireConfiguration.MaxWorkerRetryBeforeTerminate)
                {
                    qFireAnalyzer.WorkerThreadTerminated();
                    break;
                }

                var message = await qFireRepository.DeQueueMessageAsync();

                if (message==null)
                {
                    ++retryCount;
                    Thread.Sleep(20); // short delay and retry. 
                    continue;
                }

                retryCount=0;
                var sendResult = await baseMessageBroker.SendAsync(message);
                await qFireRepository.FinalizeMessageAsync(message, sendResult);
            }
        }
    }
}
