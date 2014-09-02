namespace SummerOlympiads.Data.Excel
{
    /// <summary>
    /// Record representing a row in excelfile
    /// </summary>
    public class Record
    {
        public readonly string EventId;

        public readonly string PersonId;

        public readonly string Rank;

        public readonly string Year;

        public Record(string yr, string eId, string pId, string r)
        {
            this.Year = yr;
            this.EventId = eId;
            this.PersonId = pId;
            this.Rank = r;
        }
    }
}