using AmazonWebSiteDAL.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AmazonWebSitePL.Models
{
    public class ImageViewModel
    {
        [Key]
        public virtual int Id { get; set; }


        [NotMapped, Required(ErrorMessage = "this field is required")]
        public virtual IFormFile ItemImage { get; set; }
        public virtual string Path { get; set; }

        [ForeignKey("Item")]
        public virtual int? ItemID { get; set; }//nullable to avoid cascading when delete(restrict behavior is on)
        public virtual ItemViewModel? Item { get; set; }
    }
}
