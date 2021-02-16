namespace EFCore.Model
{
    public class Profile
    {
        public int ProfileId { get; set; }
        public string Url { get; set; }

        public int UserId { get; set; }

        public Blog Blog { get; set; }
    }
}