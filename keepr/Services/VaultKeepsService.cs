namespace keepr.Services;

public class VaultKeepsService
{

  private readonly VaultsRepository _vaultRepo;
  private readonly KeepsRepository _keepRepo;
  private readonly VaultKeepRepository _vaultKeepRepo;

  public VaultKeepsService(VaultsRepository vaultRepo, KeepsRepository keepRepo, VaultKeepRepository vaultKeepRepo)
  {
    _vaultRepo = vaultRepo;
    _keepRepo = keepRepo;
    _vaultKeepRepo = vaultKeepRepo;
  }

  public VaultKeep CreateVaultKeep(VaultKeep vaultKeep)
  {
    //TODO clean up
    VaultKeep newVaultKeep = _vaultKeepRepo.CreateVaultKeep(vaultKeep);


    return newVaultKeep;
  }

  public VaultKeep GetById(int id)
  {
    VaultKeep vaultKeep = _vaultKeepRepo.GetById(id);
    return vaultKeep;
  }

  internal List<VaultedKeep> GetVaultedKeepById(int vaultId)
  {
    Vault vault = _vaultRepo.GetById(vaultId);
    if (vault.IsPrivate == true)
    {
      throw new Exception("Private ");
    }

    return _vaultKeepRepo.GetVaultedKeepById(vaultId);

  }


  internal List<VaultedKeep> GetPublicKeeps(int vaultId)
  {
    Vault vault = _vaultRepo.GetById(vaultId);
    if (vault.IsPrivate == true)
    {
      throw new Exception("Private Vault");
    }
    return _vaultKeepRepo.GetVaultedKeepById(vaultId);
  }

  internal string DeleteVaultKeep(int vaultKeepId, string accountId)
  {

    VaultKeep vaultKeep = GetById(vaultKeepId);
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
      throw new Exception("Unable to remove from fault...");
    }
    return "Successfully removed from vault";
  }

}