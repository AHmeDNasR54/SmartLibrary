
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

    public class BookRepository: Repository<Book>  ,IBooksRepository
    {
        private readonly ApplicationDbContext _context;
        public BookRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Book book)
        {
            var BookInDb = _context.Categories.FirstOrDefault(x => x.Id == book.Id);
            if (BookInDb != null)
            {
                _context.Books.Update(book);

            }
        }
    }
}
