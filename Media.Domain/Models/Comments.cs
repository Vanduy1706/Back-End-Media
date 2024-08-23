using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Media.Domain.Models
{
    public class Comments
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string CommentId { get; set; } = default!;

        [BsonElement("comment_content")]
        public string CommentContent { get; set; } = default!;

        [BsonElement("comment_image_url")]
        public string CommentImageUrl { get; set; } = default!;

        [BsonElement("comment_video_url")]
        public string CommentVideoUrl { get; set; } = default!;

        [BsonElement("comment_file_url")]
        public string CommentFileUrl { get; set; } = default!;

        [BsonElement("post_id"), BsonRepresentation(BsonType.ObjectId)]
        public string PostId { get; set; } = default!;

        [BsonElement("user_id")]
        public Guid UserId { get; set; }

        [BsonElement("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
