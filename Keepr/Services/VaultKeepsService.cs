namespace Keepr.Services
{
    public class VaultKeepsService
    {
        private readonly VaultKeepsRepository _repo;

        public VaultKeepsService(VaultKeepsRepository repo)
        {
            _repo = repo;
        }

        internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData)
        {
            VaultKeep vaultKeep = _repo.Insert(vaultKeepData);
            return vaultKeep;
        }

        internal List<VaultKeep> GetVaultKeeps(int vaultId)
        {
            List<VaultKeep> vaultKeeps = _repo.GetVaultKeeps(vaultId);
            return vaultKeeps;
        }

        internal string Remove(int vaultKeepId)
        {
            int vaultKeep = _repo.Remove(vaultKeepId);
            if (vaultKeep == 0)
            {
                throw new Exception("Delete Failed.");
            }
            if (vaultKeep > 1)
            {
                throw new Exception("Faulty Delete...");
            }
            return $"{vaultKeepId} has been deleted.";
        }
    }
}