using Media.Domain.Models;

namespace Media.Application.Interfaces.Persistence
{
    public interface IUserRepository
    {
        Task<Users>? GetUserByAccountName(string accountName);
        Task<Users>? GetUserById(Guid userId);
        Task<List<Followers>> GetFollowers(Followers followers);
        Task<Followers> GetFollower(Followers followers);
        Task Follow(Followers followers);
        Task UnFollow(Followers followers);
        void Add(Users user);
        Task UpdateProfile(Users user);
        Task ForgetPassWord(Users user);
        Task<IEnumerable<Users>> SearchUsersByName(string name);
    }
}

