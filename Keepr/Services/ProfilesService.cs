namespace Keepr.Services
{
    public class ProfilesService
    {
        private readonly ProfilesRepository _repo;

        public ProfilesService(ProfilesRepository repo)
        {
            _repo = repo;
        }

        internal Profile GetProfile(string profileId)
        {
            Profile profile = _repo.GetProfile(profileId);
            if (profile == null)
            {
                throw new Exception($"{profile.Name} does not exist.");
            }
            return profile;
        }

        internal List<Keep> GetKeeps(string profileId)
        {
            List<Keep> keeps = _repo.GetKeeps(profileId);
            return keeps;
        }

        internal List<Vault> GetVaults(string profileId, string userId)
        {
            List<Vault> vaults = _repo.GetVaults(profileId);
            if (profileId != userId)
            {
                vaults = vaults.FindAll(v => v.IsPrivate == false);
            }
            return vaults;
        }
    }
}