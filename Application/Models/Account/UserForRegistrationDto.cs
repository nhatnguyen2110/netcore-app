using Application.Common.Mappings;
using Domain.Entities.User;
using System.ComponentModel.DataAnnotations;

namespace Application.Models.Account
{
    public class UserForRegistrationDto : IMapFrom<ApplicationUser>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public string Password { get; set; } = string.Empty;
        public string? Email { get; set; }
        public ICollection<string> Roles { get; set; } = new List<string>();
    }
}
