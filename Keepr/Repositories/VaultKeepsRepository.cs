namespace Keepr.Repositories
{
    public class VaultKeepsRepository
    {
        private readonly IDbConnection _db;

        public VaultKeepsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal VaultKeep Insert(VaultKeep vaultKeepData)
        {
            string sql = @"
            INSERT INTO vaultKeeps
            (vaultId, keepId, creatorId)
            VALUES
            (@vaultId, @keepId, @creatorId);
            SELECT LAST_INSERT_ID();
            ";
            int id = _db.ExecuteScalar<int>(sql, vaultKeepData);
            vaultKeepData.Id = id;
            return vaultKeepData;
        }

        internal List<KeptKeep> GetVaultKeeps(int vaultId)
        {
            string sql = @"
            SELECT
            k.*,
            vk.*,
            a.*
            FROM vaultKeeps vk
            JOIN keeps k ON k.id = vk.keepId
            JOIN accounts a ON a.id = k.creatorId
            WHERE vk.vaultId = @vaultId;
            ";
            List<KeptKeep> keptKeeps = _db.Query<KeptKeep, VaultKeep, Profile, KeptKeep>(sql, (k, vk, a) =>
            {
                k.Creator = a;
                k.VaultKeepId = vk.Id;
                return k;
            }, new { vaultId }).ToList();
            return keptKeeps;
        }

        public VaultKeep GetOne(int id)
        {
            string sql = @"
            SELECT
            vk.*,
            k.*,
            a.*
            FROM vaultKeeps vk
            JOIN accounts a ON a.id = vk.creatorId
            JOIN keeps k ON k.id = vk.keepId
            WHERE vk.id = @id
            ";
            VaultKeep vaultKeep = _db.Query<VaultKeep, Keep, Account, VaultKeep>(sql, (vk, k, a) =>
            {
                vk.Keep = k;
                vk.Creator = a;
                return vk;
            }, new { id }).FirstOrDefault();
            return vaultKeep;
        }

        internal int Remove(int vaultKeepId)
        {
            string sql = "DELETE FROM vaultKeeps WHERE id = @vaultKeepId LIMIT 1;";
            int rowsAffected = _db.Execute(sql, new { vaultKeepId });
            return rowsAffected;
        }
    }
}