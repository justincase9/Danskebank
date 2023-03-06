using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Danskebank_API.Entities
{
    public class ProductType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ProductTypeID { get; set; }
        public string Name { get; set; }

        // Navigation property
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<ProductSubtype> ProductSubtypes { get; set; }
    }
}
