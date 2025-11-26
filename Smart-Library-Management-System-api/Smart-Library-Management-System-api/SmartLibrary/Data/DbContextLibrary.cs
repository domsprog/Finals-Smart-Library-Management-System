using Microsoft.EntityFrameworkCore;
using Smart_Library_Management_System_api.SmartLibrary.Entities;

namespace Smart_Library_Management_System_api.SmartLibrary.Data
{
    public class DbContextLibrary : DbContext
    {
        public DbContextLibrary(DbContextOptions<DbContextLibrary> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<Fine> Fines { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Catalog> Catalogs { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Faculty> Faculties { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User inheritance mapping (TPH)
            modelBuilder.Entity<User>()
                .HasDiscriminator<string>("UserType")
                .HasValue<Student>("Student")
                .HasValue<Faculty>("Faculty");

            // Book
            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(e => e.ISBN);
                entity.Property(e => e.ISBN).HasMaxLength(20);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Author).HasMaxLength(100);
                entity.Property(e => e.Price).HasColumnType("decimal(10,2)");
            });

            // Loan
            modelBuilder.Entity<Loan>(entity =>
            {
                entity.HasKey(e => e.LoanId);
                entity.Property(e => e.FineAmount).HasColumnType("decimal(10,2)");
            });

            // Fine
            modelBuilder.Entity<Fine>(entity =>
            {
                entity.HasKey(e => e.FineId);
                entity.Property(e => e.Amount).HasColumnType("decimal(10,2)");
            });

            // Seed Book
            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    ISBN = "978-0134685991",
                    Title = "Effective of Java-rice",
                    Author = "Dominic Alilin",
                    Publisher = "Julia Kong",
                    PublicationYear = 2018,
                    Category = "Programming",
                    Price = 1299.00m,
                    TotalCopies = 5,
                    AvailableCopies = 5
                });
        }
    }
}
