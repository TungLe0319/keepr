namespace keepr.Services;

public class ProfilesService
{

private readonly AccountsRepository _accountRepo;
private readonly ProfilesRepository _profileRepo;

  public ProfilesService(AccountsRepository accountRepo, ProfilesRepository profileRepo)
  {
    _accountRepo = accountRepo;
    _profileRepo = profileRepo;
  }

  public Profile GetById(string profileId)
  {
Profile profile = _profileRepo.GetById(profileId);

if( profile == null)
{
throw new Exception("Invalid profileId");
}
return profile;
  }

  internal List<Profile> GetAllProfile(Account userInfo)
  {
   return _profileRepo.GetAllProfiles();
  }
}