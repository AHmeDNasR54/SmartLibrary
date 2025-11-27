using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLibrary.Models.DTOs
{
    public class FavoriteDto
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
    }
}
