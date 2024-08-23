using System.ComponentModel.DataAnnotations;

namespace Media.Contracts.Posts
{
    public class PostRequest
    {
        public string PostId { get; set; } = default!;
        public string PostContent { get; set; } = default!;

        public string PostImageUrl { get; set; } = default!;

        public string PostVideoUrl { get; set; } = default!;

        public string PostFileUrl { get; set; } = default!;

        public int PostTotalLikes { get; set; }
        public int PostTotalComments { get; set; }
        public int PostTotalShares { get; set; }
        public int PostTotalMarks { get; set; }

        [Required]
        public Guid UserId { get; set; }
        public string ReplyId { get; set; } = default!;

    }
}
