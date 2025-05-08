using System.ComponentModel.DataAnnotations;

namespace MyProject.Models.Product
{
    public class ProductDto
    {
        public string Name { get; set; }

        [Required(ErrorMessage = "Category is required")]
        [StringLength(50, ErrorMessage = "Category can't be longer than 50 characters")]
        public string Category { get; set; }

        [Required(ErrorMessage = "Image URL is required")]
        [Url(ErrorMessage = "Please enter a valid URL")]
        public string Image { get; set; }

        [Required(ErrorMessage = "New price is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "New price must be greater than 0")]
        public decimal NewPrice { get; set; }

        [Range(0.00, double.MaxValue, ErrorMessage = "Old price must be 0 or more")]
        public decimal OldPrice { get; set; }

        [StringLength(1000, ErrorMessage = "Description can't exceed 1000 characters")]
        public string Description { get; set; }

        [Range(0, 5, ErrorMessage = "Rating must be between 0 and 5")]
        public double Rating { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Reviews must be a non-negative number")]
        public int Reviews { get; set; }
    }
}
