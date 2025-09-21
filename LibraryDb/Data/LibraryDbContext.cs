using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Data
{
    public class LibraryDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5433;Database=library_db;Username=user;Password=1234");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().ToTable("Books");
        }
    }

    public class Book
    {
        public int Id { get; set; }              // Первичный ключ
        public string Title { get; set; }        // Название книги
        public string Author { get; set; }       // Автор
        public int YearPublished { get; set; }   // Год издания
    }
}