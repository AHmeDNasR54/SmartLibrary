using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLibrary.Models
{
    public  class Category
    {
            [Key]
            public int Id { get; set; }

            [MaxLength(100)]
            public string Name { get; set; } = string.Empty;

            // Concurrency check
            [Timestamp]
            public byte[]? RowVersion { get; set; }
        
    }
}
