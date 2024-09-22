using Application.Common.Interfaces;
using Infrastructure.Common.Factories;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("SQLConnection");

            // DB context
            services.AddDbContext<ApplicationDbContext>((sp, options) =>
            {
                options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
                options.UseSqlServer(connectionString);
            });

            services.AddTransient<IDbContextFactory, ApplicationDbContextFactory>(s =>
            {
                var connectionString = configuration.GetSection("ConnectionStrings:SQLConnection").Value;
                var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer(
                    connectionString,
                    b =>
                    {
                        b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                        b.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                    })
                    .Options;
                return new ApplicationDbContextFactory(options);
            });

            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

            services.AddSingleton(TimeProvider.System);

            return services;
        }
    }
}
