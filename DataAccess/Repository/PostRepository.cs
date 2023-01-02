using Application.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository;

public class PostRepository : IPostRepository {

    private readonly SocialDbContext _dbContext;

    public PostRepository(SocialDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ICollection<Post>> GetAllPosts()
    {
        return await _dbContext.Posts.ToListAsync();
    }

    public async Task<Post> GetPostById(int postId)
    {
        return await _dbContext.Posts.FirstOrDefaultAsync(x=>x.Id==postId);
    }

    public async Task<Post> CreatePost(Post toCreate)
    {
        toCreate.DateCreated = DateTime.Now;
        toCreate.LastModified = DateTime.Now;
        _dbContext.Posts.Add(toCreate);
        await _dbContext.SaveChangesAsync();
        return toCreate;
    }

    public async Task<Post> UpdatePost(string updatedContent, int postId)
    {
        var post = await _dbContext.Posts.FirstOrDefaultAsync(x=> x.Id==postId);
        post.LastModified = DateTime.Now;
        post.Content = updatedContent;
        await _dbContext.SaveChangesAsync();
        return post;
    }

    public async Task DeletePost(int postId)
    {
        var post = await _dbContext.Posts.FirstOrDefaultAsync(x=> x.Id==postId);
        if(post is null) return;
        _dbContext.Posts.Remove(post);
        await _dbContext.SaveChangesAsync();
    }
}