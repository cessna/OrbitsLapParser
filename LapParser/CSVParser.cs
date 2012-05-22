using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.IO;
//using System.Text.RegularExpressions;

namespace LapParser
{
    class CSVParser
    {
        private DataTable _dataTable;
        private string _filePath;

        public DataTable DataTableTimes
        {
            get { return _dataTable; }
        }
        

        public CSVParser(string FilePath)
        {
            _filePath = FilePath;

            try
            {
                using (StreamReader readCSV = new StreamReader(_filePath))
                {
                    string hLine;
                    string[] hRow;
                    string line;
                    string[] row;

                    hLine = readCSV.ReadLine().Replace("\"","");
                    Console.WriteLine(hLine);
                    hRow = hLine.Split(',');
                    //add to datatable
                    InitDataTable(hRow);

                    while ((line = readCSV.ReadLine()) != null)
                    {
                        line = line.Replace("\"", "");
                        row = line.Split(',');
                        //add to datatable
                        Console.WriteLine("---Adding Row---");
                        AddRow(row);
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void InitDataTable(string[] hRow)
        {
            _dataTable = new DataTable("Laps");
            DataColumn column;

            foreach (string _header in hRow)
            {
                column = new DataColumn(_header);
                if (_header == "Lap Tm")
                {
                    Console.WriteLine("Made new Time Column " + _header);
                    column.DataType = System.Type.GetType("System.TimeSpan");
                }
                _dataTable.Columns.Add(column);
                Console.WriteLine("Made new column " + _header);
            }

            //Maybe drop this?
            DataColumn[] PrimaryKeyColumns = new DataColumn[1];
            PrimaryKeyColumns[0] = _dataTable.Columns["#"];
            _dataTable.PrimaryKey = PrimaryKeyColumns;
        }

        private void AddRow(string[] dRow)
        {
            DataRow row;

            row = _dataTable.NewRow();

            for (int x = 0; x < dRow.Length; x++)
            {
                if (string.IsNullOrWhiteSpace(dRow[x]))
                {
                    Console.WriteLine("*Added Data: None column: " + x.ToString()); ;
                }
                else if (_dataTable.Columns[x].ColumnName == "Lap Tm")
                {
                    row[x] = convertLapTm(dRow[x]);
                    Console.WriteLine("*Added Data (Timespan): " + row[x].ToString() + "column: " + x.ToString());
                }
                else
                {
                    row[x] = dRow[x];
                    Console.WriteLine("*Added Data: " + row[x] + "column: " + x.ToString());
                }
                
            }

            _dataTable.Rows.Add(row);
        }

        private TimeSpan convertLapTm(string LapTm)
        {
            string[] timer = LapTm.Split(new Char [] {':','.'});
            TimeSpan lTime;

            int Minutes;
            int Seconds;
            int Milliseconds;
            if (Int32.TryParse(timer[0], out Minutes) && Int32.TryParse(timer[1], out Seconds) && Int32.TryParse(timer[2], out Milliseconds))
            {
                lTime = new TimeSpan(0, 0, Minutes, Seconds, Milliseconds);
            }
            else
            {
                lTime = new TimeSpan(0, 0, 59, 59, 59);
            }

            return lTime;
        }
    }
}
