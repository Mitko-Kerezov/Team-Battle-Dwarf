using System;
using Excel = Microsoft.Office.Interop.Excel;
using SQLToMongoTransfer;
using System.Collections.Generic;


namespace CreateExcelFiles
{
    class Program
    {
        class Record
        {
            public string year;
            public string eventID;
            public string personID;
            public string rank;

            public Record(string yr, string eId, string pId, string r)
            {
                this.year = yr;
                this.eventID = eId;
                this.personID = pId;
                this.rank = r;
            }
        }

        static void SaveDataToFile(List<Record> records)
        {

            //            something.OrderBy(r => Guid.NewGuid()).Take(5);

            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add();

            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            for (int rows = 1; rows <= records.Count; rows++)
            {
                xlWorkSheet.Cells[rows, 1] = records[rows - 1].year;
                xlWorkSheet.Cells[rows, 2] = records[rows - 1].eventID;
                xlWorkSheet.Cells[rows, 3] = records[rows - 1].personID;
                xlWorkSheet.Cells[rows, 4] = records[rows - 1].rank;
            }

            xlWorkBook.SaveAs("d:\\dbteamwork\\csharp-Excel.xlsx");
            xlWorkBook.Close(true);
            xlApp.Quit();

            releaseObject(xlWorkSheet);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);
        }
        private static void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
            }
            finally
            {
                GC.Collect();
            }
        }

        static void Main(string[] args)
        {
            SummerOlympicsEntities sqlEntities = new SummerOlympicsEntities();

            List<Record> records = new List<Record>();
            Record record = new Record("Year", "EventID", "PersonID", "Rank");
            records.Add(record);
            record = new Record("1998", "6", "10", "1");
            records.Add(record);
            SaveDataToFile(records);
        }
    }
}
