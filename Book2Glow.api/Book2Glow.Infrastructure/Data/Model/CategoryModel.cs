using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book2Glow.Infrastructure.Data.Model
{
    [Table("Category")]
    public class CategoryModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("C_Id")]
        public Guid Id { get; set; }
        [Column("C_Name")]
        public string Name { get; set; }

        public ICollection<BusinessCategoryModel> BusinessCategories { get; set; } = new List<BusinessCategoryModel>();
    }
}
