using Domain.Entities;
using MediatR;

namespace Application.Posts.Commands;

public class CreatePost : IRequest<Post>
{
    public string? PostContent { get; set; }
}