using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Media.Domain.Models
{
    public class Users
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid UserId { get; set; }

        [BsonElement("user_name")]
        public string UserName { get; set; } = default!;

        [BsonElement("user_description")]
        public string UserDescription { get; set; } = default!;

        [BsonElement("date_of_birth")]
        public string DateOfBirth { get; set; }

        [BsonElement("phone_number")]
        public string PhoneNumber { get; set; } = default!;

        [BsonElement("address")]
        public string Address { get; set; } = default!;

        [BsonElement("user_job")]
        public string UserJob { get; set; } = default!;

        [BsonElement("personal_image_url")]
        public string PersonalImageUrl { get; set; } = default!;

        [BsonElement("background_image_url")]
        public string BackgroundImageUrl { get; set; } = default!;

        [BsonElement("account_name")]
        public string AccountName { get; set; } = default!;

        [BsonElement("account_password")]
        public string AccountPassword { get; set; } = default!;

        [BsonElement("active_status")]
        public bool ActiveStatus { get; set; }

        [BsonElement("access_times")]
        public int AccessTimes { get; set; }

        [BsonElement("user_role")]
        public string? UserRole { get; set; }

        [BsonElement("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
