using System.ComponentModel.DataAnnotations;

namespace DataBaseConnection.Models
{
    public class AddNewRowViewModel
    {
        [Required]
        public string ID { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string dept_name { get; set; }
        [Required]
        public string salary { get; set; }
    }
}