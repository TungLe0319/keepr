namespace keepr.Controllers;


[ApiController]
[Route("api/[controller]")]

public class VaultKeepsController : ControllerBase
{

  private readonly Auth0Provider _auth0;
  private readonly VaultsService _vaultService;
  private readonly KeepsService _keepService;
  private readonly VaultKeepsService _vKeepService;

  public VaultKeepsController(Auth0Provider auth0, VaultsService vaultService, KeepsService keepService, VaultKeepsService vKeepService)
  {
    _auth0 = auth0;
    _vaultService = vaultService;
    _keepService = keepService;
    _vKeepService = vKeepService;
  }

  [HttpPost]
  [Authorize]
  public async Task<ActionResult<VaultKeep>> CreateVaultKeep([FromBody] VaultKeep vaultKeep)
  {
    try
    {
      Account userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);
      if (userInfo == null || userInfo.Id == null)
      {
        throw new Exception("Bad BearerToken");
      }
      vaultKeep.CreatorId = userInfo?.Id;
      VaultKeep newVaultKeep = _vKeepService.CreateVaultKeep(vaultKeep);
      return Ok(newVaultKeep);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }


  [HttpDelete("{vaultKeepId}")]
  [Authorize]
  public async Task<ActionResult<string>> DeleteVaultKeep(int vaultKeepId)
  {
    try
    {
      Account userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);
      if (userInfo == null || userInfo.Id == null)
      {
        throw new Exception("Bad Token... ");
      }

      _vKeepService.DeleteVaultKeep(vaultKeepId, userInfo?.Id);
      return Ok("VaultKeep deleted");
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }


  [HttpGet]
  public ActionResult<List<VaultKeep>> GetAllVaultKeeps()
  {

    try
    {
      List<VaultKeep> vKeeps = _vKeepService.GetAllVaultKeeps();
      return Ok(vKeeps);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }


}