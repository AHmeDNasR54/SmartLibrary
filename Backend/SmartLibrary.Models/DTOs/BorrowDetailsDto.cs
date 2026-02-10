using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLibrary.Models.DTOs
{
    public class BorrowDetailsDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        //public int BookId { get; set; }
        public BookGetDto BookDto { get; set; }
        public string UserName { get; set; }
        //public string BookTitle { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool IsReturned { get; set; }
    }
}
