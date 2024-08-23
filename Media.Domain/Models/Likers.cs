using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Media.Domain.Models
{
    public class Likers
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string LikerId { get; set; } = default!;

        [BsonElement("post_id"), BsonRepresentation(BsonType.ObjectId)]
        public string PostId { get; set; } = default!;

        [BsonElement("user_id")]
        public Guid UserId { get; set; }

        [BsonElement("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
