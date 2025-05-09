using System.ComponentModel.DataAnnotations;

namespace MyProject.Models.ProductModel
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name can't be longer than 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Category is required")]
        [StringLength(50, ErrorMessage = "Category can't be longer than 50 characters")]
        public string Category { get; set; }

        [Required(ErrorMessage = "Image URL is required")]
        [Url(ErrorMessage = "Please enter a valid URL")]
        public string Image { get; set; }

      
        public int NewPrice { get; set; }

        
        public int OldPrice { get; set; }

        [StringLength(1000, ErrorMessage = "Description can't exceed 1000 characters")]
        public string Description { get; set; }
        public int  Quantity { get; set; }

        [Range(0, 5, ErrorMessage = "Rating must be between 0 and 5")]
        public double Rating { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Reviews must be a non-negative number")]
        public int Reviews { get; set; }



       
    }
}
