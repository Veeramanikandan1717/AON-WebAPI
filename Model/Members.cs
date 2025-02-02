namespace ArulOliNagar.Model
{
    using MongoDB.Driver;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    public class Members
    {


        [BsonId]
        public ObjectId Id { get; set; }  // MongoDB's unique identifier for the document

        
        public string MemberName { get; set; }
        public string FatherName { get; set; }

        public string BloodGroup { get; set; }

        public int Age { get; set;  }

        public string UserId { get; set; }

        public string PhoneNo { get; set;  }
         
        public byte[] Photo { get; set; }   



    }

}
