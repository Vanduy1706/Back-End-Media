using ErrorOr;
using Media.Application.Comment.Common;
using Media.Application.Interfaces.Persistence;
using Media.Domain.Common.Errors;
using MediatR;

namespace Media.Application.Comment.Queries.GetCommentsByPost
{
    public class GetCommentByPostQueryHandler : IRequestHandler<GetCommentByPostQuery, ErrorOr<List<CommentResult>>>
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IPostRepository _postRepository;

        public GetCommentByPostQueryHandler(ICommentRepository commentRepository, IPostRepository postRepository)
        {
            _commentRepository = commentRepository;
            _postRepository = postRepository;
        }

        public async Task<ErrorOr<List<CommentResult>>> Handle(GetCommentByPostQuery query, CancellationToken cancellationToken)
        {
            var existingPost = await _postRepository.GetPostById(query.PostId);
            if (existingPost == null)
            {
                return Errors.Post.NotFoundPost;
            }

            var listComments = await _commentRepository.GetCommentsByPost(query.PostId);
            if(listComments.Count == 0)
            {
                return Errors.Comment.NotFoundComment;
            }

            var result = new List<CommentResult>();
            foreach (var comment in listComments)
            {
                var commentResult = new CommentResult()
                {
                    CommentId = comment.CommentId,
                    CommentContent = comment.CommentContent,
                    CommentImageUrl = comment.CommentImageUrl,
                    CommentVideoUrl = comment.CommentVideoUrl,
                    CommentFileUrl = comment.CommentFileUrl,
                    PostId = comment.PostId,
                    UserId = comment.UserId,
                    CreatedAt = DateTime.UtcNow,
                };

                result.Add(commentResult);
            }

            return result;
        }
    }
}
