using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SM.Application.Interfaces;
using SM.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Infrastructure
{
    public static class InfrastructureDI
    {
        public static IServiceCollection AddInfastructure(this IServiceCollection services,
            IConfiguration conf)
        {
            var confString = conf.GetConnectionString("DefaultConnection");
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(confString);
            });
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            return services;
        }
    }
}
