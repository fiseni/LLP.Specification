namespace LLP.Specification.Api.Blogs.BlogEndpoints;

[ApiController]
public class Get : ControllerBase
{
    private readonly IMediator _mediator;

    public Get(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("/blogs/{id}")]
    [SwaggerOperation(Summary = "Gets a single Blog", Tags = new[] { "Blogs" })]
    public async Task<ActionResult<BlogDto>> GetBlog(Guid id, CancellationToken cancellationToken)
    {
        if (id.Equals(Guid.Empty)) return BadRequest();

        var response = await _mediator.Send(new BlogGetByIdRequest(id), cancellationToken);

        return Ok(response);
    }
}

public record BlogGetByIdRequest(Guid Id) : IRequest<BlogDto>;

public class BlogGetByIdHandler : IRequestHandler<BlogGetByIdRequest, BlogDto>
{
    private readonly IReadRepository<Blog> _readRepository;

    public BlogGetByIdHandler(IReadRepository<Blog> readRepository)
    {
        _readRepository = readRepository;
    }

    public async Task<BlogDto> Handle(BlogGetByIdRequest request, CancellationToken cancellationToken)
    {
        var spec = new BlogByIdSpec(request.Id);
        var result = await _readRepository.ProjectToFirstOrDefaultAsync<BlogDto>(spec, cancellationToken);

        if (result is null) throw new($"The blog with Id: {request.Id} is not found!");

        return result;
    }
}