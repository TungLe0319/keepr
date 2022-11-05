namespace keepr.Services;

public class AccountService
{
  private readonly AccountsRepository _repo;
  private readonly VaultsRepository _vRepo;

  public AccountService(AccountsRepository repo, VaultsRepository vRepo)
  {
    _repo = repo;
    _vRepo = vRepo;
  }

  internal Account GetProfileByEmail(string email)
  {
    return _repo.GetByEmail(email);
  }

  internal Account GetOrCreateProfile(Account userInfo)
  {
    Account profile = _repo.GetById(userInfo.Id);
    if (profile == null)
    {
      return _repo.Create(userInfo);
    }
    return profile;
  }

  internal Account Edit(Account editData, string userEmail)
  {
    Account original = GetProfileByEmail(userEmail);
    original.Name = editData.Name.Length > 0 ? editData.Name : original.Name;
    original.Picture = editData.Picture.Length > 0 ? editData.Picture : original.Picture;
    return _repo.Edit(original);
  }

  internal List<Vault> GetMyVaults(string accountId)
  {

  return _repo.GetMyVaults(accountId);
 
  }


}
