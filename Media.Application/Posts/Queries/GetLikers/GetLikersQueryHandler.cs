using ErrorOr;
using Media.Application.Interfaces.Persistence;
using Media.Application.Posts.Common;
using Media.Domain.Common.Errors;
using Media.Domain.Models;
using MediatR;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Media.Application.Posts.Queries.GetLikers
{
    public class GetLikersQueryHandler : IRequestHandler<GetLikersQuery, ErrorOr<LikerResult>>
    {
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;

        public GetLikersQueryHandler(IPostRepository postRepository, IUserRepository userRepository)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<LikerResult>> Handle(GetLikersQuery request, CancellationToken cancellationToken)
        {
            var hasUser = await _userRepository.GetUserById(request.UserId);
            if(hasUser == null)
            {
                return Errors.User.UserNotFound;
            }

            var hasPost = await _postRepository.GetPostById(request.PostId);
            if( hasPost == null)
            {
                return Errors.Post.NotFoundPost;
            }

            var liker = new Likers()
            {
                PostId = request.PostId,
                UserId = hasUser.UserId,
            };

            var hasLikers = await _postRepository.GetLiker(liker);


            var newLikerPost = new LikerResult()
            {
                UserId = hasLikers.UserId,
                PostId = hasLikers.PostId,
            };
           

            return newLikerPost;
        }
    }
}
