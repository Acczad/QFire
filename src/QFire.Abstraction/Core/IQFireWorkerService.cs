using System.Threading.Tasks;

namespace QFire.Abstraction.Core
{
    public interface IQFireWorkerService
    {
        Task ConsumeAsync();
    }
}
