using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MADARAS_IOANA_Lab2.Models;

namespace MADARAS_IOANA_Lab2.Data
{
    public class MADARAS_IOANA_Lab2Context : DbContext
    {
        public MADARAS_IOANA_Lab2Context (DbContextOptions<MADARAS_IOANA_Lab2Context> options)
            : base(options)
        {
        }

        public DbSet<MADARAS_IOANA_Lab2.Models.Book> Book { get; set; } = default!;
        public DbSet<MADARAS_IOANA_Lab2.Models.Publisher> Publisher { get; set; } = default!;
        public DbSet<MADARAS_IOANA_Lab2.Models.Author> Author { get; set; } = default!;
    }
}
