namespace SummerOlympiads.Data.Excel
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Data.OleDb;

    public class ExcelReader : IEnumerable<Record>
    {
        private readonly OleDbConnection connection;

        public ExcelReader(string filename)
        {
            var connectionString = string.Format(ExcelSettings.Default.ConnectionString, filename);
            this.connection = new OleDbConnection(connectionString);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public IEnumerator<Record> GetEnumerator()
        {
            this.connection.Open();

            using (this.connection)
            {
                var queryData = new OleDbCommand("SELECT * FROM [Sheet1$]", this.connection);
                var reader = queryData.ExecuteReader();

                while (reader.Read())
                {
                    var year = reader["Year"].ToString();
                    var eventId = reader["EventID"].ToString();
                    var athleteId = reader["PersonID"].ToString();
                    var rank = reader["Rank"].ToString();

                    var record = new Record(year, eventId, athleteId, rank);

                    yield return record;
                }
            }
        }
    }
}