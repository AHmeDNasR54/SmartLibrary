
using Microsoft.EntityFrameworkCore;
using SmartLibrary.DataAccess.Data;
using SmartLibrary.DataAccess.Implemenatation;
using SmartLibrary.Models;
using SmartLibrary.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLibrary.DataAccess.Implementation
{

    public class FavoriteRepository: Repository<Favorite>  ,IFavoriteRepository
    {
        private readonly ApplicationDbContext _context;

        public FavoriteRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddFavoriteAsync(Favorite favorite)
        {
            await _context.Favorites.AddAsync(favorite);
        }

        public void Update(Favorite favorite)
        {
            _context.Favorites.Update(favorite);
        }

        public async Task<bool> ExistsAsync(string userId, int bookId)
        {
            return await _context.Favorites
                .AnyAsync(f => f.UserId == userId && f.BookId == bookId);
        }

        public async Task<IEnumerable<Favorite>> GetUserFavoritesAsync(string userId)
        {
            return await _context.Favorites
                .Include(f => f.Book)
                .Where(f => f.UserId == userId)
                .ToListAsync();
        }

        public async Task<Favorite?> GetFavoriteAsync(string userId, int bookId)
        {
            return await _context.Favorites
                .FirstOrDefaultAsync(f => f.UserId == userId && f.BookId == bookId);
        }
    }
}

