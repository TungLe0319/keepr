namespace keepr.Services;

public class VaultKeepsService{

  private readonly VaultsRepository _vaultRepo;
  private readonly KeepsRepository _keepRepo;
private readonly VaultKeepRepository _vaultKeepRepo;
 
  internal VaultKeep CreateVaultKeep(VaultKeep vaultKeep)
  {
return _vaultKeepRepo.CreateVaultKeep(vaultKeep);
  }


}