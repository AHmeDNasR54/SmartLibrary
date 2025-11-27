using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLibrary.Models.Repositories
{
    public interface IFavoriteRepository: IRepository<Favorite>
    {
        Task AddFavoriteAsync(Favorite favorite);
        void Update(Favorite favorite);
        Task<bool> ExistsAsync(string userId, int bookId);
        Task<IEnumerable<Favorite>> GetUserFavoritesAsync(string userId);
        Task<Favorite?> GetFavoriteAsync(string userId, int bookId);
    }
}
