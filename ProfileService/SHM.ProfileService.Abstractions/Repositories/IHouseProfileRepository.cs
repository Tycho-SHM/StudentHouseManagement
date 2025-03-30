using SHM.ProfileService.Model;

namespace SHM.ProfileService.Abstractions.Repositories;

public interface IHouseProfileRepository
{
    public Task<List<HouseProfile>> GetAll();
    public Task<HouseProfile> Add(HouseProfile houseProfile);
}