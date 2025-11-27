using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLibrary.Models.DTOs
{
        public class UserProfileDto
        {
            public string Id { get; set; }
            public string FullName { get; set; }
            public string Email { get; set; }

            public List<BorrowDetailsDto> BorrowHistory { get; set; }
            public List<FavoriteDto> FavoriteBooks { get; set; }
        }

    
}
