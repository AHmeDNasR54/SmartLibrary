using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLibrary.Models.Repositories
{
    public interface IBooksRepository: IRepository<Book>
    {
         void Update(Book book);
    }
}
