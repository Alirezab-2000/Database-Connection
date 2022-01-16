using System.Collections.Generic;
using DataBaseConnection.Models;

namespace DataBaseConnection.Utilities
{
    public class Methodes
    {
        public static string GetAverage(List<MainViewModel> rows)
        {
            float sub = 0;

            foreach (var row in rows)
            {
                sub += float.Parse(row.Amount[row.Amount.Count - 1]);
            }

            float avg = sub / (rows.Count);
            
            return avg+"";
        }
    }
}