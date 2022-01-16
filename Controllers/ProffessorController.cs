using System.Collections.Generic;
using DataBaseConnection.Models;
using DataBaseConnection.Utilities;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace DataBaseConnection.Controllers
{
    public class ProfessorController : Controller
    {
        private IDataBaseConnection _dataBaseConnection;

        public ProfessorController(IDataBaseConnection dataBaseConnection)
        {
            _dataBaseConnection = dataBaseConnection;
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ResultIndex(string ID)
        {
            if(ModelState.IsValid){
            var connection = _dataBaseConnection._Connection;
            ProfessorResultViewModel viewModel = new ProfessorResultViewModel();
            
            connection.Open();

            MySqlCommand getResultcommand = connection.CreateCommand();
            getResultcommand.CommandText =
                "select t.ID , c.credits , c.title , c.dept_name , t.semester , t.year , s.building , s.room_number , group_concat(ti.day) as days , ti.start_time , ti.end_time from ((teaches as t inner join course as c on t.course_id=c.course_id) inner join section as s on t.semester=s.semester and t.year=s.year and t.sec_id=s.sec_id and t.course_id=s.course_id)"+
                " inner join timeslot as ti on s.time_slot_id=ti.time_slot_id group by c.course_id , s.sec_id , s.semester , s.year , t.ID having t.ID='" +
                ID + "';";
            MySqlDataReader resultReader = getResultcommand.ExecuteReader();

            viewModel.Results = resultReader.ToList();
            connection.Close();

            connection.Open();
            MySqlCommand getProfileCommand = connection.CreateCommand();
            getProfileCommand.CommandText =
                "select name , dept_name , salary from instructor where ID ='" +
                ID + "';";
            MySqlDataReader profileReader = getProfileCommand.ExecuteReader();
            
            List<string> list = new List<string>();

            while (profileReader.Read())
            {
                list.Add(profileReader[0].ToString());
                list.Add(profileReader[1].ToString());
                list.Add(profileReader[2].ToString());
            }
            viewModel.ProfileDetails = list;
            connection.Close();
            
            connection.Open();
            MySqlCommand command3 = connection.CreateCommand();
            command3.CommandText = "select s.name , s.dept_name , s.tot_cred , s.ID from advisor as a inner join student as s on a.s_id=s.ID where a.i_id='" +
                                   ID + "';";
            MySqlDataReader student = command3.ExecuteReader();

            viewModel.StudentResults = student.ToList();
            connection.Close();

            if (viewModel.ProfileDetails.Count == 0)
            {
                ModelState.AddModelError("" , "the id dosent correspand to any one");
                return View("Index");
            }
            return View("ProfessorResult" , viewModel);
            }
            else
            {
                ModelState.AddModelError("" , "the id dosent correspand to any one");
                return View("Index");
            }
        }
    }
}