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

        internal List<VaultKeep> GetVaultKeeps(int vaultId)
        {
            string sql = @"
            SELECT
            vk.*,
            vk.id vaultKeepId,
            k.*,
            a.*
            FROM vaultKeeps vk
            JOIN keeps k ON k.id = vk.keepId
            JOIN accounts a ON a.id = vk.creatorId
            WHERE vk.vaultId = @vaultId;
            ";
            List<VaultKeep> vaultKeeps = _db.Query<VaultKeep, Profile, VaultKeep>(sql, (vk, a) =>
            {
                vk.Creator = a;
                return vk;
            }, new { vaultId }).ToList();
            return vaultKeeps;
        }

        internal int Remove(int vaultKeepId)
        {
            string sql = "DELETE FROM vaultKeeps WHERE id = @vaultKeepId LIMIT 1;";
            int rowsAffected = _db.Execute(sql, new { vaultKeepId });
            return rowsAffected;
        }
    }
}