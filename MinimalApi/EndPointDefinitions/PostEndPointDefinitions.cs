using MinimalApi.Abstractions;
using Application.Posts.Queries;
using Application.Posts.Commands;
using Domain.Entities;
using MediatR;
using MinimalApi.Filters;

namespace MinimalApi.EndPointDefinitions;

public class PostEndPointDefinitions : IEndPointDefinitions
{
    public void RegisterEndpoints(WebApplication app)
    {
        var route = app.MapGroup("/api/posts/");

        route.MapGet("/{id}", GetPostById).WithName("GetPostById");
        route.MapGet("/", GetAllPost);
        route.MapPost("/", CreatePost).AddEndpointFilter<PostValidationFilter>();
        route.MapPut("/{id}", UpdatePost).AddEndpointFilter<PostValidationFilter>();
        route.MapDelete("/{id}", DeletePost);
    }

    private async Task<IResult> GetPostById(IMediator mediator, int id)
    {
        var getPost = new GetPostbyId { PostId = id };
        var post = await mediator.Send(getPost);
        return TypedResults.Ok(post);
    }

    private async Task<IResult> GetAllPost(IMediator mediator)
    {
        var getAllPosts = await mediator.Send(new GetAllPosts());
        return TypedResults.Ok(getAllPosts);
    }

    private async Task<IResult> CreatePost(IMediator mediator, Post post)
    {
        var createPost = new CreatePost { PostContent = post.Content };
        var createdPost = await mediator.Send(createPost);
        return Results.CreatedAtRoute("GetPostById", new { createdPost.Id }, createdPost);
    }

    private async Task<IResult> UpdatePost(ISender mediator,Post post,int id)
    {
        var updatePost = new UpdatePost { PostId = id, PostContent = post.Content };
        var updatedPost = await mediator.Send(updatePost);
        return TypedResults.Ok(updatedPost);
    }

    private async Task<IResult> DeletePost(ISender mediator,int id)
    {
        var deletePost = new DeletePost { PostId = id };
        await mediator.Send(deletePost);
        return TypedResults.NoContent();
    }
}