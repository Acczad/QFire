using QFire.Abstraction.Configuration;
using QFire.Abstraction.Core;
using System;

namespace QFire.Core
{
    public class MessageKeyGenerator : IMessageKeyGenerator
    {
        private readonly QFireConfiguration qFireConfiguration;
        private readonly object lockObject;
        private const string QfireMessageAbbrivation = "qfiremsg";
        public MessageKeyGenerator(QFireConfiguration qFireConfiguration)
        {
            this.qFireConfiguration=qFireConfiguration;
            lockObject = new object();
        }
        public string GenerateKey()
        {
            lock (lockObject)
            {
                var key = $"{QfireMessageAbbrivation}:{qFireConfiguration.CacheAbbrivation}:{DateTime.UtcNow.Ticks}-{Helpers.StringHelper.GetRandomString(3)}";
                return key;
            }
        }
        public string GetQfireCacheAbbrivation()
        {
            return QfireMessageAbbrivation;
        }
    }
}
