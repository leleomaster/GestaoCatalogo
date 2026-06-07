using GestaoCatalogo.Application.Interfaces;
using GestaoCatalogo.Application.Services;
using GestaoCatalogo.Domain.Interfaces;
using GestaoCatalogo.Infrastructure.Persistence;
using GestaoCatalogo.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GestaoCatalogo.CrossCutting.Ioc;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string? connectionString)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<ICategoriaRepository, CategoriaRepository>();
        services.AddScoped<IProdutoRepository, ProdutoRepository>();

        return services;
    }

    public static IServiceCollection AddApplication(this IServiceCollection services)
    {

        services.AddScoped<ICategoriaService, CategoriaService>();
        services.AddScoped<IProdutoService, ProdutoService>();

        return services;
    }
}
