using System.ComponentModel.DataAnnotations;

namespace InventoryApi_Service.Dtos
{
    public record InventoryRequestDto
    {

        [Required(ErrorMessage = "The ProductSKU is Required")]
        public string ProductSKU { get; set; }

        [Required(ErrorMessage = "The Stock is Required")]
        public int Stock { get; set; }

    }



    public record InventoryResponseDto
    {
        public int Id { get; set; }
        public string ProductSKU { get; set; }


        public int Stock { get; set; }

    }


}
