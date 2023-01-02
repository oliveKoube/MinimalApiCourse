using Application.Posts.Queries;
using Application.Posts.Commands;
using Domain.Entities;
using MediatR;

namespace MinimalApi;

public static class MinimalApis
{
    public static WebApplication MinimalApi(this WebApplication application)
    {
        GetPostById(application);
        CreatePost(application);
        GetAllPosts(application);
        UpdatePost(application);
        DeletePost(application);

        return  application;
    }
    
    static void GetPostById(WebApplication webApplication)
    {
        webApplication.MapGet("/api/posts/{id}", async (ISender mediator, int id) =>
        {
            var getPost = new GetPostbyId { PostId = id };
            var post = await mediator.Send(getPost);
            return Results.Ok(post);
        }).WithName("GetPostById");
    }

    private static void GetAllPosts(WebApplication webApplication)
    {
        webApplication.MapGet("/api/posts", async (ISender mediator) =>
        {
            var getAllPosts = await mediator.Send(new GetAllPosts());
            return Results.Ok(getAllPosts);
        });
    }

    private static void CreatePost(WebApplication webApplication)
    {
        webApplication.MapPost("/api/posts", async (ISender mediator, Post post) =>
        {
            var createPost = new CreatePost { PostContent = post.Content };
            var createdPost = await mediator.Send(createPost);
            return Results.CreatedAtRoute("GetPostById", new { createdPost.Id }, createdPost);
        });
    }

    private static void UpdatePost(WebApplication webApplication)
    {
        webApplication.MapPut("/api/posts/{id}", async (ISender mediator,Post post,int id) =>
        {
            var updatePost = new UpdatePost { PostId = id, PostContent = post.Content };
            var updatedPost = await mediator.Send(updatePost);
            return Results.Ok(updatedPost);
        });
    }

    private static void DeletePost(WebApplication webApplication)
    {
        webApplication.MapDelete("/api/posts/{id}", async (ISender mediator,int id) =>
        {
            var deletePost = new DeletePost { PostId = id };
            await mediator.Send(deletePost);
            return Results.NoContent();
        });
    }
}