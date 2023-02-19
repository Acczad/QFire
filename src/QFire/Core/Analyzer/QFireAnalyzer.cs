using QFire.Abstraction.Analyzer;
using QFire.Abstraction.Configuration;
using QFire.Abstraction.Message;
using QFire.Abstraction.MessageBroker;
using QFire.Abstraction.MessageRepository;
using System.Threading.Tasks;

namespace QFire.Core.Analyzer
{
    public class QFireAnalyzer<T> : IQFireAnalyzer<T> where T : QFireMessage
    {
        private readonly QFireConfiguration qFireConfiguration;
        private readonly IQFireRepository<T> qFireRepository;
        private readonly IQFireBaseMessageBroker messageBroker;
        private byte CurrentWorketThreadCout;
        private readonly object LockObject;
        public QFireAnalyzer(
            QFireConfiguration qFireConfiguration
            , IQFireRepository<T> qFireRepository
            , IQFireBaseMessageBroker messageBroker)
        {
            this.qFireConfiguration=qFireConfiguration;
            this.qFireRepository=qFireRepository;
            this.messageBroker=messageBroker;
            CurrentWorketThreadCout=0;
            LockObject= new object();
        }

        public bool IsMaxWorkerThreadExceed()
        {
            return CurrentWorketThreadCout >= qFireConfiguration.MaxWorkerThread;
        }
        public bool IsQueueSizeIncreasing()
        {
            var currentSize = qFireRepository.GetQueueCount();
            if (currentSize == 0)
                return false;

            return true;
        }
        public void WorkerThreadCreated()
        {
            lock (LockObject)
            {
                ++CurrentWorketThreadCout;
            }
        }
        public void WorkerThreadTerminated()
        {
            lock (LockObject)
            {
                --CurrentWorketThreadCout;
            }
        }

        public async Task CheckMessageBrokerAvaiable()
        {
            var pignResult = await messageBroker.PingAsync();
        }
    }
}
