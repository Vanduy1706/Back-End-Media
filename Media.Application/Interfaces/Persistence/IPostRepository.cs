using Media.Domain.Models;

namespace Media.Application.Interfaces.Persistence
{
    public interface IPostRepository
    {
        void Add(Media.Domain.Models.Posts post);
        void AddReplyPost(Media.Domain.Models.Posts post);
        void LikePost(Likers likers);
        void DislikePost(Likers likers);
        Task<List<Media.Domain.Models.Posts>>? GetAllPosts();
        Task<List<Media.Domain.Models.Posts>>? GetPostByUser(Guid userId);
        Task<List<Media.Domain.Models.Posts>>? GetCommentByReplyId(string postId);
        Task<Media.Domain.Models.Posts>? GetDetailsPostByUser(string postId, Guid userId);
        Task<Media.Domain.Models.Posts>? GetPostById(string postId);
        Task<Likers> GetLiker(Likers likers);
        Task<List<Likers>> GetLikers(Likers likers);
        Task<bool> UpdatePostByUser(Media.Domain.Models.Posts post);
        Task<bool> UpdateLikePost(Media.Domain.Models.Posts post);
        Task<bool> UpdateCommentAmount(Media.Domain.Models.Posts post);
        void DeletePostByUser(string postId, Guid userId);
    }
}
