namespace Keepr.Models
{
    public class Vault : RepoItem<int>
    {
        public Profile Creator { get; set; }
        public string CreatorId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Img { get; set;}
        public bool IsPrivate { get; set; }
    }
}