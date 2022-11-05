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
    return _vaultKeepRepo.CreateVaultKeep(vaultKeep);
  }

  public VaultKeep GetById(int id){
    VaultKeep vaultKeep = _vaultKeepRepo.GetById(id);
    return vaultKeep;
  }

  internal List<KeptKeep> GetKeepsByVaultId(int vaultId)
  {
    return _vaultKeepRepo.GetKeepsByVaultId(vaultId);
  }
}