using System.ComponentModel.DataAnnotations;

namespace WebApiTemplate.Application.UserProfile.Models
{
    public class UserProfileUpdateModel
    {
        public DateTime DateOfBirth { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string District { get; set; }
        public string PostalCode { get; set; }
        public string Position { get; set; }
    }
}
