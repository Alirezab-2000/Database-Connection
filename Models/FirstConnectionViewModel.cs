using System.ComponentModel.DataAnnotations;

namespace DataBaseConnection.Models
{
    public class FirstConnectionViewModel
    {
        [Required]
        public string DataBaseName { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}