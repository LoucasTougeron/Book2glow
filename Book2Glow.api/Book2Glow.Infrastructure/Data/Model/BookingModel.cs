using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book2Glow.Infrastructure.Data.Model
{
    public class BookingModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto-généré par la base
        [Column("B_Id")] // Nom de la colonne en base
        public Guid Id { get; set; }
        [Column("B_StartDate")]
        public DateOnly StartDate { get; set; } // Stocke les dates
        [Column("B_StartTime")]
        public TimeOnly StartTime { get; set; } // Stocke les heures
        [ForeignKey("ServiceId")]
        public Guid ServiceId { get; set; }
        public ServiceModel Service { get; set; }
    }
}
