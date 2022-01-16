using System.Collections.Generic;
using System.Linq;
using DataBaseConnection.Models;
using MySql.Data.MySqlClient;

namespace DataBaseConnection.Utilities
{
    public static class MySqlDataReaderExtentions
    {
        public static List<MainViewModel> ToList(this MySqlDataReader reader)
        {
            List<MainViewModel> viewModels = new List<MainViewModel>();

            var columns = Enumerable.Range(0, reader.FieldCount).Select(reader.GetName).ToList();

            while (reader.Read())
            {
                MainViewModel viewModel = new MainViewModel();
                List<string> values = new List<string>();
                
                foreach (string column in columns)
                {
                    values.Add(reader[column].ToString());
                }

                viewModel.ColumnName = columns;
                viewModel.Amount = values;
                
                viewModels.Add(viewModel);
            }

            return viewModels;
        }
    }
}