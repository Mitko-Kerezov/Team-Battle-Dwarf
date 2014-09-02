namespace SummerOlympiads.Logic.SqlImporter
{
    using System;
    using System.Linq;

    using SummerOlympiads.Data.Excel;
    using SummerOlympiads.Data.Mongo;
    using SummerOlympiads.Model;
    using SummerOlympiads.Utils;

    public class SqlImporter
    {
        public void Import(OlympiadsEntities db)
        {
            var mongoReader = new MongoReader();
            
            var excelFiles = ZipHandler.ExtractDefaultFile();
            foreach (var excelFile in excelFiles)
            {
                var importer = new ExcelReader(excelFile);

                foreach (var record in importer)
                {
                    var newEvent = mongoReader.GetEvent(int.Parse(record.EventId));
                    var newAthlete = mongoReader.GetPerson(int.Parse(record.PersonId));
                    var newCity = mongoReader.GetCity(int.Parse(record.Year));

                    using (var scope = db.Database.BeginTransaction())
                    {
                        var cityInSql = db.Cities.Where(c => c.Name == newCity.Name).FirstOrDefault();
                        if (cityInSql == null)
                        {
                            cityInSql = new City() { Name = newCity.Name};
                            db.Cities.Add(cityInSql);
                        }

                        var olympiadInSql = db.SummerOlympiads.Where(o => o.City.Name == cityInSql.Name).FirstOrDefault();
                        if (olympiadInSql == null)
                        {
                            olympiadInSql = new SummerOlympiad()
                                                {
                                                    City = cityInSql,
                                                    CityId = cityInSql.CityId,
                                                    Year = newCity.Edition
                                                };
                            cityInSql.SummerOlympiads.Add(olympiadInSql);
                            db.SummerOlympiads.Add(olympiadInSql);
                        }


                        var sportInSql = db.Sports.Where(s => s.Name == newEvent.Sport).FirstOrDefault();
                        if (sportInSql == null)
                        {
                            sportInSql = new Sport() { Name = newEvent.Sport };
                            db.Sports.Add(sportInSql);
                        }

                        var disciplineInSql = db.Disciplines.Where(d => d.Name == newEvent.Discipline).FirstOrDefault();
                        if (disciplineInSql == null)
                        {
                            disciplineInSql = new Discipline()
                                                  {
                                                      Name = newEvent.Discipline,
                                                      Sport = sportInSql,
                                                      SportId = sportInSql.SportId
                                                  };
                            sportInSql.Disciplines.Add(disciplineInSql);
                            db.Disciplines.Add(disciplineInSql);
                        }

                        var eventInSql = db.Events.Where(e => e.Name == newEvent.Evt).FirstOrDefault();
                        if (eventInSql == null)
                        {
                            eventInSql = new Event()
                                             {
                                                 Name = newEvent.Evt,
                                                 Discipline = disciplineInSql,
                                                 DisciplineId = disciplineInSql.DisciplineId,
                                                 SummerOlympiad = olympiadInSql,
                                                 SummerOlympiadId = olympiadInSql.SummerOlympiadId
                                             };
                            disciplineInSql.Events.Add(eventInSql);
                            olympiadInSql.Events.Add(eventInSql);
                            db.Events.Add(eventInSql);
                        }

                        var nationalityInSql =
                            db.Nationalities.Where(n => n.Name == newAthlete.Country).FirstOrDefault();
                        if (nationalityInSql == null)
                        {
                            nationalityInSql = new Nationality() { Name = newAthlete.Country, };
                            db.Nationalities.Add(nationalityInSql);
                        }

                        var athleteInSql = db.Athletes.Where(a => a.FullName == newAthlete.Name).FirstOrDefault();
                        if (athleteInSql == null)
                        {
                            athleteInSql = new Athlete()
                                               {
                                                   FullName = newAthlete.Name,
                                                   Gender = newAthlete.Gender[0].ToString(),
                                                   Nationality = nationalityInSql,
                                                   NationalityId = nationalityInSql.NationalityId,
                                               };
                            nationalityInSql.Athletes.Add(athleteInSql);
                            db.Athletes.Add(athleteInSql);
                        }

                        var rankAsInteger = int.Parse(record.Rank);

                        var rankInSql = db.Rankings.Where(r => r.Rank == rankAsInteger).FirstOrDefault();
                        if (rankInSql == null)
                        {
                            rankInSql = new Ranking()
                                            {
                                                Athlete = athleteInSql,
                                                AthleteId = athleteInSql.AthleteId,
                                                Event = eventInSql,
                                                EventId = eventInSql.EventId,
                                                Rank = rankAsInteger
                                            };
                            athleteInSql.Rankings.Add(rankInSql);
                            eventInSql.Rankings.Add(rankInSql);
                            db.Rankings.Add(rankInSql);
                        }

                        db.SaveChanges();
                        scope.Commit();
                    }
                }
            }
            
        }
    }
}