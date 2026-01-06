using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLibrary.Models.Services
{
    public interface IRecommendationService
    {
        Task<List<Book>> GetRecommendedBooksAsync(int days = 30, int count = 5);
    }
}
