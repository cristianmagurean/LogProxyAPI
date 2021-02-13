using System.Collections.Generic;

namespace LogProxyAPI.Models
{
    public class AirTableSaveRequestDTO
    {
        public List<SaveRecordsDTO> records { get; set; }
    }
    public class SaveRecordsDTO
    {       
        public FieldsDTO fields { get; set; }       
    }   
}
