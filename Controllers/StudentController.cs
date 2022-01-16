using System.Collections.Generic;
using DataBaseConnection.Models;
using DataBaseConnection.Utilities;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace DataBaseConnection.Controllers
{
    public class StudentController : Controller
    {
        private readonly IDataBaseConnection _dataBaseConnection;

        public StudentController(IDataBaseConnection dataBaseConnection)
        {
            _dataBaseConnection = dataBaseConnection;
        }
        
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult ResultIndex(string ID)
        {
            if(ModelState.IsValid){
            var connection = _dataBaseConnection._Connection;
            StudentResultViewModel viewModel = new StudentResultViewModel();
            
            connection.Open();

            MySqlCommand getResultcommand = connection.CreateCommand();
            

            getResultcommand.CommandText =
                "select t.ID , c.credits , c.title , c.dept_name , i.name as instructor , t.semester , t.year, s.building , s.room_number , group_concat(ti.day) as days , ti.start_time , ti.end_time , grade" +
                "from takes as t left outer join section as s using (course_id , sec_id , semester , year) left outer join timeslot as ti using(time_slot_id) left outer join course as c using (course_id) left outer join teaches as te using (course_id , sec_id , semester , year) left outer join instructor as i on te.id = i.id" +
                "group by course_id , sec_id , semester , year , t.ID having t.ID='"+ID+"'";
            
            getResultcommand.CommandText =
                "select t.ID , i.ID as insID , c.credits , c.title , c.dept_name , i.name as instructor , t.semester , t.year, s.building , s.room_number , group_concat(ti.day) as days , ti.start_time , ti.end_time , grade from ((((takes as t inner join course as c on t.course_id=c.course_id) inner join section as s on s.sec_id=t.sec_id and s.course_id=t.course_id and s.semester=t.semester and s.year=t.year)"+
                "inner join timeslot as ti on s.time_slot_id=ti.time_slot_id) inner join teaches as te on c.course_id=te.course_id and s.sec_id=te.sec_id and s.semester=te.semester and te.year=s.year) inner join instructor as i on te.ID=i.ID group by c.course_id , s.sec_id , s.semester , s.year , t.ID having  t.ID='" +
                ID + "';";
            
            MySqlDataReader resultReader = getResultcommand.ExecuteReader();

            viewModel.Results = resultReader.ToList();
            connection.Close();

            connection.Open();
            MySqlCommand getProfileCommand = connection.CreateCommand();
            getProfileCommand.CommandText =
                "select s.name , s.dept_name , tot_cred  ,i.name , i.dept_name ,  i.ID from (student as s inner join advisor as a on s.ID = a.s_ID) inner join instructor as i on a.i_ID=i.ID where s.ID ='" +
                ID + "';";
            MySqlDataReader profileReader = getProfileCommand.ExecuteReader();
            
            List<string> list = new List<string>();

            while (profileReader.Read())
            {
                list.Add(profileReader[0].ToString());
                list.Add(profileReader[1].ToString());
                list.Add(profileReader[2].ToString());
                list.Add(profileReader[3].ToString());
                list.Add(profileReader[4].ToString());
                list.Add(profileReader[5].ToString());
                list.Add(Methodes.GetAverage(viewModel.Results));
            }

            viewModel.ProfileDetails = list;
            connection.Close();

            if (viewModel.ProfileDetails.Count == 0)
            {
                ModelState.AddModelError("" , "the id dosent correspand to any one");
                return View("Index");
            }
            return View("StudentResult" , viewModel);
            }
            else
            {
                ModelState.AddModelError("" , "the id dosent correspand to any one");
                return View("Index");
            }
        }

        public IActionResult SectionProvider()
        {
            throw new System.NotImplementedException();
        }
    }
}