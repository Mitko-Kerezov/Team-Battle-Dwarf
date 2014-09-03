namespace SummerOlympiads.Data.Pdf
{
    using System;
    using System.Globalization;
    using System.IO;

    using MigraDoc.DocumentObjectModel;
    using MigraDoc.DocumentObjectModel.Tables;
    using MigraDoc.Rendering;

    using PdfSharp.Pdf;

    using SummerOlympiads.Model.Reports;

    public class PdfExporter
    {
        internal void ExportToFile(string reportName, CountryReport report)
        {
            var filename = FilenameCheck(ref reportName);
            var document = new Document();
            AddMetadata(reportName, document);
            var style = DefineStyles(document);

            var table = TableHeader(document, reportName, report);
            Columns(table);
            HeaderRows(table);
            FillData(report, table, style);
            SaveDocument(document, filename);
        }

        private static void FillData(CountryReport report, Table table, Style style)
        {
            var rows = report.Athletes;
            foreach (var countryReportRow in rows)
            {
                Row row = table.AddRow();
                row.Height = "1cm";
                row.HeadingFormat = true;
                row.Format.Alignment = ParagraphAlignment.Center;
                row.Shading.Color = Colors.SteelBlue;
                style.Font.Color = Colors.White;

                row.Cells[0].AddParagraph(countryReportRow.Name);
                row.Cells[0].Format.Alignment = ParagraphAlignment.Right;
                row.Cells[0].VerticalAlignment = VerticalAlignment.Center;

                row.Cells[1].AddParagraph(countryReportRow.NumberMedals
                    .ToString(CultureInfo.InvariantCulture));
                row.Cells[1].Format.Alignment = ParagraphAlignment.Center;
                row.Cells[1].VerticalAlignment = VerticalAlignment.Center;
            }
        }

        private static void SaveDocument(Document document, string filename)
        {
            var pdfRenderer = new PdfDocumentRenderer(false, PdfFontEmbedding.Always);
            pdfRenderer.Document = document;

            pdfRenderer.RenderDocument();

            pdfRenderer.PdfDocument.Save(filename);
        }

        private static void HeaderRows(Table table)
        {
            var row = table.AddRow();
            row.Height = "1.5cm";
            row.HeadingFormat = true;
            row.Format.Alignment = ParagraphAlignment.Center;
            row.Format.Font.Bold = true;
            row.Shading.Color = Colors.DarkOrange;

            row.Cells[0].AddParagraph("FAMILY, name");
            row.Cells[0].Format.Alignment = ParagraphAlignment.Center;
            row.Cells[0].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[1].AddParagraph("Number medals");
            row.Cells[1].Format.Alignment = ParagraphAlignment.Center;
            row.Cells[1].VerticalAlignment = VerticalAlignment.Center;
        }

        private static void Columns(Table table)
        {
            // Before you can add a row, you must define the columns
            var column = table.AddColumn("12cm");
            column.Format.Alignment = ParagraphAlignment.Center;
                table.AddColumn("4cm").Format.Alignment = ParagraphAlignment.Left;
        }

        private static Table TableHeader(Document document, string reportName, CountryReport report)
        {
            // Each MigraDoc document needs at least one section.
            var section = document.AddSection();

            // Create Header
            var paragraph = section.Headers.Primary.AddParagraph();
            paragraph.AddText("Report for " + reportName + " generated on " + report.GeneratedTime.ToShortDateString());
            paragraph.Format.Font.Size = PdfSettings.Default.fontSize + 8;
            paragraph.Format.Alignment = ParagraphAlignment.Center;

            // Create the item table
            var mainTable = section.AddTable();
            mainTable.Style = "Table";
            mainTable.Borders.Color = Colors.Teal;
            mainTable.Borders.Width = 0.25;
            mainTable.Borders.Left.Width = 0.5;
            mainTable.Borders.Right.Width = 0.5;
            mainTable.Rows.LeftIndent = 0;

            return mainTable;
        }

        private static string FilenameCheck(ref string reportName)
        {
            if (!Directory.Exists(PdfSettings.Default.ReportsPath))
            {
                Directory.CreateDirectory(PdfSettings.Default.ReportsPath);
            }

            if (reportName == null)
            {
                reportName = Guid.NewGuid().ToString("D") + ".pdf";
            }

            var filename = PdfSettings.Default.ReportsPath + reportName.Trim() + ".pdf";
            return filename;
        }

        private static void AddMetadata(string reportName, Document document)
        {
            document.Info.Title = reportName + " Report";
            document.Info.Author = "Team Battle Dwarf";
            document.Info.Subject = "Generated pdf report for SummerOlympiads";
            document.Info.Keywords = "Report, summer, olympiads, medals, ranking, countries, athletes";
        }

        private static Style DefineStyles(Document document)
        {
            // Get the predefined style Normal.
            var style = document.Styles["Normal"];

            // Because all styles are derived from Normal, the next line changes the 
            // font of the whole document. Or, more exactly, it changes the font of
            // all styles and paragraphs that do not redefine the font.
            style.Font.Name = PdfSettings.Default.fontFamily;

            style = document.Styles[StyleNames.Header];
            style.ParagraphFormat.AddTabStop("16cm", TabAlignment.Right);

            style = document.Styles[StyleNames.Footer];
            style.ParagraphFormat.AddTabStop("8cm", TabAlignment.Center);

            // Create a new style called Table based on style Normal
            style = document.Styles.AddStyle("Table", "Normal");
            style.Font.Name = PdfSettings.Default.fontFamily;
            style.Font.Size = PdfSettings.Default.fontSize;
            style.Font.Color = Colors.WhiteSmoke;

            // Create a new style called Reference based on style Normal
            style = document.Styles.AddStyle("Reference", "Normal");
            style.ParagraphFormat.SpaceBefore = "1cm";
            style.ParagraphFormat.SpaceAfter = "1cm";
            style.ParagraphFormat.TabStops.AddTabStop("16cm", TabAlignment.Right);
            return style;
        }
    }
}