using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace DataBaseConnection.Models
{
    public class FirstMainViewModel
    {
        public List<Tuple<string,string,string,string>> Data { get; set; }
    }
}