using LogProxyAPI.Models;
using System.Threading.Tasks;

namespace LogProxyAPI.Interfaces
{
    public interface ISaveMessageCommand
    {
        Task<AirTableSaveResponseDTO> Execute(SaveRequestDTO request);
    }
}
