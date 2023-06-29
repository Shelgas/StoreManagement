using Microsoft.EntityFrameworkCore;
using SM.Application.Interfaces;
using SM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Infrastructure.Repository
{
    internal class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {

        public CategoryRepository(AppDbContext _context) : base(_context)
        {

        }

        public async Task<IEnumerable<Category>> GetAllCategorysAsync()
        {
            return await FindAll().ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await FindByCondition(category => category.Id == id).FirstOrDefaultAsync();
        }
    }
}
