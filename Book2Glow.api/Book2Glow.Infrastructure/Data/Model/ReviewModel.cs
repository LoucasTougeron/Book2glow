using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book2Glow.Infrastructure.Data.Model
{
    [Table("Review")]
    public class ReviewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("R_Id")]
        public Guid Id { get; set; }
        [Column("R_Stars")]
        public int stars { get; set; }
        [Column("R_Comments")]
        public string comments { get; set; }
        [Column("R_Datetime")]
        public DateOnly DateTime { get; set; }
        [Required]
        [Column("R_BookId")]
        public Guid BookId { get; set; }

        [ForeignKey("BookId")]
        public BookModel Book { get; set; }
    }
}
