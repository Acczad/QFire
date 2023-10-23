using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.Hosting;
using QFire.Abstraction.Message;
using QFire.Abstraction.Caching;
using QFire.Abstraction.MessageRepository;
using QFire.Abstraction.Core;
using System.Linq;

namespace QFire.Core.Worker
{
    public class QFireBackgroundMessageLoaderService<T> : BackgroundService where T : QFireMessage
    {
        private readonly IQFireCache qFireCache;
        private readonly IQFireInRedisRepository<T> qFireInRedisRepository;
        private readonly IMessageKeyGenerator messageKeyGenerator;
        public QFireBackgroundMessageLoaderService(
            IQFireCache qFireCache,
            IQFireInRedisRepository<T> qFireInRedisRepository,
            IMessageKeyGenerator messageKeyGenerator)
        {
            this.qFireCache=qFireCache;
            this.qFireInRedisRepository=qFireInRedisRepository;
            this.messageKeyGenerator=messageKeyGenerator;
        }
        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var keys = await qFireCache.GetKeysByPattern(messageKeyGenerator.GetQfireCacheAbbrivation());
            keys= keys.OrderBy(q => q);
            qFireInRedisRepository.LoadCachedMessageKeys(keys);
        }
    }
}
