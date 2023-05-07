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

        internal Vault GetOne(int vaultId)
        {
            Vault vault = _repo.GetOne(vaultId);
            if (vault == null)
            {
                throw new Exception($"Bad Id: {vaultId}");
            }
            return vault;
        }

        internal Vault EditVault(Vault vaultData, int vaultId)
        {
            Vault originalVault = this.GetOne(vaultId);
            originalVault.Name = vaultData.Name ?? originalVault.Name;
            originalVault.Description = vaultData.Description ?? originalVault.Description;
            originalVault.Img = vaultData.Img ?? originalVault.Img;
            _repo.EditVault(originalVault);
            originalVault.UpdatedAt = DateTime.Now;
            return originalVault;
        }

        internal string Remove(int vaultId)
        {
            Vault vault = this.GetOne(vaultId);
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

        internal List<VaultKeep> GetVaultKeeps(int vaultId)
        {
            List<VaultKeep> vaultKeeps = _vaultKeepsService.GetVaultKeeps(vaultId);
            return vaultKeeps;
        }
    }
}