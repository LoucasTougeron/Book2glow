using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Book2Glow.Infrastructure.Data.Model
{
    [Table("Book")]
    public class BookModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Book_Id")]
        public Guid Id { get; set; }

        [Required]
        [Column("Book_UserId")]
        public string ApplicationUserId { get; set; }

        [ForeignKey("ApplicationUserId")]
        public ApplicationUser User { get; set; }

        [Required]
        [Column("Book_BusinessId")]
        public Guid BusinessId { get; set; }

        [ForeignKey("BusinessId")]
        public BusinessModel Business { get; set; }

        [Required]
        [Column("Book_BookingId")]
        public Guid BookingId { get; set; }

        [ForeignKey("BookingId")]
        public BookingModel Booking { get; set; }
        [JsonIgnore]
        public ICollection<ReviewModel> Reviews { get; set; } = new List<ReviewModel>();

    }
}
