using LogProxyAPI.Models;
using System.Collections.Generic;

namespace LogProxyAPI.Controllers.LogProxy.DTO
{
    public class SaveResponseDTO
    {
        public List<RecordsDTO> records { get; set; }
    }
}
