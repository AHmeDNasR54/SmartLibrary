
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

    public class CategoryRepository: Repository<Category>  ,ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Category category)
        {
            var CategoryInDb = _context.Categories.FirstOrDefault(x => x.Id == category.Id);
            if (CategoryInDb != null)
            {
                CategoryInDb.Name = category.Name;
                
            }
        }
    }
}
