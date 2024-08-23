using System.ComponentModel.DataAnnotations;

namespace Media.Contracts.Comment
{
    public class CommentResponse
    {
        public string CommentId { get; set; } = default!;

        public string CommentContent { get; set; } = default!;

        public string CommentImageUrl { get; set; } = default!;

        public string CommentVideoUrl { get; set; } = default!;

        public string CommentFileUrl { get; set; } = default!;

        public string PostId { get; set; } = default!;

        [Required]
        public Guid UserId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
