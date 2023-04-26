using Application.Common.Mappings;
using Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Account
{
    public class UserInfoDto : IMapFrom<ApplicationUser>
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public bool IsFirstLogin { get; set; }
    }
}
