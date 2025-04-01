using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using SHM.ProfileService.Abstractions.Repositories;
using SHM.ProfileService.Model;

namespace SHM.ProfileService.MongoDb;

public class UserProfileRepository : IUserProfileRepository
{
    private readonly ILogger<UserProfileRepository> _logger;
    private readonly MongoClient _client;
    private readonly IMongoDatabase _database;
    private readonly IMongoCollection<UserProfile> _collection;

    public UserProfileRepository(ILogger<UserProfileRepository> logger, IOptions<MongoDbOptions> options)
    {
        _logger = logger;
        _client = new MongoClient(options.Value.ConnectionString);
        _database = _client.GetDatabase(options.Value.Database);
        _collection = _database.GetCollection<UserProfile>(options.Value.UserProfileCollection);
    }
    
    public async Task<List<UserProfile>> GetAll()
    {
        return await _collection.AsQueryable().ToListAsync();
    }

    public async Task<UserProfile?> GetById(Guid id)
    {
        if (!_collection.Find(FilterDefinition<UserProfile>.Empty).Limit(1).Any())
        {
            return null;
        }
        
        return await _collection.Find(x => x.Id.Equals(id)).FirstOrDefaultAsync();
    }

    public async Task<UserProfile?> GetByUserId(string userId)
    {
        if (!_collection.Find(FilterDefinition<UserProfile>.Empty).Limit(1).Any())
        {
            return null;
        }
        
        return await _collection.Find(x => x.UserId.Equals(userId)).FirstOrDefaultAsync();
    }

    public async Task<UserProfile> Add(UserProfile userProfile)
    {
        await _collection.InsertOneAsync(userProfile);
        return _collection.Find(x => x.Id == userProfile.Id).FirstOrDefault();
    }

    public async Task<UserProfile> Update(UserProfile userProfile)
    {
        await _collection.ReplaceOneAsync(x => x.Id.Equals(userProfile.Id), userProfile);
        return _collection.Find(x => x.Id == userProfile.Id).FirstOrDefault();
    }
}