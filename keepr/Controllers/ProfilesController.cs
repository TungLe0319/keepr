namespace keepr.Controllers;

[ApiController]
[Route("api/[controller]")]

public class ProfilesController : ControllerBase
{
  private readonly AccountService _accountService;
  private readonly Auth0Provider _auth0Provider;
private readonly ProfilesService _profileService;
  private readonly KeepsService _keepService;
  private readonly VaultKeepsService _vaultService;

  public ProfilesController(AccountService accountService, Auth0Provider auth0Provider, ProfilesService profileService, KeepsService keepService, VaultKeepsService vaultService)
  {
    _accountService = accountService;
    _auth0Provider = auth0Provider;
    _profileService = profileService;
    _keepService = keepService;
    _vaultService = vaultService;
  }

  [HttpGet("{profileId}/keeps")]
  public async Task<ActionResult<List<Keep>>> GetKeepsByForProfiles()
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);

     List< Keep> profileKeeps = _keepService.GetKeepsForProfiles(userInfo?.Id);
      return Ok(profileKeeps);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

    [HttpGet("{profileId}")]
    public  ActionResult<Profile> GetById(string profileId)
    {

      try
      {
    ;
    
        Profile profile = _profileService.GetById(profileId);
        return Ok(profile);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  

  
    [HttpGet]
    public async  Task<ActionResult<List<Profile>>> GetAllProfile()
    {
      try
      {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);

        List<Profile> profile = _profileService.GetAllProfile(userInfo);
        return Ok(profile);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  
}
