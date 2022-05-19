using Entities = WebApiTemplate.Domain.Entities;

namespace WebApiTemplate.Application.UserProfile.Models
{
    public class UserProfileGetModelView
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string District { get; set; }
        public string PostalCode { get; set; }
        public string Position { get; set; }
    }

    public static class UserProfileGetModelViewExtension
    {
        public static UserProfileGetModelView ToUserProfileGetView(this Entities.UserProfile userProfile)
        {
            return new UserProfileGetModelView
            {
                Id = userProfile.Id,
                UserId = userProfile.UserId,
                Email = userProfile.Email,
                UserName = userProfile.UserName,
                DateOfBirth = userProfile.DateOfBirth,
                Country = userProfile.Country,
                City = userProfile.City,
                State = userProfile.State,
                District = userProfile.District,
                PostalCode = userProfile.PostalCode,
                Position = userProfile.Position,
            };
        }
    }
}
