namespace SummerOlympiads.Xml.XmlReport
{
    using System.Linq;
    using System.Xml;

    using Model;
    using System;

    public class XmlReportGenerator
    {
        private OlympiadsEntities database;
        private const string XmlReportFilePath = "../../../../XmlReports/";

        private const string XmlVersion = "1.0";
        private const string XmlEncoding = "UTF-8";
        private const string XmlStandalone = null;
        private const string XmlExtension = ".xml";

        private const string XmlRootElement = "olympiad";
        private const string XmlRootFirstAttribute = "year";
        private const string XmlRootSecondAttribute = "city";

        private const string XmlChildElementTierOne = "event";
        private const string XmlChildElementTierOneAttribute = "name";

        private const string XmlChildElementTierTwo = "athlete";
        private const string XmlChildElementTierTwoFirstAttribute = "name";
        private const string XmlChildElementTierTwoSecondAttribute = "rank";


        public XmlReportGenerator(OlympiadsEntities database)
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
            XmlNode docNode = doc.CreateXmlDeclaration(XmlVersion, XmlEncoding, XmlStandalone);
            doc.AppendChild(docNode);

            XmlNode olympiadNode = doc.CreateElement(XmlRootElement);
            XmlAttribute olympiadYearAttribute = doc.CreateAttribute(XmlRootFirstAttribute);
            olympiadYearAttribute.Value = year.ToString();
            XmlAttribute olympiadCityNameAttribute = doc.CreateAttribute(XmlRootSecondAttribute);
            olympiadCityNameAttribute.Value = this.database.SummerOlympiads.Where(o => o.Year == year).Select(o => o.City.Name).First();

            olympiadNode.Attributes.Append(olympiadYearAttribute);
            olympiadNode.Attributes.Append(olympiadCityNameAttribute);

            foreach (var eventItem in events)
            {
                XmlNode eventNode = doc.CreateElement(XmlChildElementTierOne);
                XmlAttribute eventNameAttribute = doc.CreateAttribute(XmlChildElementTierOneAttribute);
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
                    XmlNode athleteNode = doc.CreateElement(XmlChildElementTierTwo);
                    XmlAttribute athleteNameAttribute = doc.CreateAttribute(XmlChildElementTierTwoFirstAttribute);
                    athleteNameAttribute.Value = athleteWithRank.AthleteName;
                    athleteNode.Attributes.Append(athleteNameAttribute);

                    XmlAttribute athleteRankAttribute = doc.CreateAttribute(XmlChildElementTierTwoSecondAttribute);
                    athleteRankAttribute.Value = athleteWithRank.AthleteRank.ToString();
                    athleteNode.Attributes.Append(athleteRankAttribute);

                    eventNode.AppendChild(athleteNode);
                }

                olympiadNode.AppendChild(eventNode);

                doc.AppendChild(olympiadNode);

                doc.Save(XmlReportFilePath + year.ToString() + XmlExtension);
            }
        }
    }
}
