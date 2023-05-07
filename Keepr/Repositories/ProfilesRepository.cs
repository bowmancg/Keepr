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
            vk.*
            FROM keep k
            JOIN vaultKeep vk ON k.creatorId = k.creatorId
            WHERE vk.creatorId = @creatorId;
            ";
            List<Keep> keeps = _db.Query<Keep, Profile, Keep>(sql, (k, p) =>
            {
                k.Creator = p;
                return k;
            }, new { profileId }).ToList();
            return keeps;
        }
    }
}