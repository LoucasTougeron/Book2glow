using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Book2Glow.Infrastructure.Data.Model
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }  
        public string LastName { get; set; } 
        public string PhoneNumber { get; set; } 
    }
}
