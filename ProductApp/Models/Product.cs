using System.ComponentModel.DataAnnotations;

namespace ProductApp.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Title { get; set; }

        [Required]
        [MaxLength(250)]
        public string? Description { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public byte[] ImageData { get; set; }    // To store image bytes
        public string ImageName { get; set; }    // To store the file name

        [Required]
        public decimal Price { get; set; } // Assuming Price is a decimal
    }
}
