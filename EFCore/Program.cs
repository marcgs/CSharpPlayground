using System;

namespace EFCore
{
    using System.Linq;
    using Model;

    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new BloggingContext())
            {
                // Create
                Console.WriteLine("Creating User with Profile");
                var user = new User {Name = "Marc"};
                user.Profile = new Profile { Url = "https://myprofile.com"};
                user.Profile.Blog = new Blog {Url = "https://marcgs.github.io/"};
                db.Add(user);
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
                //Console.WriteLine("Deleting data");
                //db.Remove(user);
                //db.SaveChanges();
            }
        }
    }
}
