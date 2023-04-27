using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities.User;

namespace Application.Models.Account
{
    public class UserForRegistrationDto : IMapFrom<ApplicationUser>
    {
        public void Mapping(Profile profile)
        {
            profile.CreateMap<ApplicationUser, UserForRegistrationDto>().ReverseMap()
                ;
        }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public string Password { get; set; } = string.Empty;
        public string? Email { get; set; }
    }
}
