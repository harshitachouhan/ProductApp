using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProductApp.Data;
using ProductApp.Models;

namespace ProductApp.Pages.Products
{
    public class ListModel : PageModel
    {
        private readonly DotNetAppContext _context;

        public ListModel(DotNetAppContext context)
        {
            _context = context;
        }

        public IList<Product> Products { get; set; } = new List<Product>();

        [BindProperty(SupportsGet = true)]
        public string SearchTitle { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? StartDate { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? EndDate { get; set; }

        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; }

        private const int PageSize = 10;

        public async Task OnGetAsync(int page = 1)
        {
            CurrentPage = page;

            IQueryable<Product> query = _context.Products.AsQueryable();

            // Filtering
            if (!string.IsNullOrWhiteSpace(SearchTitle))
            {
                query = query.Where(p => p.Title.Contains(SearchTitle));
            }

            if (StartDate.HasValue)
            {
                query = query.Where(p => p.Date >= StartDate.Value);
            }

            if (EndDate.HasValue)
            {
                query = query.Where(p => p.Date <= EndDate.Value);
            }

            // Pagination
            //int totalItems = await query.CountAsync();
            //TotalPages = (int)Math.Ceiling(totalItems / (double)PageSize);

            //Products = await query
            //    .OrderBy(p => p.Date)
            //    .Skip((CurrentPage - 1) * PageSize)
            //    .Take(PageSize)
            //    .ToListAsync();

            Products = query.ToList();
        }
    }
}
