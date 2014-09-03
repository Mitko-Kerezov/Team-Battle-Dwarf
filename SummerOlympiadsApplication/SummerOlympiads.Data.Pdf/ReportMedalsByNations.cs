namespace SummerOlympiads.Data.Pdf
{
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

        public void Generate()
        {
            foreach (var nationality in this.db.Nationalities)
            {
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
}