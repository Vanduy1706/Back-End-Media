using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Media.Application.Reply.Common
{
    public class ReplyResult
    {
        public string ReplyId { get; set; } = default!;

        public string ReplyContent { get; set; } = default!;

        public string ReplyImageUrl { get; set; } = default!;

        public string ReplyVideoUrl { get; set; } = default!;

        public string ReplyFileUrl { get; set; } = default!;

        public Guid UserId { get; set; }

        public string CommentId { get; set; } = default!;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

    }
}
