using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using QFire.Abstraction.Configuration;
using QFire.Abstraction.Message;

namespace QFire.Abstraction.MessageRepository
{
    public interface IQFireInRedisRepository<T> : IQFireRepository<T> where T : QFireMessage
    {
       void LoadCachedMessageKeys(IEnumerable<string> keys);
    }
}