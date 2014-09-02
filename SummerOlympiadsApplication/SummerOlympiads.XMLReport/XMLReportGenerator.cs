namespace SummerOlympiads.Xml.XMLReport
{
    using System.Linq;
    using System.Xml;

    using Model;
    using System;

    public class XMLReportGenerator
    {
        private OlympiadsEntities database;
        private const string XmlReportFilePath = "../../../../XmlReports/";

        public XMLReportGenerator(OlympiadsEntities database)
        {
            this.database = database;
        }

        /// <summary>
        /// Generates an XML report for the olympiad, taking place during a given year
        /// </summary>
        /// <param name="year">The year the olympiad took place</param>
        public void GenerateXMLReport(int year)
        {
            if (this.database.SummerOlympiads.Where(o => o.Year == year).FirstOrDefault() == null)
            {
                throw new ArgumentException("There is no data about an olympiad in the year " + year);
            }

            var events = this.database.Events.Where(e => e.SummerOlympiad.Year == year).Select(e => e.Name);

            XmlDocument doc = new XmlDocument();
            XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.AppendChild(docNode);

            XmlNode olympiadNode = doc.CreateElement("olympiad");
            XmlAttribute olympiadYearAttribute = doc.CreateAttribute("year");
            olympiadYearAttribute.Value = year.ToString();
            XmlAttribute olympiadCityNameAttribute = doc.CreateAttribute("city");
            olympiadCityNameAttribute.Value = this.database.SummerOlympiads.Where(o => o.Year == year).Select(o => o.City.Name).First();

            olympiadNode.Attributes.Append(olympiadYearAttribute);
            olympiadNode.Attributes.Append(olympiadCityNameAttribute);

            foreach (var eventItem in events)
            {
                XmlNode eventNode = doc.CreateElement("event");
                XmlAttribute eventNameAttribute = doc.CreateAttribute("name");
                eventNameAttribute.Value = eventItem;
                eventNode.Attributes.Append(eventNameAttribute);

                var athletesAndRanks = this.database.Rankings
                    .Where(r => r.Event.Name == eventItem && r.Rank <= 3)
                    .Select(r => new
                    {
                        AthleteName = r.Athlete.FullName,
                        AthleteRank = r.Rank
                    })
                    .OrderBy(a => a.AthleteRank);

                foreach (var athleteWithRank in athletesAndRanks)
                {
                    XmlNode athleteNode = doc.CreateElement("athlete");
                    XmlAttribute athleteNameAttribute = doc.CreateAttribute("name");
                    athleteNameAttribute.Value = athleteWithRank.AthleteName;
                    athleteNode.Attributes.Append(athleteNameAttribute);

                    XmlAttribute athleteRankAttribute = doc.CreateAttribute("rank");
                    athleteRankAttribute.Value = athleteWithRank.AthleteRank.ToString();
                    athleteNode.Attributes.Append(athleteRankAttribute);

                    eventNode.AppendChild(athleteNode);
                }

                olympiadNode.AppendChild(eventNode);

                doc.AppendChild(olympiadNode);

                doc.Save(XmlReportFilePath + year.ToString() +".xml");

                //Console.WriteLine("Report saved at " + XmlReportFilePath + year.ToString() + ".xml");

            }
        }
    }
}
