namespace keepr.Services;

public class VaultsService{
  private readonly VaultsRepository _vaultRepo;
  private readonly KeepsRepository _keepRepo;

  public VaultsService(VaultsRepository vaultRepo, KeepsRepository keepRepo)
  {
    _vaultRepo = vaultRepo;
    _keepRepo = keepRepo;
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
if( vault.CreatorId == accountId)
{
throw new Exception("Unauthorized, not yours..");
}
_vaultRepo.DeleteVault(vaultId);

  }

  internal Vault EditVault(Vault vaultData, string accountId)
  {
Vault original = GetById(vaultData.Id);
if( original.CreatorId == accountId)
{
throw new Exception("Unauthorized not yours...");
}


original.Name = vaultData.Name;
original.IsPrivate = vaultData.IsPrivate;
original.Description = vaultData.Description;
original.CoverImg = vaultData.CoverImg == "//thiscatdoesnotexist.com" ? original.CoverImg : vaultData.CoverImg;
original.KeepCount = vaultData.KeepCount;

Vault updated = _vaultRepo.EditVault(original);
return updated;

  }
}