using System.ComponentModel.DataAnnotations;

namespace ProductsApi.Dtos
{
    public record ProductRequestDto {

      [Required(ErrorMessage ="The name is required XD")]
      public string Name { get; set; }

      [Required(ErrorMessage = "The Description is required ")]        
      public string Description { get; set; }

      [Required(ErrorMessage = "The Price is required ")]
      [RegularExpression(@"^\d+.\d{0,2}$", ErrorMessage = "Price can't have more than 2 decimal places")]
      public decimal Price { get; set; }


    };

    public record ProductResponseDto {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string SKU { get; set; }
    }


}
