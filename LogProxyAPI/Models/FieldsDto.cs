using System;
using System.ComponentModel.DataAnnotations;

namespace LogProxyAPI.Models
{
    public class FieldsDTO
    {
        [Required]
        public string id { get; set; }
        [Required]
        public string Summary { get; set; }
        [Required]
        public string Message { get; set; }
        [Required]
        public string receivedAt { get; set; }
    }
}
