using Microsoft.EntityFrameworkCore;
using SM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { } 
        


    }
}
