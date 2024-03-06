﻿using System.Collections.Generic;
using System;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ESourcing.Sourcing.Entities
{
    public class Auction
    {
        public Auction()
        {
            IncludedSellers = new List<string>();
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime FinishedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Status { get; set; }
        public List<string> IncludedSellers { get; set; }
    }
    public enum Status
    {
        Active = 0,
        Closed = 1,
        Passive = 2
    }
}
