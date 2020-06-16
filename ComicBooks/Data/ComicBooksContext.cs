using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ComicBooks.Models;

namespace ComicBooks.Data
{
    public class ComicBooksContext : DbContext
    {
        public ComicBooksContext (DbContextOptions<ComicBooksContext> options)
            : base(options)
        {
        }

        public DbSet<ComicBooks.Models.Genre> Genre { get; set; }

        public DbSet<ComicBooks.Models.ComicBook> ComicBook { get; set; }
    }
}
