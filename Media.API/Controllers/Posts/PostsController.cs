using ErrorOr;
using Media.Application.Comment.Commands.CreateComment;
using Media.Application.Posts.Commands.CreateComment;
using Media.Application.Posts.Commands.CreatePost;
using Media.Application.Posts.Commands.DeletePost;
using Media.Application.Posts.Commands.DislikePost;
using Media.Application.Posts.Commands.LikePost;
using Media.Application.Posts.Commands.UpdatePost;
using Media.Application.Posts.Common;
using Media.Application.Posts.Queries.GetComments;
using Media.Application.Posts.Queries.GetDetailPost;
using Media.Application.Posts.Queries.GetLikers;
using Media.Application.Posts.Queries.GetPostByUser;
using Media.Application.Posts.Queries.ListPosts;
using Media.Contracts.Posts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Media.API.Controllers.Posts
{
    [Route("posts")]
    public class PostsController : ApiController
    {
        private readonly ISender _meidator;

        public PostsController(ISender meidator)
        {
            _meidator = meidator;
        }

        [AllowAnonymous]
        [HttpGet] 
        public async Task<IActionResult> GetListPosts()
        {
            var query = new ListPostsQuery();

            var listPostsResult = await _meidator.Send(query);

            return listPostsResult.Match(
                listPostsResult => Ok(ListPostResult(listPostsResult)),
                errors => Problem(errors));
        }

        [HttpGet("{postId}")]
        public async Task<IActionResult> GetDetailPost(string postId)
        {
            var query = new GetDetailPostQuery()
            {
                PostId = postId
            };

            var postResult = await _meidator.Send(query);

            return postResult.Match(
                postResult => Ok(PostResult(postResult)),
                errors => Problem(errors));
        }

        [HttpGet("comment/{postId}")]
        public async Task<IActionResult> GetListPostByReplyId(string postId)
        {
            var query = new GetCommentQuery();
            query.postId = postId;

            var listCommentResult = await _meidator.Send(query);

            return listCommentResult.Match(
                listCommentResult => Ok(ListPostResult(listCommentResult)),
                errors => Problem(errors));
        }

        [HttpGet("{userId}/users")]
        public async Task<IActionResult> GetPostByUser(string userId)
        {
            var query = new GetPostByUserQuery();
                query.UserId = Guid.Parse(userId);

            var listPostsOfUserResult = await _meidator.Send(query);

            return listPostsOfUserResult.Match(
                listPostsOfUserResult => Ok(ListPostResult(listPostsOfUserResult)),
                errors => Problem(errors));
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(PostRequest request)
        {
            var command = new CreatePostCommand()
            {
                PostContent = request.PostContent,
                PostImageUrl = request.PostImageUrl,
                PostVideoUrl = request.PostVideoUrl,
                PostFileUrl = request.PostFileUrl,
                UserId = request.UserId,
            };

            ErrorOr<PostResult> createResult = await _meidator.Send(command);

            return createResult.Match(
                createResult => Ok(),
                errors => Problem(errors));
        }
        
        [HttpPost("comment")]
        public async Task<IActionResult> CreateComment(PostRequest request)
        {
            var command = new CreateReplyPostCommand()
            {
                PostContent = request.PostContent,
                PostImageUrl = request.PostImageUrl,
                PostVideoUrl = request.PostVideoUrl,
                PostFileUrl = request.PostFileUrl,
                UserId = request.UserId,
                ReplyId = request.ReplyId
            };

            ErrorOr<PostResult> createResult = await _meidator.Send(command);

            return createResult.Match(
                createResult => Ok(),
                errors => Problem(errors));
        }

        [HttpGet("liker/{userId}/{postId}")]
        public async Task<IActionResult> getLiker(Guid userId, string postId)
        {
            var query = new GetLikersQuery()
            {
                PostId = postId,
                UserId = userId
            };

            var result = await _meidator.Send(query);

            return result.Match(
                result => Ok(result),
                errors => Problem(errors));
        }

        [HttpPost("like")]
        public async Task<IActionResult> LikePost(LikeRequest request)
        {
            var command = new LikePostCommand()
            {
                PostId = request.PostId,
                UserId = request.UserId,
            };

            ErrorOr<PostResult> result = await _meidator.Send(command);

            return result.Match(
                result => Ok(PostResult(result)),
                errors => Problem(errors));
        }

        private object? PostResult(PostResult result)
        {
            var postResponse = new PostResponse
            {
                PostId = result.PostId,
                PostContent = result.PostContent,
                PostImageUrl = result.PostImageUrl,
                PostVideoUrl = result.PostVideoUrl,
                PostFileUrl = result.PostFileUrl,
                PostTotalLikes = result.PostTotalLikes,
                PostTotalComments = result.PostTotalComments,
                PostTotalShares = result.PostTotalShares,
                PostTotalMarks = result.PostTotalMarks,
                UserId = result.UserId,
                UserName = result.UserName,
                ImageUser = result.ImageUser,
                ReplyId = result.ReplyId,
                ReplierName = result.ReplierName,
                CreatedAt = result.CreatedAt,
            };

            return postResponse;
        }

        [HttpPatch]
        public async Task<IActionResult> UpdatePost(PostRequest request)
        {
            var command = new UpdatePostCommand()
            {
                PostId = request.PostId,
                PostContent = request.PostContent,
                PostImageUrl = request.PostImageUrl,
                PostVideoUrl = request.PostVideoUrl,
                PostFileUrl= request.PostFileUrl,
                UserId= request.UserId,
            };

            ErrorOr<PostResult> updateResult = await _meidator.Send(command);

            return updateResult.Match(
                updateResult => NoContent(),
                errors => Problem(errors));
        }

        [HttpDelete("{postId}/users/{userId}")]
        public async Task<IActionResult> DeletePost(string postId, string userId)
        {
            var command = new DeletePostCommand()
            {
                PostId = postId,
                UserId = Guid.Parse(userId)
            };

            var deleteResult = await _meidator.Send(command);

            return deleteResult.Match(
                deleteResult => NoContent(),
                errors => Problem(errors));
        }

        [HttpDelete("dislike")]
        public async Task<IActionResult> DislikePost(LikeRequest request)
        {
            var command = new DislikePostCommand()
            {
                PostId = request.PostId,
                UserId = request.UserId,
            };

            var dislikeResult = await _meidator.Send(command);

            return dislikeResult.Match(
                dislikeResult => Ok(dislikeResult),
                errors => Problem(errors));
        }

        private object? ListPostResult(List<PostResult> listPostsResult)
        {
            var result = new List<PostResponse>();

            foreach (var postResult in listPostsResult)
            {
                var postResponse = new PostResponse
                {
                    PostId = postResult.PostId,
                    PostContent = postResult.PostContent,
                    PostImageUrl = postResult.PostImageUrl,
                    PostVideoUrl = postResult.PostVideoUrl,
                    PostFileUrl = postResult.PostFileUrl,
                    PostTotalLikes = postResult.PostTotalLikes,
                    PostTotalComments = postResult.PostTotalComments,
                    PostTotalShares = postResult.PostTotalShares,
                    PostTotalMarks = postResult.PostTotalMarks,
                    UserId = postResult.UserId,
                    UserName = postResult.UserName,
                    ImageUser = postResult.ImageUser,
                    ReplyId = postResult.ReplyId,
                    ReplierName = postResult.ReplierName,
                    CreatedAt = postResult.CreatedAt,
                };

                result.Add(postResponse);
            }

            return result;
        }
    }
}
