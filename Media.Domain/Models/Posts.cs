using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Media.Domain.Models
{
    public class Posts
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string PostId { get; set; } = default!;

        [BsonElement("post_content")]
        public string PostContent { get; set; } = default!;

        [BsonElement("post_image_url")]
        public string PostImageUrl { get; set; } = default!;

        [BsonElement("post_video_url")]
        public string PostVideoUrl { get; set; } = default!;

        [BsonElement("post_file_url")]
        public string PostFileUrl { get; set; } = default!;

        [BsonElement("post_total_likes")]
        public int PostTotalLikes { get; set; } 
        [BsonElement("post_total_comments")]
        public int PostTotalComments { get; set; }
        [BsonElement("post_total_shares")]
        public int PostTotalShares { get; set; }
        [BsonElement("post_total_marks")]
        public int PostTotalMarks { get; set; }

        [BsonElement("user_id")]
        public Guid UserId { get; set; }

        [BsonElement("reply_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ReplyId { get; set; } = default!;

        [BsonElement("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
