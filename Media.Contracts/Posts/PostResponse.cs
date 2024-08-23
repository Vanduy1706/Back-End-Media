namespace Media.Contracts.Posts
{
    public class PostResponse
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

        public Guid UserId { get; set; }
        public string UserName { get; set; } = default!;
        public string ImageUser { get; set; } = default!;
        public string ReplyId { get; set; } = default!;
        public string ReplierName { get; set; } = default!;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
