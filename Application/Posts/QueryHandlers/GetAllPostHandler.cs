using Application.Abstractions;
using Application.Posts.Queries;
using Domain.Entities;
using MediatR;

namespace Application.Posts.QueryHandlers;

public class GetAllPostHandler : IRequestHandler<GetAllPosts,ICollection<Post>>
{
    private readonly IPostRepository _postRepository;

    public GetAllPostHandler(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }
    public async Task<ICollection<Post>> Handle(GetAllPosts request, CancellationToken cancellationToken)
    {
        return await _postRepository.GetAllPosts();
    }
}