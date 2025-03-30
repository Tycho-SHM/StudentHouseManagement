namespace SHM.ProfileService.MongoDb;

public class MongoDbOptions
{
    public string ConnectionString { get; set; }
    public string Database { get; set; }
    public string HouseProfileCollection { get; set; }
    public string UserProfileCollection { get; set; }
}