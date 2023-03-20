using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext : IdentityDbContext<UsersPerson>
    {
        public DataContext(DbContextOptions options) : base(options) { }
        public DbSet<Authors> Authors { get; set; }
        public DbSet<Books> Books { get; set; }
        public DbSet<Genres> Genres { get; set; }
        public DbSet<UsersPerson> UserPersons { get; set; }
        public DbSet<Borrows> Borrows { get; set; }
        public DbSet<BooksAuthors> BooksAuthors { get; set; }
        public DbSet<BooksGenres> BooksGenres { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Borrows>()
                .HasOne(b => b.Book)
                .WithMany(b => b.Borrows)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<BooksAuthors>(x =>
            {

                x.HasKey(ba => new { ba.BookId, ba.AuthorId });

                x.HasOne(ba => ba.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(ba => ba.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);

                x.HasOne(ba => ba.Book)
                .WithMany(b => b.Authors)
                .HasForeignKey(ba => ba.BookId);
            });

            builder.Entity<BooksGenres>(bg =>
            {
                bg.HasKey(ba => new { ba.BookId, ba.GenreId });

                bg.HasOne(bg => bg.Genre)
                  .WithMany(a => a.Books)
                  .HasForeignKey(aa => aa.GenreId)
                  .OnDelete(DeleteBehavior.Cascade);

                bg.HasOne(bg => bg.Book)
                  .WithMany(u => u.Genres)
                  .HasForeignKey(aa => aa.BookId);
            });
        }
    }
}
