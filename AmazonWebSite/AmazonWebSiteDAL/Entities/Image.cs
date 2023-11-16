using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonWebSiteDAL.Entities
{
    public class Image
    {
        [Key]
        public virtual int Id { get; set; }
        
        
        [NotMapped,Required(ErrorMessage = "this field is required")]
        public virtual IFormFile ItemImage {  get; set; }
        public virtual string Path { get; set; }

        [ForeignKey("Item")]
        public virtual int? ItemID { get; set; }//nullable to avoid cascading when delete(restrict behavior is on)
        public virtual Items? Item { get; set; }
    }
}
