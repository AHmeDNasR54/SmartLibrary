using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SmartLibrary.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Author { get; set; } = null!;
        public string? Description { get; set; }
        public string? CategoryName { get; set; }
        public int TotalCopies { get; set; }
        public int AvailableCopies { get; set; }
        public double Price { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Timestamp]
        public byte[]? RowVersion { get; set; }

        public string? ImageUrl { get; set; } // IMage property

        public int categoryId { get; set; }
        
        [JsonIgnore]
        public Category? category { get; set; }
        // Navigation property for borrowed books
        public ICollection<Borrow> UserBorrows { get; set; } = new List<Borrow>();
        public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();

    }
}
