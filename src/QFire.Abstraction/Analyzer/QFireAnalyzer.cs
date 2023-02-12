using QFire.Abstraction.Message;

namespace QFire.Abstraction.Analyzer
{
    public interface IQFireAnalyzer<T> where T : QFireMessage
    {
        bool IsQueueSizeIncreasing();
        bool IsMaxWorkerThreadExceed();
        void WorkerThreadCreated();
        void WorkerThreadTerminated();
    }
}
