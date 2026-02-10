using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLibrary.Models.DTOs
{
    public class FavoriteDetailsDto
    {
        public int Id { get; set; }
        public BookGetDto bookDto { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
    }
}
