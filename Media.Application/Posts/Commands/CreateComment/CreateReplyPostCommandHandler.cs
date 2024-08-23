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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Media.Application.Posts.Commands.CreateComment
{
    public class CreateReplyPostCommandHandler : IRequestHandler<CreateReplyPostCommand, ErrorOr<PostResult>>
    {
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;

        public CreateReplyPostCommandHandler(IPostRepository postRepository, IUserRepository userRepository)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<PostResult>> Handle(CreateReplyPostCommand command, CancellationToken cancellationToken)
        {
            var hasUser = await _userRepository.GetUserById(command.UserId);
            if (hasUser == null)
            {
                return Errors.User.UserNotFound;
            }

            var hasPost = await _postRepository.GetPostById(command.ReplyId);
            if (hasPost == null)
            {
                return Errors.Post.NotFoundPost;
            }

            var post = new Media.Domain.Models.Posts
            {
                PostContent = command.PostContent,
                PostImageUrl = command.PostImageUrl,
                PostVideoUrl = command.PostVideoUrl,
                PostFileUrl = command.PostFileUrl,
                PostTotalLikes = 0,
                PostTotalComments = 0,
                PostTotalShares = 0,
                PostTotalMarks = 0,
                UserId = command.UserId,
                ReplyId = command.ReplyId,
                CreatedAt = DateTime.UtcNow,
            };

            _postRepository.AddReplyPost(post);

            var newComment = new Media.Domain.Models.Posts
            {
                PostId = hasPost.PostId,
                PostTotalComments = hasPost.PostTotalComments + 1
            };

            await _postRepository.UpdateCommentAmount(newComment);

            var result = new PostResult
            {
                PostId = post.PostId,
                PostContent = post.PostContent,
                PostImageUrl = post.PostImageUrl,
                PostVideoUrl = post.PostVideoUrl,
                PostFileUrl = post.PostFileUrl,
                PostTotalLikes = post.PostTotalLikes,
                PostTotalComments= post.PostTotalComments,
                PostTotalShares= post.PostTotalShares,
                PostTotalMarks= post.PostTotalMarks,
                UserId = post.UserId,
                ReplyId = post.ReplyId,
                CreatedAt = post.CreatedAt,
            };

            return result;
        }
    }
}
