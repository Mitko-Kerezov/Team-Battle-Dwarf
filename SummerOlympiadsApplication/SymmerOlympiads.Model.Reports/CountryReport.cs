namespace SummerOlympiads.Model.Reports
{
    using System;
    using System.Collections.Generic;

    public class CountryReport
    {
        public CountryReport()
        {
            this.Athletes = new List<CountryReportRow>();
            this.GeneratedTime = DateTime.Now;
        }

        public DateTime GeneratedTime { get; set; }

        public IEnumerable<CountryReportRow> Athletes { get; set; }
    }
}