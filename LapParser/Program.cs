using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Threading;

namespace LapParser
{
    class Program
    {
        static void Main(string[] args)
        {
             bool firstProc;
             using (Mutex LapMutex = new Mutex
                (
                true,
                "LapParser_77eef519388125870654f396ffdc9041",
                out firstProc
                ))
             {
                 if (firstProc)
                 {
                     CSVLoader csvFiles = new CSVLoader();
                     foreach (string path in csvFiles.csvFiles)
                     {
                         CSVParser parser = new CSVParser(path);
                         DataParser dParser = new DataParser(parser.DataTableTimes);
                         DataTable2Text outputFile = new DataTable2Text(dParser.ConsistencyRanking(), System.IO.Path.ChangeExtension(path, "txt"));
                     }

                     Console.ReadKey();
                 }
             }
        }
    }
}
