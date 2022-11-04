namespace keepr.Services;

public class VaultsService{
  private readonly VaultsRepository _vaultRepo;
  private readonly KeepsRepository _keepRepo;
  private readonly VaultKeepRepository _vaultKeepRepo;

  public VaultsService(VaultsRepository vaultRepo, KeepsRepository keepRepo, VaultKeepRepository vaultKeepRepo)
  {
    _vaultRepo = vaultRepo;
    _keepRepo = keepRepo;
    _vaultKeepRepo = vaultKeepRepo;
  }

  internal List<Vault> GetAllVaults()
  {
return _vaultRepo.GetAllVaults();

  }

  internal Vault GetById(int vaultId)
  {
  Vault vault = _vaultRepo.GetById(vaultId);
  if( vault == null)
  {
  throw new Exception("Invalid vaultId");
  }


  
return vault;
  }

  internal Vault CreateVault(Vault newVaultData)
  {
   return _vaultRepo.CreateVault(newVaultData);
  }

  internal void DeleteVault(int vaultId, string accountId)
  {
Vault vault = GetById(vaultId);
if( vault.CreatorId != accountId)
{
throw new Exception("Unauthorized, not yours..");
}
_vaultRepo.DeleteVault(vaultId);

  }

  internal Vault EditVault(Vault vaultData, string accountId)
  {
Vault original = GetById(vaultData.Id);
if( original.CreatorId != accountId)
{
throw new Exception("Unauthorized not yours...");
}


original.Name = vaultData.Name;
original.IsPrivate = vaultData.IsPrivate;
original.Img = vaultData.Img;
Vault updated = _vaultRepo.EditVault(original);
return updated;

  }

  internal List<Keep> GetKeepsByVaultId(int vaultId)
  {
 List<Keep>  keeps= _keepRepo.GetKeepsByVaultId(vaultId);

 if( keeps == null)
 {
 throw new Exception("Invalid VaultId");
 }
 return keeps;
  }

}