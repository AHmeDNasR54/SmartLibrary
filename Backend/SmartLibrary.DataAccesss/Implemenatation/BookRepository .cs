
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
        public async Task<List<Book>> SearchBooksByTitleAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return new List<Book>();

            
            return await _context.Books
    .Where(b => EF.Functions.Like(b.Title, $"%{searchTerm}%"))
    .OrderByDescending(b => EF.Functions.Like(b.Title, $"{searchTerm}%"))
    .ThenBy(b => b.Title)
    .AsNoTracking()
    .ToListAsync();
            /*
                                    DB - friendly

                        أسرع

                        تقدر تستخدم index(جزئيًا)

                         دي النسخة اللي تحطها في CV

                         Bonus: ترتيب النتائج بذكاء

                        خلي:

                        اللي العنوان بيبدأ بالكلمة يطلع الأول

                        بعده اللي بس يحتوي عليها
                                    */
        }


    }
}
