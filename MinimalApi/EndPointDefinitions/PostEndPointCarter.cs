using Application.Posts.Commands;
using Application.Posts.Queries;
using Carter;
using Domain.Entities;
using MediatR;
using MinimalApi.Filters;

namespace MinimalApi.EndPointDefinitions;

public class PostEndPointCarter : CarterModule
{
    public PostEndPointCarter() : base("/api/posts/")
    {

    }
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/{id}", GetPostById).WithName("GetPostById");
        app.MapGet("/", GetAllPost);
        app.MapPost("/", CreatePost).AddEndpointFilter<PostValidationFilter>();
        app.MapPut("/{id}", UpdatePost).AddEndpointFilter<PostValidationFilter>();
        app.MapDelete("/{id}", DeletePost);
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