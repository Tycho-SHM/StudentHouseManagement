using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SHM.ProfileService.Abstractions.Repositories;
using SHM.ProfileService.Model.House;

namespace SHM.ProfileService.EfCore.Repository;

public class HouseProfileRepository : IHouseProfileRepository
{
    private readonly ProfileContext _dbContext;
    private readonly ILogger<HouseProfileRepository> _logger;

    public HouseProfileRepository(ILogger<HouseProfileRepository> logger, ProfileContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task<List<HouseProfile>> GetAll()
    {
        return await _dbContext.HouseProfiles.ToListAsync();
    }

    public async Task<HouseProfile> GetById(Guid id)
    {
        return await _dbContext.HouseProfiles.FindAsync(id);
    }

    public async Task<HouseProfile> Add(HouseProfile houseProfile)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ExistsByName(string houseName)
    {
        throw new NotImplementedException();
    }
}