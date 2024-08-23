using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Media.Domain.Models
{
    public class Followers
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string FollowerId { get; set; } = default!;

        [BsonElement("user_id")]
        public Guid UserId { get; set; }

        [BsonElement("followed_id")]
        public Guid FollowedId { get; set; }

        [BsonElement("following_status")]
        public bool FollowingStatus { get; set; }

        [BsonElement("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
