namespace Keepr.Repositories
{
    public class KeepsRepository
    {
        private readonly IDbConnection _db;

        public KeepsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal Keep Insert(Keep keepData)
        {
            string sql = @"
            INSERT INTO keeps
            (name, description, img, views, kept, creatorId)
            VALUES
            (@name, @description, @img, @views, @kept, @creatorId);

            SELECT
            k.*,
            a.*
            FROM keeps k
            JOIN accounts a ON a.id = k.creatorId
            WHERE k.id = LAST_INSERT_ID();
            ";
            Keep keep = _db.Query<Keep, Account, Keep>(sql, (keep, creator) =>
            {
                keep.Creator = creator;
                return keep;
            }, keepData).FirstOrDefault();
            return keep;
        }

        public List<Keep> Get()
        {
            string sql = @"
            SELECT
            k.*, a.*
            FROM keeps k
            JOIN accounts a ON a.id = k.creatorId;
            ";
            List<Keep> keeps = _db.Query<Keep, Account, Keep>(sql, (keep, creator) =>
            {
                keep.Creator = creator;
                return keep;
            }).ToList();
            return keeps;
        }

        internal List<Keep> GetAll(string query)
        {
            query = '%' + query +'%';
            string sql = @"
            SELECT
            k.*,
            a.*
            FROM keeps k
            JOIN accounts a ON k.creatorId = a.id
            WHERE k.name LIKE @query;
            ";
            List<Keep> keeps = _db.Query<Keep, Profile, Keep>(sql, (keep, creator) =>
            {
                keep.Creator = creator;
                return keep;
            }, new { query }).ToList();
            return keeps;
        }

        public Keep GetOne(int id)
        {
            string sql = @"
            SELECT
            k.*,
            a.*
            FROM keeps k
            JOIN accounts a ON a.id = k.creatorId
            WHERE k.id = @id;
            ";
            Keep keep = _db.Query<Keep, Account, Keep>(sql, (keep, creator) =>
            {
                keep.Creator = creator;
                return keep;
            }, new { id }).FirstOrDefault();
            return keep;
        }

        internal void EditKeep(Keep originalKeep)
        {
            string sql = @"
            UPDATE keeps
            SET
            name = @Name,
            description = @Description,
            img = @Img
            WHERE id = @Id;
            ";
            _db.Execute(sql, originalKeep);
        }

        internal int Remove(int keepId)
        {
            string sql = "DELETE FROM keeps WHERE id = @keepId LIMIT 1;";
            int rowsAffected = _db.Execute(sql, new { keepId });
            return rowsAffected;
        }
    }
}