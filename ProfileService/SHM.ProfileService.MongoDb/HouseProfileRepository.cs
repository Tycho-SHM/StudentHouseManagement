using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using SHM.ProfileService.Abstractions.Repositories;
using SHM.ProfileService.Model;

namespace SHM.ProfileService.MongoDb;

public class HouseProfileRepository : IHouseProfileRepository
{
    private readonly ILogger<HouseProfileRepository> _logger;
    private readonly MongoClient _client;
    private readonly IMongoDatabase _database;
    private readonly IMongoCollection<HouseProfile> _collection;

    public HouseProfileRepository(ILogger<HouseProfileRepository> logger, IOptions<MongoDbOptions> options)
    {
        _logger = logger;
        _client = new MongoClient(options.Value.ConnectionString);
        _database = _client.GetDatabase(options.Value.Database);
        _collection = _database.GetCollection<HouseProfile>(options.Value.HouseProfileCollection);
    }
    
    public async Task<List<HouseProfile>> GetAll()
    {
        return await _collection.AsQueryable().ToListAsync();
    }

    public async Task<HouseProfile> GetById(Guid id)
    {
        return await _collection.Find(x => x.Id.Equals(id)).FirstOrDefaultAsync();
    }

    public async Task<HouseProfile> Add(HouseProfile houseProfile)
    {
        await _collection.InsertOneAsync(houseProfile);
        return _collection.Find(x => x.Id == houseProfile.Id).FirstOrDefault();
    }
}