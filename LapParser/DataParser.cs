using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

namespace LapParser
{
    class DataParser
    {

        private DataTable _dataTable;

        public DataParser(DataTable inDataTable)
        {
            _dataTable = inDataTable;
        }

        public DataTable ConsistencyRanking()
        {
            //avg of top 3 laps per driver
            //StringBuilder consistencyString = new StringBuilder();

            DataTable times = new DataTable("Times");
            DataColumn dColumn;
            DataRow dRow;

            dColumn = new DataColumn("Driver");
            times.Columns.Add(dColumn);
            dColumn = new DataColumn("Avg Lap Time of Top 3 Laps");
            dColumn.DataType = System.Type.GetType("System.TimeSpan");
            times.Columns.Add(dColumn);

            var Drivers = (from Row in _dataTable.AsEnumerable() where Row["Lap Tm"].ToString() != "" select Row["Name"]).Distinct();

            foreach (string Driver in Drivers)
            {
                Console.WriteLine("---Driver: " + Driver);
                var TopTimes = (from Row in _dataTable.AsEnumerable() where Row["Name"].ToString() == Driver && Row["Lap Tm"].ToString() != "" orderby Row["Lap Tm"] select Row["Lap Tm"]).Take(3);

                TimeSpan avgTime = new TimeSpan(0);
                foreach (TimeSpan time in TopTimes)
                {
                    avgTime = avgTime.Add(time);
                    Console.WriteLine("Time: " + time.ToString());
                }
                avgTime = TimeSpan.FromMilliseconds(avgTime.TotalMilliseconds / 3);
                Console.WriteLine("Avg Time: " + avgTime.ToString());
                dRow = times.NewRow();
                dRow[0] = Driver;
                dRow[1] = avgTime;
                times.Rows.Add(dRow);
            }

            return times;
        }
    }
}
