using LogProxyAPI.Entities;
using System.Threading.Tasks;

namespace LogProxyAPI.Interfaces
{
    public interface IUserService
    {
        Task<User> Authenticate(string username, string password);       
    }
}
