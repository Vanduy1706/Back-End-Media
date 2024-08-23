using Media.Application.Interfaces.Persistence;
using Media.Domain.Models;
using Media.Infrastructure.Data;
using MongoDB.Bson;
using MongoDB.Driver;
using static Media.Domain.Common.Errors.Errors;

namespace Media.Infrastructure.Persistence
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<Users> _users;
        private readonly IMongoCollection<Followers> _followers;

        public UserRepository(MongoDBContext context)
        {
            _users = context.Users;
            _followers = context.Followers;
        }

        public void Add(Users user)
        {
            _users.InsertOne(user);
        }

        public async Task Follow(Followers followers)
        {
            await _followers.InsertOneAsync(followers);
        }

        public async Task ForgetPassWord(Users user)
        {
            var updateDefinition = Builders<Users>.Update
                .Set(u => u.AccountPassword, user.AccountPassword);

            await _users.UpdateOneAsync(
                useraccount => useraccount.AccountName == user.AccountName,
                updateDefinition);
        }

        public async Task<Followers> GetFollower(Followers followers)
        {
            return await _followers.Find(
                follower => follower.UserId == followers.UserId 
                && follower.FollowedId == followers.FollowedId).FirstOrDefaultAsync();
        }

        public async Task<List<Followers>> GetFollowers(Followers followers)
        {
            return await _followers.Find(follower => follower.UserId == followers.UserId).ToListAsync();
        }

        public async Task<Users>? GetUserByAccountName(string accountName)
        {
            var user = await _users.Find(users => users.AccountName == accountName).FirstOrDefaultAsync();
            return user;
        }

        public async Task<Users>? GetUserById(Guid userId)
        {
            return await _users.Find(user => user.UserId == userId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Users>> SearchUsersByName(string name)
        {
            var filter = Builders<Users>.Filter.Regex("user_name", new BsonRegularExpression(name, "i")); // "i" for case-insensitive
            var users = await _users.Find(filter).ToListAsync();
            return users;
        }

        public async Task UnFollow(Followers followers)
        {
            await _followers.DeleteOneAsync(
                follower => follower.UserId == followers.UserId && follower.FollowedId == followers.FollowedId);
        }

        public async Task UpdateProfile(Users user)
        {
            var updateDefinition = Builders<Users>.Update
                .Set(u => u.UserName, user.UserName)
                .Set(u => u.UserDescription, user.UserDescription)
                .Set(u => u.DateOfBirth, user.DateOfBirth)
                .Set(u => u.PhoneNumber, user.PhoneNumber)
                .Set(u => u.Address, user.Address)
                .Set(u => u.UserJob, user.UserJob)
                .Set(u => u.PersonalImageUrl, user.PersonalImageUrl)
                .Set(u => u.BackgroundImageUrl, user.BackgroundImageUrl);

            await _users.UpdateOneAsync(
                userAccount => userAccount.UserId == user.UserId,
                updateDefinition);
        }
    }
}
