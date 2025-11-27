
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
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private readonly ApplicationDbContext _context;
        public ApplicationUserRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<ApplicationUser> GetUserWithDetailsAsync(string userId)
        {
            return await _context.ApplicationUsers
                .Include(u => u.UserBorrows)
                    .ThenInclude(b => b.Book)
                .Include(u => u.FavoriteBooks)
                    .ThenInclude(f => f.Book)
                .FirstOrDefaultAsync(u => u.Id == userId);
        }


    }
}
