using Domain.Entities;
using MediatR;

namespace Application.Posts.Queries;

public class GetPostbyId : IRequest<Post> {
    public int PostId { get; set; }
}