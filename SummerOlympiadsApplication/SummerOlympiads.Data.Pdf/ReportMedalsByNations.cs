namespace SummerOlympiads.Data.Pdf
{
    using System;
    using System.Linq;

    using SummerOlympiads.Model;
    using SummerOlympiads.Model.Reports;

    public class ReportMedalsByNations
    {
        private readonly OlympiadsEntities db;

        public ReportMedalsByNations(OlympiadsEntities olympiadsEntities)
        {
            this.db = olympiadsEntities;
        }

        public void Generate(string nationalityAbbr)
        {
            var nationality = this.db.Nationalities.Where(n => n.Name == nationalityAbbr).FirstOrDefault();
            if(nationality == null)
            {
                throw new ArgumentException("There is no such nationality in database!");
            }

            var exporter = new PdfExporter();
            var nationalityName = nationality.Name;
            var report = new CountryReport
                                {
                                    Athletes =
                                        this.db.Rankings.Where(
                                            r => r.Athlete.Nationality.Name == nationalityName)
                                        .GroupBy(a => a.Athlete.FullName)
                                        .Select(
                                            a =>
                                            new CountryReportRow
                                                {
                                                    Name = a.Key, 
                                                    NumberMedals = a.Count()
                                                })
                                        .OrderByDescending(a => a.NumberMedals)
                                };

            exporter.ExportToFile(nationalityName, report);
        }
    }
}