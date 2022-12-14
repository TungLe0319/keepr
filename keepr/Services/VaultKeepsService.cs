namespace keepr.Services;

public class VaultKeepsService
{

  private readonly VaultsRepository _vaultRepo;
  private readonly KeepsRepository _keepRepo;
  private readonly VaultsService _vaultService;
  private readonly VaultKeepRepository _vaultKeepRepo;

  public VaultKeepsService(VaultsRepository vaultRepo, KeepsRepository keepRepo, VaultsService vaultService, VaultKeepRepository vaultKeepRepo)
  {
    _vaultRepo = vaultRepo;
    _keepRepo = keepRepo;
    _vaultService = vaultService;
    _vaultKeepRepo = vaultKeepRepo;
  }

  public VaultKeep CreateVaultKeep(VaultKeep vaultKeep)
  {
    //TODO clean up
    Vault vault = _vaultRepo.GetById(vaultKeep.VaultId);
    Keep keep = _keepRepo.GetById(vaultKeep.KeepId);
    keep.Kept ++;
    if (vault == null)
    {
      throw new Exception("Invalid [example]");
    }

    if (vault.CreatorId != vaultKeep.CreatorId)
    {
      throw new Exception("Unauthorized cannot add keep to this vault");
    }

    // VaultKeep newVaultKeep = _vaultKeepRepo.CreateVaultKeep(vaultKeep);

   VaultKeep newVaultKeep = _vaultKeepRepo.CreateVaultKeep(vaultKeep);

    // if (newVaultKeep == null)
    // {
    //   throw new Exception("Invalid [example]");
    // }

    return newVaultKeep;
  }

  public VaultKeep GetById(int id)
  {
    VaultKeep vaultKeep = _vaultKeepRepo.GetById(id);
    return vaultKeep;
  }

  internal List<VaultedKeep> GetVaultedKeepById(int vaultId, string userId)
  {
    Vault vault = _vaultService.GetById(vaultId, userId);
    

    return _vaultKeepRepo.GetKeepsByVaultId(vaultId);
  }


  // internal List<VaultedKeep> GetKeepsByVaultId(int vaultId,string userId)
  // {

  //   Vault vault = _vaultService.GetById(vaultId,userId);

  //   return _vaultKeepRepo.GetKeepsByVaultId(vaultId);
  // }

  internal string DeleteVaultKeep(int vaultKeepId, string accountId)
  {

    VaultKeep vaultKeep = GetById(vaultKeepId);
    Keep keep = _keepRepo.GetById(vaultKeep.KeepId);
    if (vaultKeep == null)
    {
      throw new Exception("Invalid vaultKeep Id");
    }
    if (vaultKeep.CreatorId != accountId)
    {
      throw new Exception("Unauthorized not yours...");
    }


    var deleted = _vaultKeepRepo.DeleteVaultKeep(vaultKeepId);
    if (!deleted)
    {
      throw new Exception("Unable to remove from vault...");
    }
    return "Successfully removed from vault";
  }

  internal List<VaultKeep> GetAllVaultKeeps()
  {
    return _vaultKeepRepo.GetAllVaultKeeps();
  }
}