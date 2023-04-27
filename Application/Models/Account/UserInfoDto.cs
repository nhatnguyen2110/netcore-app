using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities.User;

namespace Application.Models.Account
{
    public class UserInfoDto : IMapFrom<ApplicationUser>
    {
        public void Mapping(Profile profile)
        {
            profile.CreateMap<ApplicationUser, UserInfoDto>()
                .ForMember(d => d.IsFirstLogin, opt => opt.MapFrom(s => s.LastLoginDate == null))
                ;
        }
        public string? Id { get; set; }
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public bool IsFirstLogin { get; set; }
    }
}
