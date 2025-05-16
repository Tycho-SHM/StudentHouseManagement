using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SHM.ProfileService.Abstractions.Repositories;
using SHM.ProfileService.EfCore.Repository;

namespace SHM.ProfileService.EfCore;

public static class EfCoreServiceExtension
{
    public static IServiceCollection RegisterSHMProfileServiceEfCore(this IServiceCollection services,
        string connectionString, string databaseName)
    {
        services.AddDbContextPool<ProfileContext>(options =>
        {
            // options.UseMongoDB(connectionString, databaseName);
            options.UseNpgsql(connectionString);
        });

        services.TryAddTransient<IUserProfileRepository, UserProfileRepository>();
        services.TryAddTransient<IHouseProfileRepository, HouseProfileRepository>();

        return services;
    }
}