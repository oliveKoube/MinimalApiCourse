using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class SocialDbContext : DbContext{

    public SocialDbContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<Post> Posts { get; set;}
}