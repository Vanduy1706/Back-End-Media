using Media.Application.Interfaces.Persistence;
using Media.Domain.Models;
using Media.Infrastructure.Data;
using MongoDB.Driver;

namespace Media.Infrastructure.Persistence
{
    public class ReplyRepository : IReplyRepository
    {
        private readonly IMongoCollection<Replies> _replies;

        public ReplyRepository(MongoDBContext context)
        {
            _replies = context.Replies;
        }

        public void Add(Replies replies)
        {
            _replies.InsertOne(replies);
        }

        public async Task DeleteReply(string replyId, Guid userId)
        {
            await _replies.DeleteOneAsync(reply => reply.ReplyId == replyId && reply.UserId == userId);
        }

        public async Task<List<Replies>> GetRepliesByComment(string commentId)
        {
            return await _replies.Find(reply => reply.CommentId == commentId).ToListAsync();
        }

        public async Task<Replies> GetReplyById(string replyId)
        {
            return await _replies.Find(reply => reply.ReplyId == replyId).FirstOrDefaultAsync();
        }

        public async Task<Replies> UpdateReply(Replies replies)
        {
            var filter = Builders<Replies>.Filter.Where(
                reply => reply.UserId == replies.UserId
                && reply.ReplyId == replies.ReplyId);

            var update = Builders<Replies>.Update
                .Set(reply => reply.ReplyContent, replies.ReplyContent)
                .Set(reply => reply.ReplyImageUrl, replies.ReplyImageUrl)
                .Set(reply => reply.ReplyVideoUrl , replies.ReplyVideoUrl)
                .Set(reply => reply.ReplyFileUrl, replies.ReplyFileUrl)
                .Set(reply => reply.CreatedAt, DateTime.UtcNow);

            var options = new FindOneAndUpdateOptions<Replies>
            {
                ReturnDocument = ReturnDocument.After
            };

            var updatReplyResult = await _replies.FindOneAndUpdateAsync(filter, update, options);

            return updatReplyResult;
        }
    }
}
