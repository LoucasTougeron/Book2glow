using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book2Glow.Infrastructure.Data.Model
{
    [Table("Service")]
    public class ServiceModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("S_Id")] // Nom de la colonne en base
        public Guid Id { get; set; }
        [Column("S_Duration")] // Nom de la colonne en base
        public int duration { get; set; } // En minutes
        [Column("S_Amount")] // Nom de la colonne en base
        public float amount { get; set; }
        [Column("S_Name")] // Nom de la colonne en base
        public string name { get; set; }

        [Column("S_CategoryId")]
        public Guid CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public CategoryModel Category { get; set; }

        public ICollection<BookingModel> Bookings { get; set; } = new List<BookingModel>();


    }
}
