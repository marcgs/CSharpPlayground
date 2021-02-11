using System;

namespace EFCore
{
    using System.Linq;

    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new BloggingContext())
            {
                // Create
                Console.WriteLine("Inserting a new blog");
                db.Add(new Blog() {Url = "https://marcgs.github.io/"});
                db.SaveChanges();
                
                // Read
                var blog = db.Blogs
                    .OrderBy(b => b.BlogId)
                    .First();
                
                // Update
                Console.WriteLine("Updating the blog and adding a post");
                blog.Url = "https://marcgomez.ch";
                blog.Posts.Add(
                    new Post() { Title = "Hello World", Content = "Hello World!"}
                );
                db.SaveChanges();
                
                // Delete
                Console.WriteLine("Delete the blog");
                db.Remove(blog);
                db.SaveChanges();
            }
        }
    }
}
