using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace SmartLibrary.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; } = null!;
        public DbSet<Borrow> Borrows { get; set; } = null!;

        public DbSet<ApplicationUser> ApplicationUsers { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Book>()
                .Property(b => b.RowVersion)
                .IsRowVersion()
                .IsConcurrencyToken();

            builder.Entity<Borrow>()
                .HasOne(ub => ub.User)
                .WithMany(u => u.UserBorrows)
                .HasForeignKey(ub => ub.UserId);

            builder.Entity<Borrow>()
                .HasOne(ub => ub.Book)
                .WithMany(b => b.UserBorrows)
                .HasForeignKey(ub => ub.BookId);
        }
    }
}
