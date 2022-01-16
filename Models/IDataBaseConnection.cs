using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace DataBaseConnection.Models
{
    public interface IDataBaseConnection
    {
        public bool AddEntity(string id, string Name, string Dept_name, int salary);
        public List<string> GetTableNames();
        MySqlConnection _Connection { get; set; }
    }

    public class DataBaseConnection : IDataBaseConnection
    {
        public MySqlConnection _Connection { get; set; }
        
        
        public bool AddEntity(string id, string Name, string Dept_name, int salary)
        {
            throw new System.NotImplementedException();
        }

        public List<string> GetTableNames()
        {
            throw new System.NotImplementedException();
        }
    }
}