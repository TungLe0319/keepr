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
 if( keep.CreatorId != userId)
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

  internal Keep CreateKeep(Keep data)
  {
  return _keepRepo.CreateKeep(data);
  }

  internal List<Keep> GetKeepsForProfiles(string profileId)
  {
   return _keepRepo.GetKeepsForProfiles(profileId);
  }

  internal List<Keep> GetAllKeeps()
  {
 
  return _keepRepo.GetAllKeeps();
  }

  internal Keep EditKeep(Keep keepData, string userId)
  {
Keep original = GetById(keepData.Id);
if( original.CreatorId != userId)
{
throw new Exception("Unauthorized not yours...");
}

original.Name = keepData.Name;
original.Description = keepData.Description;

Keep updated = _keepRepo.EditKeep(original);
return updated;

  }

  internal List<Keep> GetKeepsByVaultId(int vaultId)
  {
return _keepRepo.GetKeepsByVaultId(vaultId);
  }
}