using Microsoft.EntityFrameworkCore;
using SmartLibrary.DataAccess.Data;
using SmartLibrary.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLibrary.DataAccess.Implementation
{
    public class UnitOfWork : IunitOfWork
    {
        public readonly ApplicationDbContext _context;
        public ICategoryRepository Categories { get; private set; }

        public IBooksRepository Books {  get; private set; }

        public IBorrowRepository Borrows { get; private set; }
        
        public IApplicationUserRepository ApplicationUsers { get; private set; }

        public IFavoriteRepository Favorites { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Categories = new CategoryRepository(_context);
            Books = new BookRepository(_context);
            Borrows = new BorrowRepository(_context);
            ApplicationUsers = new ApplicationUserRepository(_context);
            Favorites = new FavoriteRepository(_context);

        }


        public async Task<int> complete()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
