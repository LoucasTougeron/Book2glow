using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Book2Glow.Infrastructure.Data.Model
{
    [Table("user")]
    public class ApplicationUser : IdentityUser<int>
    {
        [Column("u_firstname")]
        public string FirstName { get; set; }

        [Column("u_lastname")]
        public string LastName { get; set; }

        [Column("u_role")]
        public string role { get; set; }

        public Client Client { get; set; }
        public Provider Provider { get; set; }
    }
}
