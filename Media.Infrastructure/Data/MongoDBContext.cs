using Media.Infrastructure.Configurations;
using Media.Domain.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Media.Infrastructure.Data
{
    public class MongoDBContext
    {
        private readonly IMongoDatabase _database;

        public MongoDBContext(IOptions<MongoDBSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.DatabaseName);

            // Kiểm tra và tạo collection nếu không tồn tại
            CreateCollectionIfNotExists<Users>("users");
            CreateCollectionIfNotExists<Posts>("posts");
            CreateCollectionIfNotExists<Comments>("comments");
            CreateCollectionIfNotExists<Replies>("replies");
            CreateCollectionIfNotExists<Followers>("followers");
            CreateCollectionIfNotExists<Likers>("likers");
        }

        public IMongoCollection<Users> Users => _database.GetCollection<Users>("users");
        public IMongoCollection<Posts> Posts => _database.GetCollection<Posts>("posts");
        public IMongoCollection<Comments> Comments => _database.GetCollection<Comments>("comments");
        public IMongoCollection<Replies> Replies => _database.GetCollection<Replies>("replies");
        public IMongoCollection<Followers> Followers => _database.GetCollection<Followers>("followers");
        public IMongoCollection<Likers> Likers => _database.GetCollection<Likers>("likers");

        public void CreateCollectionIfNotExists<T>(string collectionName)
        {
            var collections = _database.ListCollectionNames().ToList();
            if (!collections.Contains(collectionName))
            {
                _database.CreateCollection(collectionName);
            }
        }
    }
}
