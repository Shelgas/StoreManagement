using System;
using SM.Application.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Infrastructure.Repository
{
    internal class RepositoryWrapper : IRepositoryWrapper

    {
        private CategoryRepository? _categoryRepository;
        private readonly AppDbContext _appDbContext;

        public ICategoryRepository CategoryRepository => _categoryRepository ??= new CategoryRepository(_appDbContext);



        public RepositoryWrapper(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task SaveAsync()
        {
            await _appDbContext.SaveChangesAsync();
        }
    }
}
