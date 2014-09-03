namespace SummerOlympiads.Logic.ExcelExport
{
    using System.Collections.Generic;
    using System.Linq;
    using System.IO;

    using Model.MySQL;
    using Model.Excel;
    using Data.SQLite;
    using System.Data.OleDb;
    using System;

    public class ExcelExporter
    {
        private const string FilePath = "../../../../ExcelReport/YoungestMedalists.xls";

        public void ExportToExcel(IGoldMedalistsUnitOfWork mysqlDb, GoldMedalistAgesContext sqlLiteDb)
        {
            var athletes = new List<YoungestGoldMedalist>();
            
            using ((IDisposable)mysqlDb)
            {
                using(sqlLiteDb)
                {
                    foreach (var athleteAge in sqlLiteDb.GoldMedalistAges)
                    {
                        var eventName = mysqlDb.Goldmedalists.Where(g => g.Name == athleteAge.Name).Select(g => g.Event).First();
                        var youngMedalist = new YoungestGoldMedalist()
                        {
                            Age = athleteAge.Age,
                            Event = eventName,
                            Name = athleteAge.Name
                        };

                        athletes.Add(youngMedalist);
                    }
                }
            }

            var groupedByEvents = athletes.GroupBy(a => a.Event);
            var youngestMedalists = new List<YoungestGoldMedalist>();
            foreach (var athlete in groupedByEvents)
            {
                var minAge = athlete.Min(a => a.Age);
                youngestMedalists.Add(athlete.First(a => a.Age == minAge));
            }

            if (File.Exists(FilePath))
            {
                File.Delete(FilePath);
            }

            CreateExcel(youngestMedalists);
            
        }

        private void CreateExcel(IList<YoungestGoldMedalist> youngestMedalists)
        {
            using (OleDbConnection conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FilePath + ";Extended Properties='Excel 8.0;HDR=Yes'"))
            {
                conn.Open();
                OleDbCommand createCommand = new OleDbCommand("CREATE TABLE [YoungestGoldMedalists] ([Age] number, [Name] string, [Event] string)", conn);
                createCommand.ExecuteNonQuery();
                foreach (var medalist in youngestMedalists.OrderBy(a => a.Event))
                {
                    var insertCommandString = "INSERT INTO [YoungestGoldMedalists] VALUES (@age, @name, @event)";

                    OleDbCommand insertCommand = new OleDbCommand();
                    insertCommand.Connection = conn;
                    insertCommand.CommandText = insertCommandString;
                    insertCommand.Parameters.AddWithValue("@age", medalist.Age);
                    insertCommand.Parameters.AddWithValue("@name", medalist.Name);
                    insertCommand.Parameters.AddWithValue("@event", medalist.Event);
                    insertCommand.ExecuteNonQuery();
                }
            }
        }
    }
}
