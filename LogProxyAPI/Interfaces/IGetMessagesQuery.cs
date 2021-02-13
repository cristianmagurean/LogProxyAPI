using LogProxyAPI.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogProxyAPI.Interfaces
{
    public interface IGetMessagesQuery
    {
        Task<IEnumerable<Message>> Execute();
    }
}
