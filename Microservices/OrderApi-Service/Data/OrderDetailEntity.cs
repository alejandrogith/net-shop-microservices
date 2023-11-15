using System.ComponentModel.DataAnnotations.Schema;

namespace OrdersApi.Data
{

    [Table("OrderDetail")]
    public class OrderDetailEntity
    {
        public int Id { get; set; }
        public string ProductSKU { get; set; }
        public decimal Cost { get; set; }

        [ForeignKey("OrderMasterEntity")]
        public int OrderMasterId { get; set; }
        public OrderMasterEntity OrderMaster { get; set; }

    }
}
