namespace keepr.Services;
public class KeepsService
{
  private readonly KeepsRepository _keepRepo;
  private readonly VaultsRepository _vaultRepo;

  public KeepsService(KeepsRepository keepRepo, VaultsRepository vaultRepo)
  {
    _keepRepo = keepRepo;
    _vaultRepo = vaultRepo;
  }

  internal string DeleteKeep(int keepId, string userId)
  {
    Keep keep = GetById(keepId, userId);
    if (keep.CreatorId != userId)
    {
      throw new Exception("Unauthorized not yours...");
    }
    var deleted = _keepRepo.DeleteKeep(keepId);
    if (!deleted)
    {
      throw new Exception("Unable to delete this keep");
    }

    return " Successfully deleted your keep";

  }


  public Keep GetById(int id, string userId)
  {

    Keep keep = _keepRepo.GetById(id);
    if (keep == null)
    {
      throw new Exception("Invalid Keep Id");
    }
    if (keep.CreatorId != userId)
    {
      UpdateKept(keep);
    }

    return keep;
  }

  internal void UpdateKept(Keep keep)
  {
    keep.Views++;
    _keepRepo.EditKeep(keep);
  }

  internal Keep CreateKeep(Keep data)
  {
    return _keepRepo.CreateKeep(data);
  }

  internal List<Keep> GetKeepsForProfiles(string profileId)
  {
    return _keepRepo.GetKeepsForProfiles(profileId);
  }

  internal List<Keep> GetAllKeeps( int offSet)
  {

    return _keepRepo.GetAllKeeps(offSet);
  }

  internal Keep EditKeep(Keep keepData, string userId)
  {
    Keep original = GetById(keepData.Id, userId);
    if (original.CreatorId != userId)
    {
      throw new Exception("Unauthorized not yours...");
    }

    original.Name = keepData.Name ?? original.Name;
    original.Description = keepData.Description ?? original.Description;
    original.Img = keepData.Img ?? original.Img;

    Keep updated = _keepRepo.EditKeep(original);
    return updated;

  }


}