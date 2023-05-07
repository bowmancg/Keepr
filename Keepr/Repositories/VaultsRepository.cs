namespace Keepr.Repositories
{
    public class VaultsRepository
    {
        private readonly IDbConnection _db;

        public VaultsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal Vault Insert(Vault vaultData)
        {
            string sql = @"
            INSERT INTO vaults
            (name, description, img, creatorId)
            VALUES
            (@name, @description, @img, @creatorId);

            SELECT
            v.*,
            a.*
            FROM vaults v
            JOIN accounts a ON a.id = v.creatorId
            WHERE v.id = LAST_INSERT_ID();
            ";
            Vault vault = _db.Query<Vault, Account, Vault>(sql, (vault, creator) => 
            {
                vault.Creator = creator;
                return vault;
            }, vaultData).FirstOrDefault();
            return vault;
        }

        public Vault GetOne(int id)
        {
            string sql = @"
            SELECT
            v.*,
            a.*
            FROM vaults v
            JOIN accounts a ON a.id = v.creatorId
            WHERE v.id = @id;
            ";
            Vault vault = _db.Query<Vault, Account, Vault>(sql, (vault, creator) =>
            {
                vault.Creator = creator;
                return vault;
            }, new { id }).FirstOrDefault();
            return vault;
        }

        internal void EditVault(Vault originalVault)
        {
            string sql = @"
            UPDATE vaults
            SET
            name = @Name,
            description = @Description,
            img = @Img
            WHERE id = @Id;
            ";
            _db.Execute(sql, originalVault);
        }

        internal int Remove(int vaultId)
        {
            string sql = "DELETE FROM vaults WHERE id = @vaultId LIMIT 1;";
            int rowsAffected = _db.Execute(sql, new { vaultId });
            return rowsAffected;
        }
    }
}