namespace Keepr.Services
{
    public class KeepsService
    {
        private readonly KeepsRepository _repo;
        
        public KeepsService(KeepsRepository repo)
        {
            _repo = repo;
        }

        internal Keep CreateKeep(Keep keepData)
        {
            Keep keep = _repo.Insert(keepData);
            return keep;
        }

        internal List<Keep> GetKeeps()
        {
            List<Keep> keeps = _repo.Get();
            return keeps;
        }

        internal List<Keep> Get(string query)
        {
            List<Keep> keeps = _repo.GetAll(query);
            return keeps;
        }

        internal Keep GetOne(int keepId)
        {
            Keep keep = _repo.GetOne(keepId);
            if (keep == null)
            {
                throw new Exception($"Bad Id: {keepId}");
            }
            return keep;
        }

        internal Keep EditKeep(Keep keepData, int keepId, string userId)
        {
            Keep originalKeep = this.GetOne(keepId);
            if (originalKeep.CreatorId != userId)
            {
                throw new Exception("You cannot edit this.");
            }
            originalKeep.Name = keepData.Name ?? originalKeep.Name;
            originalKeep.Description = keepData.Description ?? originalKeep.Description;
            originalKeep.Img = keepData.Img ?? originalKeep.Img;
            _repo.EditKeep(originalKeep);
            originalKeep.UpdatedAt = DateTime.Now;
            return originalKeep;
        }

        internal string Remove(int keepId, string userId)
        {
            Keep keep = this.GetOne(keepId);
            if (keep.CreatorId != userId)
            {
                throw new Exception("You cannot delete this.");
            }
            int rowsAffected = _repo.Remove(keepId);
            if (rowsAffected == 0)
            {
                throw new Exception("Delete Failed.");
            }
            if (rowsAffected > 1)
            {
                throw new Exception("Faulty Delete...");
            }
            return $"{keep.Name} has been deleted.";
        }
    }
}