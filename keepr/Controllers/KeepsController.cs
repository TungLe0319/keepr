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


  [HttpPost]
 [Authorize]
  public async Task<ActionResult<Keep>> CreateKeep([FromBody]Keep data)
  {
    try
    {
      var userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);
      data.Creator = userInfo;
      data.CreatorId = userInfo.Id;
      Keep keep = _keepService.CreateKeep(data);
      return Ok(keep);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }



  [HttpGet]
  public ActionResult<List<Keep>> GetAllKeeps()
  {
    try
    {
      List<Keep> keep = _keepService.GetAllKeeps();
      return Ok(keep);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{keepId}")]

  public ActionResult<Keep> GetById(int keepId)
  {
    try
    {
      Keep keep = _keepService.GetById(keepId);
      return Ok(keep);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpPut("{keepId}")]
  [Authorize]
  public async Task<ActionResult<Keep>> EditKeep([FromBody] Keep keepData, int keepId)
  {
    try
    {
      Account userInfo = await _auth0.GetUserInfoAsync<Account>(HttpContext);
      keepData.Id = keepId;
      Keep keep = _keepService.EditKeep(keepData,userInfo?.Id);
      return Ok(keep);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }



}