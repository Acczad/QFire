using System;

namespace QFire.Abstraction.Configuration
{
    public class QFireConfiguration
    {
        public QFireConfiguration()
        {
            MaxWorkerThread=10;
            MaxWorkerRetryBeforeTerminate=3;
            CacheAbbrivation="default";
            MaxDeleyInWorkerCreation = (int)new TimeSpan(0, 0, 4).TotalSeconds;
            MaxDeleyInWorkerCreation=4;
            MaxMessageInCache= (int)new TimeSpan(24, 0, 0).TotalSeconds;
            RedisCnnString="";
        }
        public QFireConfiguration SetMaxWorkerThread(int maxCount = 10)
        {
            MaxWorkerThread = maxCount;
            return this;
        }
        public QFireConfiguration SetMaxDeleyInWorkerCreation( int maxDelay = 4)
        {
            MaxDeleyInWorkerCreation=maxDelay;
            return this;
        }
        public QFireConfiguration SetMaxWorkerRetryBeforeTerminate( int retrayCount = 3)
        {
            MaxWorkerRetryBeforeTerminate=retrayCount;
            return this;
        }
        public QFireConfiguration SetMaxMessageCacheTime( int maxTime = 24*3600)
        {
            MaxMessageInCache=maxTime;
            return this;
        }
        public QFireConfiguration SetCacheAbbrivation( string abbrivation = "default")
        {
            CacheAbbrivation=abbrivation;
            return this;
        }
        public QFireConfiguration SetRedisCnnString( string cnnString)
        {
            RedisCnnString=cnnString;
            return this;
        }

        public int MaxWorkerThread { get; private set; }
        public int MaxDeleyInWorkerCreation { get; private set; }
        public int MaxWorkerRetryBeforeTerminate { get; private set; }
        public int MaxMessageInCache { get; private set; }
        public string CacheAbbrivation { get; private set; }
        public string RedisCnnString { get; private set; }
    }
}
