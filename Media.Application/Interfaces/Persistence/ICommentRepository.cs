using Media.Domain.Models;

namespace Media.Application.Interfaces.Persistence
{
    public interface ICommentRepository
    {
        void Add(Comments comment);
        Task<List<Comments>>? GetCommentsByPost(string postId);
        Task<Comments>? GetCommentById(string commentId);
        Task<Comments> UpdateComment(Comments comment);
        Task DeleteComment(string commentId, Guid userId);
    }
}
