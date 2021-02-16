using System.ComponentModel.DataAnnotations;

namespace LogProxyAPI.Controllers.LogProxy.DTO
{
    public class SaveRequestDto
    {
        [Required]
        public string Text { get; set; }
        [Required]
        public string Title { get; set; }
    }
}
