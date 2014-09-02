using System;
using MongoDB.Bson;


namespace SQLToMongoTransfer
{
    class Event
    {
        public BsonObjectId Id { get; set; }
        public int CompetitionID { get; set; }
        public string Sport { get; set; }
        public string Discipline { get; set; }
        public string Evt { get; set; }
    }
}

