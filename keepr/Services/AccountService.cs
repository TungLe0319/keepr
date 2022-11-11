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

internal Account GetById(string userId){
  return _repo.GetById(userId);
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

  internal Account Edit(Account editData, Account userInfo)
  {
    // Account original = GetProfileByEmail(userEmail);
    Account original = GetById(userInfo.Id);
    original.Name = editData.Name.Length > 0 ? editData.Name : original.Name;
    original.Picture = editData.Picture.Length > 0 ? editData.Picture : original.Picture;
    original.CoverImg = editData.CoverImg ?? original.CoverImg;
    original.Theme = editData.Theme ?? original.Theme;
    original.Twitter = editData.Twitter ?? original.Twitter;
    original.Instagram = editData.Instagram ?? original.Instagram;
    original.FaceBook = editData.FaceBook ?? original.FaceBook;
    original.Pinterest = editData.Pinterest ?? original.Pinterest;
    return _repo.Edit(original);
  }

  internal List<Vault> GetMyVaults(string accountId)
  {

  return _repo.GetMyVaults(accountId);
 
  }

  internal List<Keep> GetMyKeeps(string accountId)
  {
  return _repo.GetMyKeeps(accountId);
  }
}
