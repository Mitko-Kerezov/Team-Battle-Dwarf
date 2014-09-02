﻿namespace SQLToMongoTransfer
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    public class Event
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public int EventId { get; set; }

        public string Sport { get; set; }

        public string Discipline { get; set; }

        public string Evt { get; set; }
    }
}