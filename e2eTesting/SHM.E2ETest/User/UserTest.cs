using System.Text.Json;
using Clerk.BackendAPI;
using Clerk.BackendAPI.Models.Operations;
using Microsoft.Extensions.Configuration;
using Microsoft.Playwright;
using MongoDB.Bson;
using SHM.ProfileService.Model;

namespace SHM.E2ETest.User;

[TestClass]
public class UserTest : PlaywrightTest
{
    private static IConfiguration _configuration;
    private Clerk.BackendAPI.Models.Components.User? _user;
    private string _token;

    private IAPIRequestContext _profileServiceApi = null!;
    private string _testProfileRandomValue = Guid.NewGuid().ToString();

    private static ClerkBackendApi _clerkSdk;
    private bool userDeleted = false;

    [ClassInitialize]
    public static void ClassInitialize(TestContext testContext)
    {
        _configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.Development.json")
            .AddEnvironmentVariables()
            .Build();

        _clerkSdk = new ClerkBackendApi(_configuration["ClerkApiSecret"]);
    }

    [TestInitialize]
    public async Task Initialize()
    {
        var userCreationRequest = new CreateUserRequestBody()
        {
            FirstName = $"E2E-UserTest-{_testProfileRandomValue}",
            EmailAddress = new List<string>()
            {
                $"E2E-UserTest-{_testProfileRandomValue}+clerk_test@example.com"
            },
            Password = "yOXyxq9AvTEpddq" //This is a password just used for testing so it is fine for it to be in the test itself. (the user is deleted right after too)
        };

        var userCreationResult = await _clerkSdk.Users.CreateAsync(userCreationRequest);
        _user = userCreationResult.User!;


        var sessionRequest = new CreateSessionRequestBody()
        {
            UserId = _user.Id!
        };
        
        var sessionResult = await _clerkSdk.Sessions.CreateAsync(sessionRequest);

        var token = await _clerkSdk.Sessions.CreateTokenAsync(
            sessionResult.Session!.Id,
            new CreateSessionTokenRequestBody()
        );
        
        _token = token.Object!.Jwt!;

        await CreateApiRequestContext();

        var emptyProfileResult = await _profileServiceApi.GetAsync("/profiles/UserProfile/GetOwnProfile");
        await Expect(emptyProfileResult).ToBeOKAsync();

        var emptyProfileJson = await emptyProfileResult.JsonAsync();
        
        var data = new
        {
            id = emptyProfileJson?.GetProperty("id").GetString(),
            displayName = $"E2E-UserTest-{_testProfileRandomValue}",
            userId = emptyProfileJson?.GetProperty("userId").GetString()
        };

        var completeProfileResult = await _profileServiceApi.PutAsync("/profiles/UserProfile",
            new APIRequestContextOptions
            {
                DataObject = data
            });
        await Expect(completeProfileResult).ToBeOKAsync();
    }
    
    private async Task CreateApiRequestContext()
    {
        var headers = new Dictionary<string, string>();
        headers.Add("Authorization", $"Bearer {_token}");
        headers.Add("Content-Type", "application/json");
 
        _profileServiceApi = await Playwright.APIRequest.NewContextAsync(new APIRequestNewContextOptions {
            // All requests we send go to this API endpoint.
            BaseURL = _configuration["ApiGatewayUrl"],
            ExtraHTTPHeaders = headers,
            IgnoreHTTPSErrors = true
        });
    }
    
    [TestMethod]
    public async Task DeleteUser()
    {
        await _clerkSdk.Users.DeleteAsync(_user.Id);

        userDeleted = true;
        
        //TODO maybe make it so this checks on clerk's end if the webhook is not pending anymore before continuing instead of just waiting a bit?
        Thread.Sleep(5000);
        
        var profileResult = await _profileServiceApi.GetAsync("/profiles/UserProfile/GetOwnProfile");
        await Expect(profileResult).ToBeOKAsync();

        var profileJson = await profileResult.JsonAsync();
        Assert.AreEqual("Deleted Profile", profileJson?.GetProperty("displayName").GetString());
    }
    
    [TestCleanup]
    public async Task Cleanup()
    {
        await _profileServiceApi.DisposeAsync();

        if (!userDeleted && _user != null)
        {
            await _clerkSdk.Users.DeleteAsync(_user.Id);
        }
    }
}