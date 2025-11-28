using Models.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Technova_ecom.Models.Entities
{
    [Table("Category")]
    public class Category
    {
        [Column("category_id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }
        [Column("name")]
        [Display(Name = "Catagory Name")]
        public string CategoryName { get; set; }
        [Column("display_order")]
        [Display(Name = "Order")]
        public int DisplayOrder { get; set; }

        public ICollection<Product>? Products { get; set; }
    }
}
