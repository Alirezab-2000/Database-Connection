using System.Collections.Generic;

namespace DataBaseConnection.Models
{
    public class ProfessorResultViewModel
    {
        public ProfessorResultViewModel()
        {
            Results = new List<MainViewModel>();
            
            ProfileDetails = new List<string>();
        }
        public List<MainViewModel> Results { get; set; }
  
        public List<MainViewModel> StudentResults { get; set; }
        public List<string> ProfileDetails { get; set; }
    }
}