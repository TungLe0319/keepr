namespace keepr.Controllers;

[ApiController]
[Route("api/[controller]")]

public class VaultsController : ControllerBase
{
  private readonly Auth0Provider _auth0;
  private readonly VaultsService _vaultService;
  private readonly KeepsService _keepService;
  private readonly VaultKeepsService _vaultKeepService;

  public VaultsController(Auth0Provider auth0, VaultsService vaultService, KeepsService keepService, VaultKeepsService vaultKeepService)
  {
    _auth0 = auth0;
    _vaultService = vaultService;
    _keepService = keepService;
    _vaultKeepService = vaultKeepService;
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

  public async Task<ActionResult<Vault>> GetById(int vaultId)
  {
    try
    {
      var userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);
    

      Vault vault = _vaultService.GetById(vaultId);
      

      if (vault == null)
      {
        throw new Exception("bad Id");
      }
      

      return Ok(vault);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }



  [HttpGet("{vaultId}/keeps")]
  public async Task<ActionResult<List<VaultedKeep>>> GetKeepsByVaultId(int vaultId)
  {
    try
    {
      var userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);
   
    if( userInfo == null) //not logged in or bad token
    {
        List<VaultedKeep> publicKeeps = _vaultKeepService.GetPublicKeeps(vaultId);
        return Ok(publicKeeps);
    }
    

      List<VaultedKeep> vaultKeeps = _vaultKeepService.GetKeepsByVaultId(vaultId);

      return Ok(vaultKeeps);
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
      if (userInfo == null || userInfo.Id == null)
      {
        throw new Exception("You are a bad user..... or your token is crappy... 🤷");
      }
      newVaultData.Creator = userInfo;
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
      // if (userInfo == null || userInfo.Id == null)
      // {
      //   throw new Exception("Bad Token... ");
      // }
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
      if (userInfo == null || userInfo.Id == null)
      {
        throw new Exception("Bad Token... ");
      }
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