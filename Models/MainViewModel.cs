using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace DataBaseConnection.Models
{
    public class MainViewModel
    {
        public MainViewModel()
        {
            ColumnName = new List<string>();
            Amount = new List<string>();
        }
        //public string TableName { get; set; }
        public List<string> ColumnName { get; set; }
        public List<string> Amount { get; set; }
    }
} 