using QFire.Abstraction.Message;
using System.Threading.Tasks;

namespace QFire.Abstraction.Core
{
    public interface IQFireBaseSender
    {
        Task<bool> SendAsync(QFireMessage message);
    }
}
