namespace EFCore.Model
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }

        // 1-1 relationship
        public int ProfileId { get; set; }
        public Profile Profile { get; set; }
    }
}