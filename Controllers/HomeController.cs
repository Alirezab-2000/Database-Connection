using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DataBaseConnection.Models;
using MySql.Data.MySqlClient;

namespace DataBaseConnection.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDataBaseConnection _dataBaseConnection;

        public HomeController(ILogger<HomeController> logger, IDataBaseConnection dataBaseConnection)
        {
            _logger = logger;
            _dataBaseConnection = dataBaseConnection;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(FirstConnectionViewModel model)
        {
            string cs = @"server=localhost;userid=" + model.UserName + ";password=" + model.Password + ";database=" +
                        model.DataBaseName;
            _dataBaseConnection._Connection = new MySqlConnection(cs);
            var mySqlConnection = _dataBaseConnection._Connection;
            mySqlConnection.Open();

            var stm = "SELECT VERSION()";
            var cmd = new MySqlCommand(stm, mySqlConnection);

            var version = cmd.ExecuteScalar().ToString();
            Console.WriteLine($"MySQL version: {version}");

            mySqlConnection.Close();
            return View("index2");
        }

        public IActionResult Index2()
        {
            return View();
        }

        public IActionResult Main()
        {
            var mySqlConnection = _dataBaseConnection._Connection;
            FirstMainViewModel model = new FirstMainViewModel();
            model.Data = new List<Tuple<string, string, string, string>>();
            
            mySqlConnection.Open();
            MySqlCommand command2 = mySqlConnection.CreateCommand();
            command2.CommandText = "SELECT * FROM INSTRUCTOR";
            MySqlDataReader reader2 = command2.ExecuteReader();
            while (reader2.Read())
            {
                model.Data.Add(
                    Tuple.Create(reader2["ID"].ToString() , reader2["name"].ToString() 
                        , reader2["dept_name"].ToString() , reader2["salary"].ToString())
                    );
            }
            mySqlConnection.Close();

            return View(model);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
        
        [HttpGet]
        public IActionResult AddNewRow()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult AddNewRow(AddNewRowViewModel model)
        {
            var mySqlConnection = _dataBaseConnection._Connection;
            
            mySqlConnection.Open();
            MySqlCommand command2 = mySqlConnection.CreateCommand();
            command2.CommandText = "insert into INSTRUCTOR (ID , name , dept_name , salary) values ('"+model.ID+"' , '"+model.name+"' , '"+model.dept_name+"' , '"+model.salary+"')";
            var result = command2.ExecuteNonQuery();
            mySqlConnection.Close();
            return  RedirectToAction("Main");
        }

        public IActionResult DeleteRow(string id)
        {
            var mySqlConnection = _dataBaseConnection._Connection;
            mySqlConnection.Open();
            MySqlCommand command2 = mySqlConnection.CreateCommand();
            command2.CommandText = "DELETE FROM INSTRUCTOR WHERE ID="+id;
            var result = command2.ExecuteNonQuery();
            mySqlConnection.Close();

            return RedirectToAction("Main");
        }
        
        [HttpGet]
        public IActionResult EditRow(string id , string name , string dept_name , string salary)
        {
            ViewBag.Id = id;
            ViewBag.name = name;
            ViewBag.dept_name = dept_name;
            ViewBag.salary = salary;
            
            return View();   
        }

        [HttpPost]
        public IActionResult EditRow(AddNewRowViewModel model)
        {
            var mySqlConnection = _dataBaseConnection._Connection;
            
            mySqlConnection.Open();
            MySqlCommand command2 = mySqlConnection.CreateCommand();
            command2.CommandText = "update INSTRUCTOR set name='"+model.name+"' , dept_name='"+model.dept_name+"' ,salary='"+float.Parse(model.salary)+"' where ID= '"+model.ID+"'";
            var result = command2.ExecuteNonQuery();
            mySqlConnection.Close();
            return  RedirectToAction("Main");   
        }
    }
}