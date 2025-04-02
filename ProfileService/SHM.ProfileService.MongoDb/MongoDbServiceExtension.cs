using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SHM.ProfileService.Abstractions.Repositories;

namespace SHM.ProfileService.MongoDb;

public static class MongoDbServiceExtension
{
    public static IServiceCollection RegisterSHMMongoDb(this IServiceCollection services,
        Action<MongoDbOptions> mongoDbOptions)
    {
        services.Configure(mongoDbOptions);
        services.TryAddSingleton<IHouseProfileRepository, HouseProfileRepository>();
        services.TryAddSingleton<IUserProfileRepository, UserProfileRepository>();

        return services;
    }
}