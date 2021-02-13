using System.Collections.Generic;

namespace LogProxyAPI.Models
{
    public class AirTableGetResponseDTO
    {
        public List<RecordsDTO> records { get; set; }
        public string offset { get; set; }
    }     
}
