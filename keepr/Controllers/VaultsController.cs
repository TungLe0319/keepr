namespace keepr.Controllers;

[ApiController]
[Route("api/[controller]")]

public class VaultsController : ControllerBase {
  private readonly Auth0Provider _auth0;
  private readonly VaultsService _vaultService;
  private readonly KeepsService _keepService;

  public VaultsController(Auth0Provider auth0, VaultsService vaultService, KeepsService keepService)
  {
    _auth0 = auth0;
    _vaultService = vaultService;
    _keepService = keepService;
  }




  
    [HttpGet]
    public ActionResult<List<Vault>> GetAllVault()
    {
      try
      {
        List<Vault> vaults = _vaultService.GetAllVaults();
        return Ok(vaults);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  

    [HttpGet("{vaultId}")]
  
    public ActionResult<Vault> GetById(int vaultId)
    {
      try
      {
        Vault vault = _vaultService.GetById(vaultId);
        return Ok(vault);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  


    [HttpPost]
   [Authorize]
    public async Task<ActionResult<Vault>> CreateVault([FromBody] Vault newVaultData)
    {
      try
      {
        var userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);
        if( userInfo == null || userInfo.Id == null)
        {
        throw new Exception("You are a bad user..... or your token is crappy... 🤷");
        }
        newVaultData.CreatorId = userInfo?.Id;
        Vault newVault = _vaultService.CreateVault(newVaultData);
        return Ok(newVault);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

  [HttpDelete("{vaultId}")]
  [Authorize]
  public async Task<ActionResult<string>> DeleteVault(int vaultId)
  {
    try
    {
      Account userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);
      _vaultService.DeleteVault(vaultId, userInfo?.Id);
      return Ok("Vault deleted");
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpPut("{vaultId}")]
  [Authorize]
  public async Task<ActionResult<Vault>> EditVault([FromBody] Vault vaultData, int vaultId)
  {
    try
    {
      Account userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);
      vaultData.Id = vaultId;
      Vault vault = _vaultService.EditVault(vaultData, userInfo?.Id);
      return Ok(vault);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }


  
}