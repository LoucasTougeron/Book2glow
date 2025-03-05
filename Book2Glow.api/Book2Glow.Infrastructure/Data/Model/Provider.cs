using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Book2Glow.Infrastructure.Data.Model
{
    [Table("provider")]
    public class Provider
    {
        [Key]
        [ForeignKey("User")]
        [Column("id_user")]
        public int Id { get; set; }

        [Column("p_city")]
        public string City { get; set; }

        [Column("p_country")]
        public string Country { get; set; }

        [Column("p_latitude")]
        public double Latitude { get; set; }

        [Column("p_longitude")]
        public double Longitude { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
