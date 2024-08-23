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

namespace Media.Application.Posts.Queries.GetDetailPost
{
    public class GetDetailPostQueryHandler : IRequestHandler<GetDetailPostQuery, ErrorOr<PostResult>>
    {
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;

        public GetDetailPostQueryHandler(IPostRepository postRepository, IUserRepository userRepository)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<PostResult>> Handle(GetDetailPostQuery query, CancellationToken cancellationToken)
        {
            var hasPost = await _postRepository.GetPostById(query.PostId);
            if(hasPost == null)
            {
                return Errors.Post.NotFoundPost;
            }

            var userOfPost = await _userRepository.GetUserById(hasPost.UserId);

            if (hasPost.ReplyId == null)
            {
                var post = new PostResult()
                {
                    PostId = hasPost.PostId,
                    PostContent = hasPost.PostContent,
                    PostImageUrl = hasPost.PostImageUrl,
                    PostVideoUrl = hasPost.PostVideoUrl,
                    PostFileUrl = hasPost.PostFileUrl,
                    PostTotalLikes = hasPost.PostTotalLikes,
                    PostTotalComments = hasPost.PostTotalComments,
                    PostTotalShares = hasPost.PostTotalShares,
                    PostTotalMarks = hasPost.PostTotalMarks,
                    UserId = hasPost.UserId,
                    UserName = userOfPost.UserName,
                    ImageUser = userOfPost.PersonalImageUrl,
                    ReplyId = hasPost.ReplyId,
                    CreatedAt = hasPost.CreatedAt,
                };

                return post;
            } else
            {
                var postOfReply = await _postRepository.GetPostById(hasPost.ReplyId);

                var userOfReply = await _userRepository.GetUserById(postOfReply.UserId);

                var post = new PostResult()
                {
                    PostId = hasPost.PostId,
                    PostContent = hasPost.PostContent,
                    PostImageUrl = hasPost.PostImageUrl,
                    PostVideoUrl = hasPost.PostVideoUrl,
                    PostFileUrl = hasPost.PostFileUrl,
                    PostTotalLikes = hasPost.PostTotalLikes,
                    PostTotalComments = hasPost.PostTotalComments,
                    PostTotalShares = hasPost.PostTotalShares,
                    PostTotalMarks = hasPost.PostTotalMarks,
                    UserId = hasPost.UserId,
                    UserName = userOfPost.UserName,
                    ImageUser = userOfPost.PersonalImageUrl,
                    ReplyId = hasPost.ReplyId,
                    ReplierName = userOfReply.UserName,
                    CreatedAt = hasPost.CreatedAt,
                };

                return post;
            }
        }
    }
}
