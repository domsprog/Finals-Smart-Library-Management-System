using Microsoft.EntityFrameworkCore;
using Smart_Library_Management_System_api.SmartLibrary.Entities;

namespace Smart_Library_Management_System_api.SmartLibrary.Data
{
    public class DbContextLibrary : DbContext
    {
        public DbContextLibrary(DbContextOptions<DbContextLibrary> options) : base(options)
        {
        }

        // Tables
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

            // Configure User inheritance (TPH - Table Per Hierarchy)
            modelBuilder.Entity<User>()
                .HasDiscriminator<string>("UserType")
                .HasValue<Student>("Student")
                .HasValue<Faculty>("Faculty");

            // User configuration
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId);
                entity.Property(e => e.UserId).HasMaxLength(50).IsRequired();
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.HasIndex(e => e.Email).IsUnique();
            });

            // Student configuration
            modelBuilder.Entity<Student>(entity =>
            {
                entity.Property(e => e.StudentId).HasMaxLength(50);
                entity.Property(e => e.Department).HasMaxLength(100);
            });

            // Faculty configuration
            modelBuilder.Entity<Faculty>(entity =>
            {
                entity.Property(e => e.FacultyId).HasMaxLength(50);
                entity.Property(e => e.Department).HasMaxLength(100);
                entity.Property(e => e.Position).HasMaxLength(100);
            });

            // Book configuration
            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(e => e.ISBN);
                entity.Property(e => e.ISBN).HasMaxLength(20).IsRequired();
                entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Author).HasMaxLength(100);
                entity.Property(e => e.Publisher).HasMaxLength(100);
                entity.Property(e => e.Category).HasMaxLength(50);
                entity.Property(e => e.Price).HasColumnType("decimal(10,2)");
            });

            // Loan configuration
            modelBuilder.Entity<Loan>(entity =>
            {
                entity.HasKey(e => e.LoanId);
                entity.Property(e => e.LoanId).HasMaxLength(50).IsRequired();
                entity.Property(e => e.UserId).HasMaxLength(50).IsRequired();
                entity.Property(e => e.ISBN).HasMaxLength(20).IsRequired();
                entity.Property(e => e.FineAmount).HasColumnType("decimal(10,2)");

                // Add indexes for common queries
                entity.HasIndex(e => e.UserId);
                entity.HasIndex(e => e.ISBN);
                entity.HasIndex(e => e.ReturnDate);
            });

            // Fine configuration
            modelBuilder.Entity<Fine>(entity =>
            {
                entity.HasKey(e => e.FineId);
                entity.Property(e => e.FineId).HasMaxLength(50).IsRequired();
                entity.Property(e => e.LoanId).HasMaxLength(50).IsRequired();
                entity.Property(e => e.UserId).HasMaxLength(50).IsRequired();
                entity.Property(e => e.Amount).HasColumnType("decimal(10,2)");
                entity.Property(e => e.Reason).HasMaxLength(500);

                // Add indexes
                entity.HasIndex(e => e.UserId);
                entity.HasIndex(e => e.LoanId);
                entity.HasIndex(e => e.IsPaid);
            });

            // Reservation configuration
            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.HasKey(e => e.ReservationId);
                entity.Property(e => e.ReservationId).HasMaxLength(50).IsRequired();
                entity.Property(e => e.UserId).HasMaxLength(50).IsRequired();
                entity.Property(e => e.ISBN).HasMaxLength(20).IsRequired();

                // Add indexes
                entity.HasIndex(e => e.UserId);
                entity.HasIndex(e => e.ISBN);
                entity.HasIndex(e => e.IsActive);
            });

            // Catalog configuration
            modelBuilder.Entity<Catalog>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.CatalogId).HasMaxLength(50).IsRequired();
                entity.Property(e => e.Name).HasMaxLength(200);
                entity.Property(e => e.Category).HasMaxLength(100);
                entity.Property(e => e.Description).HasMaxLength(1000);

                // Configure BookISBNs as a JSON column or use separate table
                entity.Property(e => e.BookISBNs)
                    .HasConversion(
                        v => string.Join(',', v),
                        v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
                    );

                // Add unique constraint on CatalogId
                entity.HasIndex(e => e.CatalogId).IsUnique();
            });

            // Seed data
            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    ISBN = "978-0134685991",
                    Title = "Effective of Java-rice",
                    Author = "Dominic Alilin",
                    PublicationYear = 2018,
                    Publisher = "Julia Kong",
                    Category = "Programming",
                    Price = 1299.00m,
                    TotalCopies = 5,
                    AvailableCopies = 5
                },
                new Book
                {
                    ISBN = "978-0135166307",
                    Title = "Clean Code",
                    Author = "Martin Aranzado",
                    PublicationYear = 2008,
                    Publisher = "Prentice Hall",
                    Category = "Programming",
                    Price = 1499.00m,
                    TotalCopies = 3,
                    AvailableCopies = 3
                },
                new Book
                {
                    ISBN = "978-0132350884",
                    Title = "Clean Architecture",
                    Author = "Roberto jack",
                    PublicationYear = 2017,
                    Publisher = "Prentice Hall",
                    Category = "Software Engineering",
                    Price = 1599.00m,
                    TotalCopies = 4,
                    AvailableCopies = 4
                }
            );

            // Seed sample students
            modelBuilder.Entity<Student>().HasData(
                new
                {
                    UserId = "STU001",
                    Name = "John Doe",
                    Email = "john.doe@university.edu",
                    RegisteredDate = DateTime.Now,
                    StudentId = "2024-001",
                    Department = "Computer Science",
                    UserType = "Student"
                },
                new
                {
                    UserId = "STU002",
                    Name = "Jakee Sucgang",
                    Email = "jane.smith@university.edu",
                    RegisteredDate = DateTime.Now,
                    StudentId = "2024-002",
                    Department = "Information Technology",
                    UserType = "Student"
                }
            );

            // Seed sample faculty
            modelBuilder.Entity<Faculty>().HasData(
                new
                {
                    UserId = "FAC001",
                    Name = "Dr. Alice Johnson",
                    Email = "alice.johnson@university.edu",
                    RegisteredDate = DateTime.Now,
                    FacultyId = "FAC-2020-001",
                    Department = "Computer Science",
                    Position = "Professor",
                    UserType = "Faculty"
                }
            );
        }
    }
}