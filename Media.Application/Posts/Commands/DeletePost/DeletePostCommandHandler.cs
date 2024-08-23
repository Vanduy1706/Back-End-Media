using ErrorOr;
using Media.Application.Interfaces.Persistence;
using Media.Application.Posts.Common;
using Media.Domain.Common.Errors;
using MediatR;

namespace Media.Application.Posts.Commands.DeletePost
{
    public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, ErrorOr<PostResult>>
    {
        private readonly IPostRepository _postRepository;

        public DeletePostCommandHandler(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<ErrorOr<PostResult>> Handle(DeletePostCommand command, CancellationToken cancellationToken)
        {
            var hasUserWithPost = await _postRepository.GetDetailsPostByUser(command.PostId, command.UserId);
            if(hasUserWithPost == null)
            {
                return Errors.Authorization.UnAuthorized;
            }

            if(hasUserWithPost.ReplyId == null)
            {
                _postRepository.DeletePostByUser(command.PostId, command.UserId);

                return new PostResult();
            }

            var hasPostOfReply = await _postRepository.GetPostById(hasUserWithPost.ReplyId);

            if(hasPostOfReply == null)
            {
                return Errors.Post.NotFoundPost;
            }

            _postRepository.DeletePostByUser(command.PostId, command.UserId);

            var newComment = new Media.Domain.Models.Posts
            {
                PostId = hasPostOfReply.PostId,
                PostTotalComments = hasPostOfReply.PostTotalComments - 1
            };

            await _postRepository.UpdateCommentAmount(newComment);

            var result = new PostResult();

            return result;
        }
    }
}
