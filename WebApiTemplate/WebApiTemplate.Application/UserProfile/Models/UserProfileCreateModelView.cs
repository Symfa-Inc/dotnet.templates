﻿using Entities = WebApiTemplate.Domain.Entities;

namespace WebApiTemplate.Application.UserProfile.Models
{
    public class UserProfileCreateModelView
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string District { get; set; }
        public string PostalCode { get; set; }
        public string Position { get; set; }
    }

    public static class UserProfileCreateModelViewExtension
    {
        public static UserProfileCreateModelView ToUserProfileCreateView(this Entities.UserProfile userProfile)
        {
            return new UserProfileCreateModelView
            {
                Id = userProfile.Id,
                UserId = userProfile.UserId,
                Email = userProfile.Email,
                Name = userProfile.Name,
                Surname = userProfile.Surname,
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
