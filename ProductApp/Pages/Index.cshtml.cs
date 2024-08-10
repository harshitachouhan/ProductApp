using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using ProductApp.Data;
using ProductApp.Models;
namespace ProductApp.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly DotNetAppContext _context;

    public IndexModel(ILogger<IndexModel> logger, DotNetAppContext context)
    {
        _logger = logger;
        _context = context;
    }

    [BindProperty]
    public List<ProductInputModel> Products { get; set; } = new List<ProductInputModel>();

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        try
        {
            var productEntities = Products.Select(async p =>
            {
                var product = new Product
                {
                    Title = p.Title,
                    Description = p.Description,
                    Quantity = p.Quantity,
                    Date = p.Date,
                    Price = p.Price
                };

                if (p.ImageFile != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await p.ImageFile.CopyToAsync(memoryStream);
                        product.ImageData = memoryStream.ToArray();
                        product.ImageName = p.ImageFile.FileName;
                    }
                }

                return product;
            }).Select(t => t.Result).ToList();

            await _context.Products.AddRangeAsync(productEntities);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Products/List");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            ModelState.AddModelError(string.Empty, "An error occurred while saving the products.");
            return Page();
        }
    }

    public class ProductInputModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }
        public decimal Price { get; set; }

        // Add this property for file uploads
        public IFormFile ImageFile { get; set; }
    }
}
