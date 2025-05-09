using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book2Glow.Infrastructure.Data.Model
{
    [Table("Business")]
    public class BusinessModel
    {
        [Key] // Clé primaire
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto-généré par la base
        [Column("B_Id")] // Nom de la colonne en base
        public Guid Id { get; set; }

        [Column("B_Name")]
        public string Name { get; set; }

        [Column("B_City")]
        public string City { get; set; }

        [Column("B_PostalCode")]
        public string PostalCode { get; set; }

        [Column("B_Latitude")]
        public string Latitude { get; set; }

        [Column("B_Longitude")]
        public string Longitude { get; set; }

        [Column("B_Country")]
        public string Country { get; set; }

        [Column("B_Phone")]
        public string Phone { get; set; }

        [Column("B_Website")]
        public string Website { get; set; }

        [Column("B_UserId")]
        public string? ApplicationUserId { get; set; }

        // Navigation vers l'utilisateur (créateur)
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser? Creator;

        public ICollection<CategoryModel> Categories { get; set; } = new List<CategoryModel>();
    }
}
