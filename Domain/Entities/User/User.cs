using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.User
{
    public class User : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
