using LogProxyAPI.Entities;
using LogProxyAPI.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogProxyAPI.Services
{
    public class UserService : IUserService
    {      
        // users hardcoded for simplicity, to be stored in a repository with hashed passwords
        private readonly List<User> _users = new List<User>
        {
            new User { Id = "1", UserName = "HC", Password = "test" }
        };

        public async Task<User> Authenticate(string username, string password)
        {
            return await Task.Run(() => _users.SingleOrDefault(x => x.UserName == username && x.Password == password));            
        }
    }
}
