using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrdersApi.Data
{

    [Table("OrderMaster")]
    public class OrderMasterEntity
    {
        [Key]
        public int Id { get; set; }

        public string EmailUser { get; set; }

        public decimal TotalCost { get; set; }

        public DateTime? CreatedAt { get; set; }


        public List<OrderDetailEntity> OrderDetailEntity { get; set; }
    }
}
