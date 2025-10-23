using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLibrary.Models
{
    public class Borrow
    {
        public int Id { get; set; }
        public string UserId { get; set; }             
        public int BookId { get; set; }            
        public DateTime BorrowDate { get; set; } = DateTime.Now;
        public DateTime? ReturnDate { get; set; }
        public bool IsReturned { get; set; } = false;
        public Book? Book { get; set; }
        public ApplicationUser? User { get; set; }   // if you have a User entity
    }
}
