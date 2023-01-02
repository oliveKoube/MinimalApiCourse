using Application.Abstractions;
using Application.Posts.Commands;
using Domain.Entities;
using MediatR;

namespace Application.Posts.CommandHandlers;

public class UpdatePosthandler : IRequestHandler<UpdatePost,Post> {

    private readonly IPostRepository _postRepository;

    public UpdatePosthandler(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<Post> Handle(UpdatePost request, CancellationToken cancellationToken)
    {
        return await _postRepository.UpdatePost(request.PostContent,request.PostId);
    }
}