using Media.Application.Interfaces.Persistence;
using Media.Domain.Models;
using Media.Infrastructure.Data;
using MongoDB.Driver;

namespace Media.Infrastructure.Persistence
{
    public class PostRepository : IPostRepository
    {
        private readonly IMongoCollection<Posts> _posts;
        private readonly IMongoCollection<Users> _users;
        private readonly IMongoCollection<Likers> _likers;

        public PostRepository(MongoDBContext context)
        {
            _posts = context.Posts;
            _users = context.Users;
            _likers = context.Likers;
        }

        public void Add(Media.Domain.Models.Posts post)
        {
            _posts.InsertOne(post);
        }

        public void AddReplyPost(Posts post)
        {
            _posts.InsertOne(post);
        }

        public void DeletePostByUser(string postId, Guid userId)
        {
            _posts.DeleteOne(post => post.PostId == postId && post.UserId == userId);
        }

        public void DislikePost(Likers likers)
        {
            _likers.DeleteOne(liker => liker.PostId == likers.PostId && liker.UserId == likers.UserId);
        }

        public async Task<List<Posts>>? GetAllPosts()
        {
            return await _posts.Find(post => post.ReplyId == null).ToListAsync();
        }

        public async Task<List<Posts>>? GetCommentByReplyId(string postId)
        {
            return await _posts.Find(post => post.ReplyId == postId).ToListAsync();
        }

        public async Task<Posts>? GetDetailsPostByUser(string postId, Guid userId)
        {
            return await _posts.Find(post => post.PostId == postId && post.UserId == userId).FirstOrDefaultAsync();
        }

        public async Task<Likers> GetLiker(Likers likers)
        {
            return await _likers.Find(liker => liker.PostId == likers.PostId && liker.UserId == likers.UserId).FirstOrDefaultAsync();
        }

        public async Task<List<Likers>> GetLikers(Likers likers)
        {
            return await _likers.Find(liker => liker.UserId == likers.UserId).ToListAsync();
        }

        public async Task<Posts>? GetPostById(string postId)
        {
            return await _posts.Find(post => post.PostId == postId).FirstOrDefaultAsync();
        }

        public async Task<List<Posts>>? GetPostByUser(Guid userId)
        {
            return await _posts.Find(post =>  post.UserId == userId).ToListAsync();
        }

        public void LikePost(Likers likers)
        {
            _likers.InsertOne(likers);
        }

        public async Task<bool> UpdateCommentAmount(Posts post)
        {
            var updateDefinition = Builders<Posts>.Update
                .Set(p => p.PostTotalComments, post.PostTotalComments);

            var result = await _posts.UpdateOneAsync(
                p => p.PostId == post.PostId, updateDefinition);

            return result.ModifiedCount > 0;
        }

        public async Task<bool> UpdateLikePost(Posts post)
        {
            var updateDefinition = Builders<Posts>.Update
                .Set(p => p.PostTotalLikes, post.PostTotalLikes);

            var result = await _posts.UpdateOneAsync(
                p => p.PostId == post.PostId, updateDefinition);

            return result.ModifiedCount > 0;
        }

        public async Task<bool> UpdatePostByUser(Posts postUpdate)
        {
            var updateDefinition = Builders<Posts>.Update
                .Set(p => p.PostContent, postUpdate.PostContent)
                .Set(p => p.PostImageUrl, postUpdate.PostImageUrl)
                .Set(p => p.PostVideoUrl, postUpdate.PostVideoUrl)
                .Set(p => p.PostFileUrl, postUpdate.PostFileUrl)
                .Set(p => p.CreatedAt, DateTime.UtcNow);

            var result = await _posts.UpdateOneAsync(
                post => post.PostId == postUpdate.PostId && post.UserId == postUpdate.UserId,
                updateDefinition);

            return result.ModifiedCount > 0;
        }

    }
}
