using System;

namespace LogProxyAPI.Models
{
    public class RecordsDTO
    {    
        public string id { get; set; }
        public FieldsDTO fields { get; set; }
        public DateTime createdTime { get; set; }
    }
}
