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

  internal Vault GetById(int vaultId, string userId)
  {

    Vault vault = _vaultRepo.GetById(vaultId);

    var notCreator = vault.CreatorId != userId;
    var privateVault = vault.IsPrivate == true;

    if (notCreator && privateVault)
    {
  // throw new UnauthorizedAccessException();
      throw new Exception("Private Vault");
    }

    return vault;
  }

  internal Vault CreateVault(Vault newVaultData)
  {
    return _vaultRepo.CreateVault(newVaultData);
  }

  internal void DeleteVault(int vaultId, string accountId)
  {
    Vault vault = GetById(vaultId, accountId);
    if (vault.CreatorId != accountId)
    {
      throw new Exception("Unauthorized not yours..");
    }
    _vaultRepo.DeleteVault(vaultId);
  }

  internal Vault EditVault(Vault vaultData, string accountId)
  {
    Vault original = GetById(vaultData.Id, accountId);

    if (original.CreatorId != accountId)
    {
      throw new Exception("Unauthorized not yours...");
    }

    original.Name = vaultData.Name ?? original.Name;
    original.IsPrivate = vaultData.IsPrivate ;
    original.Description = vaultData.Description ?? original.Description;
    original.Img = vaultData.Img ?? original.Img;

    Vault updated = _vaultRepo.EditVault(original);

    return updated;

  }

}