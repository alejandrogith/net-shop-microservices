using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryApi_Service.Data
{
    [Table("Inventory")]
    public class InventoryEntity
    {
        [Key]
        public int Id { get; set; }
        public string ProductSKU { get; set; }


        public int Stock { get; set; }
    }
}
