using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.IO;

namespace LapParser
{
    class DataTable2Text
    {
        private DataTable _dataTable;
        private string _path;
        private StringBuilder _txtBuilder;

        public DataTable2Text(DataTable inDataTable, string path)
        {
            _dataTable = inDataTable;
            _path = path;
            _txtBuilder = new StringBuilder();
            //InitTxtFile();
            ReadDataTable();
            SaveTxtFile();
        }

        //private void InitTxtFile()
        //{
        //    File.CreateText(_path);
        //    //if (!File.Exists(_path))
        //    //{
        //    //    File.CreateText(_path);
        //    //}
        //}

        private void ReadDataTable()
        {
            
            foreach(DataColumn dCol in _dataTable.Columns)
            {
                _txtBuilder.Append(dCol.ColumnName + " - ");
            }
            _txtBuilder.AppendLine();

            DataRow[] rows = (_dataTable.Select(string.Empty, "Avg Lap Time of Top 3 Laps asc"));

            for (int x = 0; x < rows.Length; x++)
            {
                _txtBuilder.Append((x + 1).ToString() + ": ");
                foreach (object item in rows[x].ItemArray)
                {
                    if (item is TimeSpan)
                    {
                        _txtBuilder.Append(((TimeSpan)item).ToString(@"mm\:ss\.fff"));
                    }
                    else
                    {
                        _txtBuilder.Append(item.ToString() + " - ");
                    }
                }
                _txtBuilder.AppendLine();
            }

            //for (int x = 0; x < _dataTable.Rows.Count; x++)
            //{
            //    _txtBuilder.Append((x + 1).ToString() + ": ");
            //    foreach (object item in _dataTable.Rows[x].ItemArray)
            //    {
            //        if (item is TimeSpan)
            //        {
            //            _txtBuilder.Append(((TimeSpan)item).ToString(@"mm\:ss\.fff"));
            //        }
            //        else
            //        {
            //            _txtBuilder.Append(item.ToString() + " - ");
            //        }
            //    }
            //    _txtBuilder.AppendLine();
            //}

            //foreach(DataRow dRow in _dataTable.Rows)
            //{
            //    foreach (object item in dRow.ItemArray)
            //    {
            //        if (item is TimeSpan)
            //        {
            //            _txtBuilder.Append(((TimeSpan)item).ToString(@"mm\:ss\.fff"));
            //        }
            //        else
            //        {
            //            _txtBuilder.Append(item.ToString() + " - ");
            //        }
            //    }
            //    _txtBuilder.AppendLine();
            //}
        }

        private void SaveTxtFile()
        {
            using (StreamWriter outfile = new StreamWriter(_path))
            {
                outfile.Write(_txtBuilder.ToString());
            }
        }
    }
}
