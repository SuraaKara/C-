using System.Threading.Tasks;
using Clebra.Loopus.Core;
using Clebra.Loopus.Core.Service;
using Clebra.Loopus.Model;

namespace Clebra.Loopus.Service
{
    public interface IUserDataService : IDataService<LoopusUser>
    {
        Task<LoopusUser> SignIn(string username, string password);
    }
}
