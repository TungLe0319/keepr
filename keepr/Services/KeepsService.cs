namespace keepr.Services;
public class KeepsService {
  private readonly KeepsRepository _keepRepo;
  private readonly VaultsRepository _vaultRepo;

  public KeepsService(KeepsRepository keepRepo, VaultsRepository vaultRepo)
  {
    _keepRepo = keepRepo;
    _vaultRepo = vaultRepo;
  }

  internal string DeleteKeep(int keepId, string userId)
  {
 Keep keep = GetById(keepId);
 if( keep.CreatorId == userId)
 {
 throw new Exception("Unauthorized not yours...");
 }
var deleted = _keepRepo.DeleteKeep(keepId);
if( !deleted )
{
throw new Exception("Unable to delete this keep");
}

return " Successfully deleted your keep";
 
  }


  public Keep GetById(int id){
    Keep keep = _keepRepo.GetById(id);
    if( keep == null)
    {
    throw new Exception("Invalid Keep Id");
    }
    return keep;
  }
}