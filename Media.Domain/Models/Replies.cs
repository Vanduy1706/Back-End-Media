using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Media.Domain.Models
{
    public class Replies
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ReplyId { get; set; } = default!;

        [BsonElement("reply_content")]
        public string ReplyContent { get; set; } = default!;

        [BsonElement("reply_image_url")]
        public string ReplyImageUrl { get; set; } = default!;

        [BsonElement("reply_video_url")]
        public string ReplyVideoUrl { get; set; } = default!;

        [BsonElement("reply_file_url")]
        public string ReplyFileUrl { get; set; } = default!;

        [BsonElement("user_id")]
        public Guid UserId { get; set; }

        [BsonElement("comment_id"), BsonRepresentation(BsonType.ObjectId)]
        public string CommentId { get; set; } = default!;

        [BsonElement("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
