namespace Keepr.Services
{
    public class VaultsService
    {
        private readonly VaultsRepository _repo;
        private readonly VaultKeepsService _vaultKeepsService;

        public VaultsService(VaultsRepository repo, VaultKeepsService vaultKeepsService)
        {
            _repo = repo;
            _vaultKeepsService = vaultKeepsService;
        }

        internal Vault CreateVault(Vault vaultData)
        {
            Vault vault = _repo.Insert(vaultData);
            return vault;
        }

        internal Vault GetOne(int vaultId, string userId)
        {
            Vault vault = _repo.GetOne(vaultId);
            if (vault == null)
            {
                throw new Exception($"Bad Id: {vaultId}");
            }
            if (vault.IsPrivate == true && vault.CreatorId != userId)
            {
                throw new Exception($"{vault.Name} is private.");
            }
            return vault;
        }

        internal Vault EditVault(Vault vaultData, int vaultId, string userId)
        {
            Vault originalVault = _repo.GetOne(vaultId);
            if (originalVault.CreatorId != userId)
            {
                throw new Exception("You cannot edit this.");
            }
            originalVault.Name = vaultData.Name ?? originalVault.Name;
            originalVault.Description = vaultData.Description ?? originalVault.Description;
            originalVault.Img = vaultData.Img ?? originalVault.Img;
            _repo.EditVault(originalVault);
            originalVault.UpdatedAt = DateTime.Now;
            return originalVault;
        }

        internal string Remove(int vaultId, string userId)
        {
            Vault vault = _repo.GetOne(vaultId);
            if (vault.CreatorId != userId)
            {
                throw new Exception("You cannot delete this.");
            }
            int rowsAffected = _repo.Remove(vaultId);
            if (rowsAffected == 0)
            {
                throw new Exception("Delete Failed.");
            }
            if (rowsAffected > 1)
            {
                throw new Exception("Faulty Delete...");
            }
            return $"{vault.Name} has been deleted.";
        }

        internal List<VaultKeep> GetVaultKeeps(int vaultId, string accountId)
        {
            List<VaultKeep> vaultKeeps = _vaultKeepsService.GetVaultKeeps(vaultId, accountId);
            return vaultKeeps;
        }
    }
}