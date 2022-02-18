namespace LLP.Specification.Api.Blogs.BlogEndpoints;

[ApiController]
public class Delete : ControllerBase
{
    private readonly IMediator _mediator;

    public Delete(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpDelete("/blogs/{id}")]
    [SwaggerOperation(Summary = "Deletes a single Blog", Tags = new[] { "Blogs" })]
    public async Task<ActionResult<BlogDto>> DeleteBlog(Guid id, CancellationToken cancellationToken)
    {
        if (id.Equals(Guid.Empty)) return BadRequest();

        await _mediator.Send(new BlogDeleteRequest(id), cancellationToken);

        return Ok();
    }
}

public record BlogDeleteRequest(Guid Id) : IRequest;

public class BlogDeleteHandler : IRequestHandler<BlogDeleteRequest>
{
    private readonly IRepository<Blog> _repository;

    public BlogDeleteHandler(IRepository<Blog> repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(BlogDeleteRequest request, CancellationToken cancellationToken)
    {
        var spec = new BlogByIdSpec(request.Id);
        var blog = await _repository.FirstOrDefaultAsync(spec, cancellationToken);

        if (blog is null) throw new($"The blog with Id: {request.Id} is not found!");

        await _repository.DeleteAsync(blog, cancellationToken);

        return Unit.Value;
    }
}