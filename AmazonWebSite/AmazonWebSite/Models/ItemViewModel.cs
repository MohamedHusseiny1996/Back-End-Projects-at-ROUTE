using AmazonWebSiteDAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace AmazonWebSitePL.Models
{
    public class ItemViewModel
    {
        [Key]
        public virtual int Id { get; set; }

        [Required(ErrorMessage = "this item is required with MaxLength(20) & MinLength(5)"), MaxLength(20), MinLength(5)]
        public virtual string Name { get; set; }

        [Required(ErrorMessage = "this item is required with max five digits"), DataType(DataType.Currency), RegularExpression("^[0-9]{1,5}$")]
        public virtual double Price { get; set; }

        [Required(ErrorMessage = "this item is required with MaxLength(30) & MinLength(4)"), MaxLength(30), MinLength(4)]
        public virtual string Category { get; set; }

        [Required(ErrorMessage = "this item is required")]
        public virtual int UnitsInStock { get; set; }
        public virtual string? Description { get; set; }

        public virtual ImageViewModel image { get; set; }
    }
}
