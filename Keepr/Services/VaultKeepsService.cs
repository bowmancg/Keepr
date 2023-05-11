namespace Keepr.Services
{
    public class VaultKeepsService
    {
        private readonly VaultKeepsRepository _repo;
        private readonly VaultsRepository _vaultRepo;
        private readonly KeepsRepository _keepRepo;
        private readonly VaultsService _vaultsService;

        public VaultKeepsService(VaultKeepsRepository repo, VaultsRepository vaultRepo, KeepsRepository keepRepo, VaultsService vaultsService)
        {
            _repo = repo;
            _vaultRepo = vaultRepo;
            _keepRepo = keepRepo;
            _vaultsService = vaultsService;
        }

        internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData)
        {
            if (vaultKeepData.CreatorId == null)
            {
                throw new Exception("Unauthorized.");
            }

            Keep keep = _keepRepo.GetOne(vaultKeepData.KeepId);
            if (keep == null)
            {
                throw new Exception("Unauthorized.");
            }

            Vault vault = _vaultRepo.GetOne(vaultKeepData.VaultId);
            if (vault == null || vault.CreatorId != vaultKeepData.CreatorId)
            {
                throw new Exception("Unauthorized.");
            }
            VaultKeep vaultKeep = _repo.Insert(vaultKeepData);
            keep.Kept++;
            _keepRepo.Update(keep);
            // if (vaultKeepData.KeepId == 0)
            // {
            //     throw new Exception("You cannot create this.");
            // }
            // if (vaultKeepData.VaultId == 0)
            // {
            //     throw new Exception("You cannot create this.");
            // }
            return vaultKeep;
        }

        internal List<KeptKeep> GetVaultKeeps(int vaultId, string userId)
        {
            Vault vault = _vaultsService.GetOne(vaultId, userId);
            
            // if (vault.IsPrivate == true && vault.CreatorId != accountId)
            // {
            //     throw new Exception($"{vault.Name} is private.");
            // }
            List<KeptKeep> vaultKeeps = _repo.GetVaultKeeps(vaultId);
            return vaultKeeps;
        }

        internal string Remove(int vaultKeepId, string userId)
        {
            VaultKeep vaultKeep = _repo.GetOne(vaultKeepId);
            if (vaultKeep.CreatorId != userId)
            {
                throw new Exception("You cannot delete this.");
            }
            int rowsAffected = _repo.Remove(vaultKeepId);
            if (rowsAffected == 0)
            {
                throw new Exception("Delete Failed.");
            }
            if (rowsAffected > 1)
            {
                throw new Exception("Faulty Delete...");
            }
            return $"{vaultKeepId} has been deleted.";
        }
    }
}