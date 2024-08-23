using Media.Application.Interfaces.Persistence;
using Media.Domain.Models;
using Media.Infrastructure.Data;
using MongoDB.Driver;

namespace Media.Infrastructure.Persistence
{
    public class CommentRepository : ICommentRepository
    {
        private readonly IMongoCollection<Comments> _comments;

        public CommentRepository(MongoDBContext context)
        {
            _comments = context.Comments;
        }

        public void Add(Comments comment)
        {
            _comments.InsertOne(comment);
        }

        public async Task DeleteComment(string commentId, Guid userId)
        {
            await _comments.DeleteOneAsync(comment => comment.CommentId == commentId && comment.UserId == userId);
        }

        public async Task<Comments>? GetCommentById(string commentId)
        {
            return await _comments.Find(comment => comment.CommentId == commentId).FirstOrDefaultAsync();
        }

        public async Task<List<Comments>>? GetCommentsByPost(string postId)
        {
            return await _comments.Find(comment => comment.PostId == postId).ToListAsync();
        }

        public async Task<Comments> UpdateComment(Comments commentUpdate)
        {
            var filter = Builders<Comments>.Filter.Where(
                comment => comment.UserId == commentUpdate.UserId
                && comment.CommentId == commentUpdate.CommentId);

            var update = Builders<Comments>.Update
                .Set(comment => comment.CommentContent, commentUpdate.CommentContent)
                .Set(comment => comment.CommentImageUrl, commentUpdate.CommentImageUrl)
                .Set(comment => comment.CommentVideoUrl, commentUpdate.CommentVideoUrl)
                .Set(comment => comment.CommentFileUrl, commentUpdate.CommentFileUrl)
                .Set(comment => comment.CreatedAt, DateTime.UtcNow);

            var options = new FindOneAndUpdateOptions<Comments>
            {
                ReturnDocument = ReturnDocument.After
            };

            var updateCommentResult = await _comments.FindOneAndUpdateAsync(filter, update, options);

            return updateCommentResult;
        }
    }
}
