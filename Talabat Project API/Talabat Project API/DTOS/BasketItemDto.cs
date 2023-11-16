using System.ComponentModel.DataAnnotations;

namespace Talabat_Project_API.DTOS
{
	public class BasketItemDto
	{
		[Required]
		public int Id { get; set; }
		[Required]
		public string ProductName { get; set; }
		[Required]
		public string PictureUrl { get; set; }
		[Required , Range(0.1,double.MaxValue, ErrorMessage ="price can't be zero ")]
		public decimal Price { get; set; }
		[Required, Range(1, int.MaxValue, ErrorMessage = "Quantity can't be zero ")]
		public int Quantity { get; set; }
		[Required]
		public string brand { get; set; }
		[Required]
		public string type { get; set; }
	}
}
