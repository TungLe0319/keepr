namespace keepr.Services;

public class ProfilesService
{

private readonly AccountsRepository _accountRepo;
private readonly ProfilesRepository _profileRepo;
 private readonly VaultKeepRepository _vaultRepo;
 private readonly KeepsRepository _keepRepo;

  public ProfilesService(AccountsRepository accountRepo, ProfilesRepository profileRepo, VaultKeepRepository vaultRepo, KeepsRepository keepRepo)
  {
    _accountRepo = accountRepo;
    _profileRepo = profileRepo;
    _vaultRepo = vaultRepo;
    _keepRepo = keepRepo;
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

  internal List<Vault> GetVaultsForProfiles(string profileId)
  {
   return _vaultRepo.GetVaultsForProfiles(profileId);
  }
}