using Assesment.Models;
using Microsoft.EntityFrameworkCore;

namespace Assesment.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data for UserProfiles
            modelBuilder.Entity<UserProfile>().HasData(
                new UserProfile
                {
                    UserID = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "johndoe@example.com",
                    DateOfBirth = new DateTime(1990, 1, 1)
                },
                new UserProfile
                {
                    UserID = 2,
                    FirstName = "Jane",
                    LastName = "Smith",
                    Email = "janesmith@example.com",
                    DateOfBirth = new DateTime(1992, 5, 15)
                }
            );

            // Seed data for Posts
            modelBuilder.Entity<Post>().HasData(
                new Post
                {
                    PostID = 1,
                    UserID = 1,
                    Title = "Introduction to C#",
                    Content = "This is a post about C# basics.",
                    DatePosted = new DateTime(2024, 1, 10)
                },
                new Post
                {
                    PostID = 2,
                    UserID = 1,
                    Title = "ASP.NET Core Overview",
                    Content = "This post covers ASP.NET Core fundamentals.",
                    DatePosted = new DateTime(2024, 2, 15)
                },
                new Post
                {
                    PostID = 3,
                    UserID = 2,
                    Title = "JavaScript Tips",
                    Content = "JavaScript tips for beginners.",
                    DatePosted = new DateTime(2024, 3, 20)
                }
            );
        }
    }
}
