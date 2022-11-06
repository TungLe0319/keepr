namespace keepr.Services;

public class VaultsService
{

  #region readOnly
  private readonly VaultsRepository _vaultRepo;
  private readonly KeepsRepository _keepRepo;
  private readonly VaultKeepRepository _vaultKeepRepo;
  public VaultsService(VaultsRepository vaultRepo, KeepsRepository keepRepo, VaultKeepRepository vaultKeepRepo)
  {
    _vaultRepo = vaultRepo;
    _keepRepo = keepRepo;
    _vaultKeepRepo = vaultKeepRepo;
  }
#endregion
 
  internal List<Vault> GetAllVaults()
  {
    return _vaultRepo.GetAllVaults();
  }

  internal Vault GetById(int vaultId)
  {

    Vault vault = _vaultRepo.GetById(vaultId);
   
    if (vault == null)
    {
      throw new Exception("Invalid vaultId");
    }

// if( vault.CreatorId != userId  & vault.IsPrivate == true)
// {
// throw new Exception("Private Vault");
// }
if( vault.IsPrivate == true)
{
throw new Exception("Invalid [example]");
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
    if (vault.CreatorId != accountId)
    {
      throw new Exception("Unauthorized not yours..");
    }
    _vaultRepo.DeleteVault(vaultId);
  }

  internal Vault EditVault(Vault vaultData, string accountId)
  {
    Vault original = GetById(vaultData.Id);

    if (original.CreatorId != accountId)
    {
      throw new Exception("Unauthorized not yours...");
    }

    original.Name = vaultData.Name ?? original.Name;
    original.IsPrivate = vaultData.IsPrivate ?? original.IsPrivate;
    original.Description = vaultData.Description ?? original.Description;
    original.Img = vaultData.Img ?? original.Img;

    Vault updated = _vaultRepo.EditVault(original);

    return updated;

  }

}