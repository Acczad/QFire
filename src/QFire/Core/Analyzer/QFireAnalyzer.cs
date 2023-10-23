using QFire.Abstraction.Analyzer;
using QFire.Abstraction.Configuration;
using QFire.Abstraction.Message;
using QFire.Abstraction.MessageBroker;
using QFire.Abstraction.MessageRepository;
using System.Threading;
using System.Threading.Tasks;

namespace QFire.Core.Analyzer
{
    public class QFireAnalyzer<T> : IQFireAnalyzer<T> where T : QFireMessage
    {
        private readonly QFireConfiguration qFireConfiguration;
        private readonly IQFireRepository<T> qFireRepository;
        private readonly IQFireBaseMessageBroker messageBroker;
        private int CurrentWorketThreadCout;

        public QFireAnalyzer(
            QFireConfiguration qFireConfiguration
            , IQFireRepository<T> qFireRepository
            , IQFireBaseMessageBroker messageBroker)
        {
            this.qFireConfiguration=qFireConfiguration;
            this.qFireRepository=qFireRepository;
            this.messageBroker=messageBroker;
        }

        public bool IsMaxWorkerThreadExceed()
        {
            return CurrentWorketThreadCout >= qFireConfiguration.MaxWorkerThread;
        }
        public bool IsQueueSizeIncreasing()
        {
            var currentSize = qFireRepository.GetQueueCount();
            return currentSize > 0;
        }
        public void WorkerThreadCreated()
        {
            Interlocked.Increment(ref CurrentWorketThreadCout);
        }
        public void WorkerThreadTerminated()
        {
            Interlocked.Decrement(ref CurrentWorketThreadCout);
        }

        public async Task CheckMessageBrokerAvaiable()
        {
            var pignResult = await messageBroker.PingAsync();
        }
    }
}
