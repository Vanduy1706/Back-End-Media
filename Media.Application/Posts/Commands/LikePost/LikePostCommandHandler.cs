using ErrorOr;
using Media.Application.Interfaces.Persistence;
using Media.Application.Posts.Common;
using Media.Domain.Common.Errors;
using Media.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media.Application.Posts.Commands.LikePost
{
    public class LikePostCommandHandler : IRequestHandler<LikePostCommand, ErrorOr<PostResult>>
    {
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;

        public LikePostCommandHandler(IPostRepository postRepository, IUserRepository userRepository)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<PostResult>> Handle(LikePostCommand command, CancellationToken cancellationToken)
        {
            var hasPost = await _postRepository.GetPostById(command.PostId);
            if(hasPost == null)
            {
                return Errors.Post.NotFoundPost;
            }
            
            var hasUser = await _userRepository.GetUserById(command.UserId);
            if(hasUser == null)
            {
                return Errors.User.UserNotFound;
            }

            var liker = new Likers()
            {
                PostId = command.PostId,
                UserId = command.UserId,
            };

            var hasLiker = await _postRepository.GetLiker(liker);
            if(hasLiker != null)
            {
                return Errors.Liker.NotFoundLiker;
            }

            _postRepository.LikePost(liker);

            var post = new Media.Domain.Models.Posts()
            {
                PostId = hasPost.PostId,
                PostTotalLikes = hasPost.PostTotalLikes + 1
            };

            var hasUpdate = await _postRepository.UpdateLikePost(post);

            var likePost = await _postRepository.GetPostById(command.PostId);

            var postUpdate = new PostResult()
            {
                PostTotalLikes = likePost.PostTotalLikes,
            };

            return postUpdate;
        }
    }
}
