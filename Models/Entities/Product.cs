using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entities
{
    public class Product
    {
        [Column("product_id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }
        [Column("name")]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }
        [Column("price")]
        [Display(Name = "Price")]
        public float ProductPrice { get; set; }
        [Column("description")]
        [Display(Name = "Description")]
        public string ProductDescription { get; set; }
        [Column("quantity")]
        [Display(Name = "Quantity")]

        public int ProductQuantity { get; set; }
        [Column("rating")]
        [Display(Name = "Rating")]

        public string ProductRating { get; set; }
    }
}
