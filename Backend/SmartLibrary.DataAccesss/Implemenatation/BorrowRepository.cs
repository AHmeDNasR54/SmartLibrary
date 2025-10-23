
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

    public class BorrowRepository: Repository<Borrow>  ,IBorrowRepository
    {
        private readonly ApplicationDbContext _context;
        public BorrowRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Borrow borrow)
        {
            var BorrowInDb = _context.Categories.FirstOrDefault(x => x.Id == borrow.Id);
            if (BorrowInDb != null)
            {
                _context.Borrows.Update(borrow);

            }
        }
        public void AddBorrow(Borrow borrow)
        {
            borrow.BorrowDate = DateTime.Now;

            borrow.ReturnDate = borrow.BorrowDate.AddDays(3);
            borrow.IsReturned = false;
        }
    }
}
