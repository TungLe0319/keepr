namespace keepr.Controllers;

[ApiController]
[Route("api/[controller]")]
public class KeepsController : ControllerBase
{
  private readonly Auth0Provider _auth0;
  private readonly VaultsService _vaultService;
  private readonly KeepsService _keepService;
  public KeepsController(Auth0Provider auth0, VaultsService vaultService, KeepsService keepService)
  {
    _auth0 = auth0;
    _vaultService = vaultService;
    _keepService = keepService;
  }


  [HttpDelete("{keepId}")]
  [Authorize]
  public async Task<ActionResult<string>> DeleteKeep(int keepId)
  {
    try
    {
      Account userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);
      _keepService.DeleteKeep(keepId, userInfo?.Id);
      return Ok("Keep deleted");
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }




}