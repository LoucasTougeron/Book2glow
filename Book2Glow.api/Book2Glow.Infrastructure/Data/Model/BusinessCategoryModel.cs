using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book2Glow.Infrastructure.Data.Model
{
    public class BusinessCategoryModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("BC_Id")]
        public Guid Id { get; set; }

        [Column("BC_BusinessId")]
        public Guid BusinessId { get; set; }

        [ForeignKey("BusinessId")]
        public BusinessModel Business { get; set; }

        [Column("BC_CategoryId")]
        public Guid CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public CategoryModel Category { get; set; }

        // Un BusinessCategory peut avoir plusieurs Services
        public ICollection<ServiceModel> Services { get; set; } = new List<ServiceModel>();
    }
}

