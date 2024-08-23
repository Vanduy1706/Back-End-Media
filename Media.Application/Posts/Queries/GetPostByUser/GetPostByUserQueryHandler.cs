using ErrorOr;
using Media.Application.Interfaces.Persistence;
using Media.Application.Posts.Common;
using Media.Domain.Common.Errors;
using MediatR;

namespace Media.Application.Posts.Queries.GetPostByUser
{
    public class GetPostByUserQueryHandler : IRequestHandler<GetPostByUserQuery, ErrorOr<List<PostResult>>>
    {
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;

        public GetPostByUserQueryHandler(IPostRepository postRepository, IUserRepository userRepository)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<List<PostResult>>> Handle(GetPostByUserQuery query, CancellationToken cancellationToken)
        {
            var listPostsByUser = await _postRepository.GetPostByUser(query.UserId);
            if (listPostsByUser.Count == 0)
            {
                return Errors.Post.NotFoundPost;
            }

            var result = new List<PostResult>();
            foreach (var post in listPostsByUser)
            {
                var userOfPost = await _userRepository.GetUserById(post.UserId);

                if(post.ReplyId == null)
                {
                    var postResult = new PostResult
                    {
                        PostId = post.PostId,
                        PostContent = post.PostContent,
                        PostImageUrl = post.PostImageUrl,
                        PostVideoUrl = post.PostVideoUrl,
                        PostFileUrl = post.PostFileUrl,
                        PostTotalLikes = post.PostTotalLikes,
                        PostTotalComments = post.PostTotalComments,
                        PostTotalShares = post.PostTotalShares,
                        PostTotalMarks = post.PostTotalMarks,
                        UserId = post.UserId,
                        UserName = userOfPost.UserName,
                        ImageUser = userOfPost.PersonalImageUrl,
                        ReplyId = post.ReplyId,
                        CreatedAt = post.CreatedAt,
                    };

                    result.Add(postResult);
                } else
                {
                    var postOfReply = await _postRepository.GetPostById(post.ReplyId);
                    if(postOfReply == null)
                    {
                        continue;
                    }

                    var userOfReply = await _userRepository.GetUserById(postOfReply.UserId);

                    var postResult = new PostResult
                    {
                        PostId = post.PostId,
                        PostContent = post.PostContent,
                        PostImageUrl = post.PostImageUrl,
                        PostVideoUrl = post.PostVideoUrl,
                        PostFileUrl = post.PostFileUrl,
                        PostTotalLikes = post.PostTotalLikes,
                        PostTotalComments = post.PostTotalComments,
                        PostTotalShares = post.PostTotalShares,
                        PostTotalMarks = post.PostTotalMarks,
                        UserId = post.UserId,
                        UserName = userOfPost.UserName,
                        ImageUser = userOfPost.PersonalImageUrl,
                        ReplyId = post.ReplyId,
                        ReplierName = userOfReply.UserName,
                        CreatedAt = post.CreatedAt,
                    };

                    result.Add(postResult);
                }

                
            }

            return result;
        }
    }
}
