using ErrorOr;
using Media.Application.Interfaces.Persistence;
using Media.Application.Reply.Common;
using Media.Domain.Common.Errors;
using MediatR;

namespace Media.Application.Reply.Queries.GetRepliesByComment
{
    public class GetRepliesByCommentQueryHandler : IRequestHandler<GetRepliesByCommentQuery, ErrorOr<List<ReplyResult>>>
    {
        private readonly IReplyRepository _replyRepository;

        public GetRepliesByCommentQueryHandler(IReplyRepository replyRepository)
        {
            _replyRepository = replyRepository;
        }

        public async Task<ErrorOr<List<ReplyResult>>> Handle(GetRepliesByCommentQuery query, CancellationToken cancellationToken)
        {
            var listRepliesByComment = await _replyRepository.GetRepliesByComment(query.CommentId);
            if (listRepliesByComment.Count == 0)
            {
                return Errors.Reply.NotFound;
            }

            var listResult = new List<ReplyResult>();

            foreach (var reply in listRepliesByComment)
            {
                var replyResult = new ReplyResult()
                {
                    ReplyId = reply.ReplyId,
                    ReplyContent = reply.ReplyContent,
                    ReplyImageUrl = reply.ReplyImageUrl,
                    ReplyVideoUrl = reply.ReplyVideoUrl,
                    ReplyFileUrl = reply.ReplyFileUrl,
                    UserId = reply.UserId,
                    CommentId = reply.CommentId,
                    CreatedAt = reply.CreatedAt,
                };

                listResult.Add(replyResult);
            }

            return listResult;
        }
    }
}
