using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SmartLibrary.Models
{
    public class ApplicationUser:IdentityUser
    {
        [Required, MaxLength(50)]
        public string FirstName { get; set; }

        [Required, MaxLength(50)]
        public string LastName { get; set; }
        // Navigation property for borrowed books
        public ICollection<Borrow> UserBorrows { get; set; } = new List<Borrow>();

        // Favorite Books
        public ICollection<Favorite> FavoriteBooks { get; set; } = new List<Favorite>();
    }
}
