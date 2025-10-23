using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLibrary.Models.Repositories
{
    public interface IunitOfWork:IDisposable
    {
        ICategoryRepository Categories { get; }
        IBooksRepository Books { get; }
        IBorrowRepository Borrows{ get; }
        IApplicationUserRepository ApplicationUsers { get; }
        Task<int> complete();
    }
}
