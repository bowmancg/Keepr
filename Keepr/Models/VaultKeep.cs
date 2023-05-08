namespace Keepr.Models;

    public class VaultKeep : RepoItem<int>
    {
        public Vault Vault { get; set; }
        public int VaultKeepId { get; set; }
        public string CreatorId { get; set; }
        public Profile Creator { get; set; }
        public int VaultId { get; set; }
        public int KeepId { get; set; }
    }
