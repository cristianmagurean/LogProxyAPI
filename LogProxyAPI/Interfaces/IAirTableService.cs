using LogProxyAPI.Models;
using System.Threading.Tasks;

namespace LogProxyAPI.Interfaces
{
    public interface IAirTableService
    {             
        Task<AirTableGetResponseDTO> GetMessagesAsync();      
        Task<AirTableSaveResponseDTO> SaveMessageAsync(AirTableSaveRequestDTO request);
    }
}
