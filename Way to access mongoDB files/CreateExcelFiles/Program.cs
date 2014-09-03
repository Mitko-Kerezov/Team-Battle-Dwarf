using System;
using System.Linq;
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

        static void SaveDataToFile(List<Record> records, int fileNo)
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

            xlWorkBook.SaveAs("d:\\dbteamwork\\Record" + fileNo + ".xlsx");
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
            Random rnd = new Random();
            SummerOlympicsEntities sqlEntities = new SummerOlympicsEntities();
            Record record;
            int personCount = sqlEntities.Athletes.Count() - 1;
            int eventCount = sqlEntities.Competitions.Count() - 1;
            for (int numOfFiles = 0; numOfFiles < 100; numOfFiles++)
            {
                int year = sqlEntities.Cities.OrderBy(r => Guid.NewGuid()).First().Edition;
                int numberOfParticipants = rnd.Next(100) + 10;
                List<int> places = new List<int>();
                for (int i = 0; i < numberOfParticipants; i++)
                {
                    places.Add(i+1);
                }
                List<Record> records = new List<Record>(numberOfParticipants);
                record = new Record("Year", "EventID", "PersonID", "Rank");
                records.Add(record);

                for (int i = 0; i < numberOfParticipants; i++)
                {
                    int evt = sqlEntities.Competitions.OrderBy(r => true).Skip(rnd.Next(eventCount)).First().CompetitionID;
                    int athl = sqlEntities.Athletes.OrderBy(r => true).Skip(rnd.Next(personCount)).First().AthletID;
                    int place = rnd.Next(places.Count);
                    record = new Record(year.ToString(), evt.ToString(), athl.ToString(), places[place].ToString());
                    records.Add(record);
                    places.RemoveAt(place);
                }
                SaveDataToFile(records, numOfFiles);
                Console.WriteLine("Record {0} saved", numOfFiles);
            }

            //List<Record> records = new List<Record>();
            //Record record = new Record("Year", "EventID", "PersonID", "Rank");
            //records.Add(record);
            //record = new Record("1998", "6", "10", "1");
            //records.Add(record);
        }
    }
}
