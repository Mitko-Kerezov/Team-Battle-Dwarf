namespace SummerOlympiads.Data.Excel
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data.OleDb;

    public class ExcelReader : IEnumerable<Tuple<int, int, int, int>>
    {
        private readonly OleDbConnection connection;

        public ExcelReader(string filename)
        {
            var connectionString = string.Format(ExcelSettings.Default.ConnectionString, filename);
            this.connection = new OleDbConnection(connectionString);
        }

        public IEnumerator<Tuple<int, int, int, int>> GetEnumerator()
        {
            this.connection.Open();

            using (this.connection)
            {
                var queryData = new OleDbCommand("SELECT * FROM [Sheet1$]", this.connection);
                var reader = queryData.ExecuteReader();
                while (reader.Read())
                {
                    // TODO: make class like return new Olympiad
                    var year = (int)(double)reader["Year"];
                    var eventId = (int)(double)reader["EventId"];
                    var athleteId = (int)(double)reader["AthleteId"];
                    var rank = (int)(double)reader["Rank"];
                    var row = new Tuple<int, int, int, int>(year, eventId, athleteId, rank);
                    yield return row;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}