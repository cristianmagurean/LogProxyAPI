using System.ComponentModel.DataAnnotations;

namespace LogProxyAPI.Models
{
    public class AuthenticateDTO
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
