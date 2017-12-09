using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataManagement.Models
{
    public class Picture
    {
        public ObjectId ID { get; set; }

        [BsonElement("PictureID")]
        public int PictureID { get; set; }

        [BsonElement("Name")]
        public string PictureName { get; set; }

        [BsonElement("Caption")]
        public string PictureCap { get; set; }
    }
}
