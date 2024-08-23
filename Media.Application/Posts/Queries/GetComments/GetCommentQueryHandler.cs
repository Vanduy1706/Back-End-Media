using ErrorOr;
using Media.Application.Interfaces.Persistence;
using Media.Application.Posts.Common;
using Media.Domain.Common.Errors;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Media.Domain.Common.Errors.Errors;

namespace Media.Application.Posts.Queries.GetComments
{
    public class GetCommentQueryHandler : IRequestHandler<GetCommentQuery, ErrorOr<List<PostResult>>>
    {
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;

        public GetCommentQueryHandler(IPostRepository postRepository, IUserRepository userRepository)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<List<PostResult>>> Handle(GetCommentQuery query, CancellationToken cancellationToken)
        {
            var hasComment = await _postRepository.GetCommentByReplyId(query.postId);
            if(hasComment == null)
            {
                return Errors.Post.NotFoundPost;
            }

            var result = new List<PostResult>();
            foreach (var comment in hasComment)
            {
                var userOfPost = await _userRepository.GetUserById(comment.UserId);

                var postOfReply = await _postRepository.GetPostById(query.postId);

                var userOfReply = await _userRepository.GetUserById(postOfReply.UserId);

                var postResult = new PostResult
                {
                    PostId = comment.PostId,
                    PostContent = comment.PostContent,
                    PostImageUrl = comment.PostImageUrl,
                    PostVideoUrl = comment.PostVideoUrl,
                    PostFileUrl = comment.PostFileUrl,
                    PostTotalLikes = comment.PostTotalLikes,
                    PostTotalComments = comment.PostTotalComments,
                    PostTotalShares = comment.PostTotalShares,
                    PostTotalMarks = comment.PostTotalMarks,
                    UserId = comment.UserId,
                    UserName = userOfPost.UserName,
                    ImageUser = userOfPost.PersonalImageUrl,
                    ReplyId = comment.ReplyId,
                    ReplierName = userOfReply.UserName,
                    CreatedAt = comment.CreatedAt,
                };

                result.Add(postResult);
            }

            return result;
        }
    }
}
