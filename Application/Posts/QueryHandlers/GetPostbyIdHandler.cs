using Application.Abstractions;
using Application.Posts.Queries;
using Domain.Entities;
using MediatR;

namespace Application.Posts.QueryHandlers;

public class GetPostbyIdHandler : IRequestHandler<GetPostbyId,Post>
{
    private readonly IPostRepository _postRepository;

    public GetPostbyIdHandler(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<Post> Handle(GetPostbyId request, CancellationToken cancellationToken)
    {
        return await _postRepository.GetPostById(request.PostId);
    }
}