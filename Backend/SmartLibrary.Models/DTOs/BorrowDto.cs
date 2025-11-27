using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLibrary.Models.DTOs
{
    public class BorrowDto
    {

            public int Id { get; set; }
            public string UserId { get; set; }
            public int BookId { get; set; }
            public DateTime BorrowDate { get; set; }
            public DateTime? ReturnDate { get; set; }
            public bool IsReturned { get; set; }
    }

        

        // لو حابب تعمل DTO خاص بعملية الإنشاء (POST)
        public class CreateBorrowDto
        {
            public int UserId { get; set; }
            public int BookId { get; set; }
        }
 }


