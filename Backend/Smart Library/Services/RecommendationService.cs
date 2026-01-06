using Microsoft.EntityFrameworkCore;
using SmartLibrary.DataAccess.Data;
using SmartLibrary.Models;
using SmartLibrary.Models.Services;

namespace SmartLibrary.Models.Services
{
    public class RecommendationService : IRecommendationService
    {
        private readonly ApplicationDbContext _context;

        public RecommendationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Book>> GetRecommendedBooksAsync(int days = 30, int count = 5)
        {
            var fromDate = DateTime.UtcNow.AddDays(-days);

            // Step 1: get top borrowed books in the last 'days'
            var topBorrowedBooks = await _context.Borrows
                .Where(b => b.BorrowDate >= fromDate)
                .GroupBy(b => b.BookId)
                .OrderByDescending(g => g.Count())
                .Take(count)
                .Select(g => g.First().Book)
                .AsNoTracking()
                .ToListAsync();

            // Step 2: if less than 'count', fill with other books
            if (topBorrowedBooks.Count < count)
            {
                var additionalBooks = await _context.Books
                    .Where(b => !topBorrowedBooks.Select(tb => tb.Id).Contains(b.Id))
                    .Take(count - topBorrowedBooks.Count)
                    .AsNoTracking()
                    .ToListAsync();

                topBorrowedBooks.AddRange(additionalBooks);
            }

            return topBorrowedBooks;
        }
    }
}
