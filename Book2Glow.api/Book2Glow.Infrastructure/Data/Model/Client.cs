using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Book2Glow.Infrastructure.Data.Model
{
    [Table("client")]
    public class Client
    {
        [Key]
        [ForeignKey("User")]
        [Column("id_user")]
        public int Id { get; set; }

        [Column("c_city")]
        public string City { get; set; }

        [Column("c_country")]
        public string Country { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
