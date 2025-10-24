using MADARAS_IOANA_Lab2.Data;
using MADARAS_IOANA_Lab2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MADARAS_IOANA_Lab2.Pages.Books
{
    public class DetailsModel : BookCategoriesPageModel
    {
        private readonly MADARAS_IOANA_Lab2.Data.MADARAS_IOANA_Lab2Context _context;

        public DetailsModel(MADARAS_IOANA_Lab2.Data.MADARAS_IOANA_Lab2Context context)
        {
            _context = context;
        }

        public Book Book { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            var book = await _context.Book
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .Include(b => b.BookCategories).ThenInclude(b => b.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);

            if (book == null)
                return NotFound();

            Book = book;

            PopulateAssignedCategoryData(_context, book);

            ViewData["PublisherID"] = new SelectList(_context.Publisher, "ID", "PublisherName");
            ViewData["AuthorID"] = new SelectList(_context.Author, "ID", "FullName");

            return Page();
        }
    }
}
