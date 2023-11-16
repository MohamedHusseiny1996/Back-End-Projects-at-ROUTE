using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmazonWebSitePL.Models
{
    public class OrdersViewModel
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }
        [MaxLength(40),MinLength(15) ,Required(ErrorMessage ="Name is required with minLength(15)")]
        public string Name { get; set; }

        [Required(ErrorMessage = "This field is required "),RegularExpression("^[1-9]{1}[0-9]{0,4}")]
        public int CountOfItems { get; set; }

        [Required(ErrorMessage = "This field is required with 11 digits "),RegularExpression("^[0-9]{11}")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "This field is required ")]
        public string Address { get; set; } 
        public int ItemID { get; set; }
        public DateTime DateTime { get; set; }= DateTime.Now;
        


    }
}
