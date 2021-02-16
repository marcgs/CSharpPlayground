namespace EFCore.Model
{
    using System.Collections.Generic;

    public class Blog
    {
        public int BlogId { get; set; }
        public string Url { get; set; }

        public List<Post> Posts { get; } = new List<Post>();

        public int ProfileId { get; set; }
        public Profile Profile { get; set; }
    }
}