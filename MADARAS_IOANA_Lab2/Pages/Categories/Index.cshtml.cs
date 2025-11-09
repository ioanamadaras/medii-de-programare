using MADARAS_IOANA_Lab2.Data;
using MADARAS_IOANA_Lab2.Models;
using MADARAS_IOANA_Lab2.Models.ViewModels;
using MADARAS_IOANA_Lab2.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MADARAS_IOANA_Lab2.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly MADARAS_IOANA_Lab2.Data.MADARAS_IOANA_Lab2Context _context;

        public IndexModel(MADARAS_IOANA_Lab2.Data.MADARAS_IOANA_Lab2Context context)
        {
            _context = context;
        }

        public CategoryIndexData CategoryData { get; set; }
        public int CategoryID { get; set; }



        public IList<Category> Category { get;set; } = default!;

        public async Task OnGetAsync(int? id, int? bookID)
        {

            CategoryData = new CategoryIndexData();
            CategoryData.Books = await _context.Book
                .Include(b => b.Author)
                .Include(b => b.BookCategories)
                .ToListAsync();

            CategoryData.Categories= await _context.Category
            .Include(c => c.BookCategories)
            .OrderBy(b => b.CategoryName)
            .ToListAsync();
            if (id != null)
            {
                CategoryID = id.Value;
                Category category = CategoryData.Categories
                    .Where(i => i.ID == id.Value).Single();
                CategoryData.Books = category.BookCategories
                    .Select(bc => bc.Book)
                    .ToList();

                Category = CategoryData.Categories.ToList();

            }
        }
    }
}
