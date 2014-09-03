using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace SQLToMongoTransfer
{
    class Program
    {
        static void Main(string[] args)
        {
            // connect to SQLEXPRESS
            SummerOlympicsEntities sqlEntities = new SummerOlympicsEntities();

            //connect to MongoDB
            var mongoClient = new MongoClient("mongodb://localhost");
            var mongoServer = mongoClient.GetServer();
            var mongoDatabase = mongoServer.GetDatabase("battle_dwarf_team");


            //var coll = mongoDatabase.CreateCollection("Athletes");
            //var athletes = mongoDatabase.GetCollection<Athlete>("Athletes");

            //Person tmpAthlete = new Person();
            //var sqlAthlets =
            //    from athl in sqlEntities.Athletes
            //    select athl;
            //int count = 0;
            //foreach (var athl in sqlAthlets)
            //{
            //    count++;
            //    tmpAthlete.Id = null;
            //    tmpAthlete.PersonID = athl.AthletID;
            //    tmpAthlete.Name = athl.Name;
            //    tmpAthlete.Country = athl.Country;
            //    tmpAthlete.Gender = athl.Gender;
            //    athletes.Insert(tmpAthlete);
            //    Console.Write("\rProcesing {0} records", count);
            //}
            //Console.Write("\rPress Enter");
            //Console.ReadLine();
            //var resultPersons =
            //    from p in athletes.AsQueryable<Person>()
            //    select p;
            //foreach (var p in resultPersons)
            //{
            //    Console.WriteLine(p.Id);
            //}

            //var mongoDBCreateCollectionResult = mongoDatabase.CreateCollection("Events");
            //var events = mongoDatabase.GetCollection<Event>("Events");

            //Event tmpEvent = new Event();
            //var sqlEvent =
            //    from evt in sqlEntities.Competitions
            //    select evt;
            //int count = 0;
            //foreach (var evt in sqlEvent)
            //{
            //    count++;
            //    tmpEvent.Id = null;
            //    tmpEvent.EventId = evt.CompetitionID;
            //    tmpEvent.Discipline = evt.Discipline;
            //    tmpEvent.Evt = evt.Event;
            //    tmpEvent.Sport = evt.Sport;
            //    events.Insert(tmpEvent);
            //    Console.Write("\rProcesing {0} records", count);
            //}
            //Console.Write("\rPress Enter");
            //Console.ReadLine();
            //var resultEvents =
            //    from e in events.AsQueryable<Event>()
            //    select e;
            //foreach (var e in resultEvents)
            //{
            //    Console.WriteLine(e.Id);
            //}

            var mongoDBCreateCitiesResult = mongoDatabase.CreateCollection("Cities");
            var cities = mongoDatabase.GetCollection<City>("Cities");
            Cities tmpCity = new Cities();
            var sqlCity =
                from city in sqlEntities.Cities
                select city;
            int count = 0;
            foreach (var city in sqlCity)
            {
                count++;
                tmpCity.Id = null;
                tmpCity.CityID = city.CityID;
                tmpCity.Name = city.City1;
                tmpCity.Edition = city.Edition;
                tmpCity.SpecialAnthem = null;
                cities.Insert(tmpCity);
                Console.Write("\rProcesing {0} records", count);
            }
            Console.Write("\rPress Enter");
            Console.ReadLine();
            var resultCities =
                from c in cities.AsQueryable<Cities>()
                select c;
            foreach (var e in resultCities)
            {
                Console.WriteLine(e.Id);
            }

        }
    }
}