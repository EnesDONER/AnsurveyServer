﻿using Core.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete;

public class Survey:IMongoDBEntity
{
    [BsonId,BsonElement("_id"),BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public int OwnerUserId { get; set; }
    public string Title  { get; set; }
    public string Description { get; set; }
    public IEnumerable<Question> Questions { get; set; }
}
