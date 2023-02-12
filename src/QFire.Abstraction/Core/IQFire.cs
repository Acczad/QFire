using QFire.Abstraction.Message;
using System.Threading.Tasks;

namespace QFire.Abstraction.Core
{
    public interface IQFire<T> where T : QFireMessage
    {
        Task<bool> SendAsync(T message);
    }
}