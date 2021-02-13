using System.ComponentModel.DataAnnotations;

namespace LogProxyAPI.Models
{
    public class SaveRequestDTO
    {
        [Required(ErrorMessage = "Please fill value")]       
        public string Title { get; set; }
        [Required(ErrorMessage = "Please fill value")]
        public string Text { get; set; }      
    }
}
