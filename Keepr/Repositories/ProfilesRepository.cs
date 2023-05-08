namespace Keepr.Repositories
{
    public class ProfilesRepository
    {
        private readonly IDbConnection _db;

        public ProfilesRepository(IDbConnection db)
        {
            _db = db;
        }

        public Profile GetProfile(string id)
        {
            string sql = @"
            SELECT
            a.*
            FROM accounts a
            WHERE a.id = @id;
            ";
            return _db.QueryFirstOrDefault<Profile>(sql, new { id });
        }

        internal List<Keep> GetKeeps(string profileId)
        {
            string sql = @"
            SELECT
            k.*,
            k.id keepId
            FROM keeps k
            WHERE k.creatorId = @profileId;
            ";
            List<Keep> keeps = _db.Query<Keep>(sql, new { profileId }).ToList();
            return keeps;
        }

        internal List<Vault> GetVaults(string profileId)
        {
            string sql = @"
            SELECT
            v.*,
            v.id vaultId
            FROM vaults v
            WHERE v.creatorId = @profileId;
            ";
            List<Vault> vaults = _db.Query<Vault>(sql, new { profileId }).ToList();
            return vaults;
        }
    }
}