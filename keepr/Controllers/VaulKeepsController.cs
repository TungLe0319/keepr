namespace keepr.Controllers;
[ApiController]
[Route("api/[controller]")]

public class VaultKeepsController : ControllerBase
{
  private readonly Auth0Provider _auth0;
  private readonly VaultsService _vaultService;
  private readonly KeepsService _keepService;
  private readonly VaultKeepsService _vKeep;

  public VaultKeepsController(Auth0Provider auth0, VaultsService vaultService, KeepsService keepService, VaultKeepsService vKeep)
  {
    _auth0 = auth0;
    _vaultService = vaultService;
    _keepService = keepService;
    _vKeep = vKeep;
  }

  [HttpPost]
  [Authorize]
  public async Task<ActionResult<VaultKeep>> CreateVaultKeep([FromBody] VaultKeep vaultKeep)
  {
    try
    {
      var userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);

      if (userInfo == null || userInfo.Id == null)
      {
        throw new Exception("Bad Token");
      }

      vaultKeep.CreatorId = userInfo.Id;
      VaultKeep newVaultKeep = _vKeep.CreateVaultKeep(vaultKeep);


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
      _vKeep.DeleteVaultKeep(vaultKeepId, userInfo.Id);
      return Ok("VaultKeep deleted");
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }



}