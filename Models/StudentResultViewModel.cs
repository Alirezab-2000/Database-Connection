using System.Collections.Generic;

namespace DataBaseConnection.Models
{
    public class StudentResultViewModel
    {
        public StudentResultViewModel()
        {
            Results = new List<MainViewModel>();
            
            ProfileDetails = new List<string>();
        }
        public List<MainViewModel> Results { get; set; }
        public List<string> ProfileDetails { get; set; }
    }
}