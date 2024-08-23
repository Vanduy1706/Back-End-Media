using Media.Domain.Models;

namespace Media.Application.Interfaces.Persistence
{
    public interface IReplyRepository
    {
        void Add(Replies replies);
        Task<List<Replies>> GetRepliesByComment(string commentId);
        Task<Replies> GetReplyById(string replyId);
        Task<Replies> UpdateReply(Replies replies);
        Task DeleteReply(string replyId, Guid userId);
    }
}
