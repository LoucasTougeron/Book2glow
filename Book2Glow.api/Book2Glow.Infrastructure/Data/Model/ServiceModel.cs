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
        [Column("S_Id")] 
        public Guid Id { get; set; }
        [Column("S_Duration")] 
        public int duration { get; set; } 
        [Column("S_Amount")] 
        public float amount { get; set; }
        [Column("S_Name")] 
        public string name { get; set; }

        [Column("S_BusinessCategoryId")]
        public Guid BusinessCategoryId { get; set; }

        [ForeignKey("BusinessCategoryId")]
        public BusinessCategoryModel? BusinessCategory { get; set; }

        public ICollection<BookingModel> Bookings { get; set; } = new List<BookingModel>();


    }
}
