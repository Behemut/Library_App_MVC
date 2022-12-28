using Library_API.Models;
using Library_API.Models.Auth;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Library_API.DBContext
{
    public class ApplicationDBContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            
            base.OnModelCreating(builder);

            //builder.Entity<Books>()
            //    .HasMany<Genres>(b => b.Genres)
            //    .WithMany(g => g.Books)
            //    .UsingEntity(j => j.ToTable("BookGenres"));

            //builder.Entity<Books>()
            //    .HasMany<Authors>(b => b.Authors)
            //    .WithMany(a => a.Books)  
            //    .UsingEntity(j => j.ToTable("BookAuthors"));


            builder.Entity<Borrows>().HasKey(b => new { b.BookId, b.UserId });
     
            builder.Entity<Borrows>()
                .HasOne(b => b.Book)
                .WithMany(b => b.Borrows)
                .HasForeignKey(b => b.BookId);

            builder.Entity<Borrows>()
                .HasOne(b => b.User)
                .WithMany(u => u.Borrows)
                .HasForeignKey(b => b.UserId);

            builder.Entity<BooksAuthors>().HasKey(b => new { b.BookId, b.AuthorId });

            builder.Entity<BooksAuthors>()
                .HasOne(b => b.Book)
                .WithMany(b => b.BooksAuthors)
                .HasForeignKey(b => b.BookId);
            
            builder.Entity<BooksAuthors>()
                .HasOne(b => b.Author)
                .WithMany(a => a.BooksAuthors)
                .HasForeignKey(b => b.AuthorId);


            builder.Entity<BooksGenres>().HasKey(b => new { b.BookId, b.GenreId });

            builder.Entity<BooksGenres>()
                .HasOne(b => b.Book)
                .WithMany(b => b.BooksGenres)
                .HasForeignKey(b => b.BookId);

            builder.Entity<BooksGenres>()
                .HasOne(b => b.Genre)
                .WithMany(g => g.BooksGenres)
                .HasForeignKey(b => b.GenreId);


        }

        
        public DbSet<Authors> Authors { get; set; }

        public DbSet<Books> Books { get; set; }

        public DbSet<Genres> Genres { get; set; }

        public DbSet<UsersPerson> UserPersons { get; set; }
        public DbSet<Borrows> Borrows { get; set; }
    }
}
