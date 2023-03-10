using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Text.Json.Serialization;

namespace Danskebank_API.Entities
{
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int TypeID { get; set; }
        public int SubtypeID { get; set; }

        // Navigation properties
        [JsonIgnore]
        public virtual ProductType Type { get; set; }
        [JsonIgnore]
        public virtual ProductSubtype Subtype { get; set; }
    }
}
