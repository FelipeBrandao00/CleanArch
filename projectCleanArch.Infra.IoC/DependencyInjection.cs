using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using projectCleanArch.Application.Interfaces;
using projectCleanArch.Application.Mappings;
using projectCleanArch.Application.Services;
using projectCleanArch.Domain.Account;
using projectCleanArch.Domain.Interfaces;
using projectCleanArch.Infra.Data.Context;
using projectCleanArch.Infra.Data.Identity;
using projectCleanArch.Infra.Data.Repositories;

namespace projectCleanArch.Infra.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName))
            );

            services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            options.AccessDeniedPath = "/Account/Login");

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddAutoMapper(typeof(DomainToDTOMappingProfile));

            services.AddScoped<IAuthenticate, AuthenticateService>();
            services.AddScoped<ISeedUserRoleInitial, SeedUserRoleInitial>();

            var myHandlers = AppDomain.CurrentDomain.Load("projectCleanArch.Application");
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(myHandlers));



            return services;
        }
    }
}
