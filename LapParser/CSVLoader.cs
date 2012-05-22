using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;

namespace LapParser
{
    class CSVLoader
    {
        private List<string> _csvFiles;

        public List<string> csvFiles
        {
            get { return _csvFiles; }
        }

        public CSVLoader()
        {
            _csvFiles = new List<string>();
            foreach (string csvFile in Directory.GetFiles(Directory.GetCurrentDirectory(), "*.csv"))
            {
                _csvFiles.Add(csvFile);
            }          
        }
    }
}
