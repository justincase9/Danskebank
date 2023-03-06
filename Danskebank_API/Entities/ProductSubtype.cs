using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Danskebank_API.Entities
{
    public class ProductSubtype
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int SubtypeID { get; set; }
        public string Name { get; set; }
        public int TypeID { get; set; }

        // Navigation property
        public virtual ProductType Type { get; set; }
        public virtual ICollection<Product> Products { get; set; }

    }
}
