namespace LLP.Specification.Api.Blogs.BlogEndpoints;

[ApiController]
public class List : ControllerBase
{
    private readonly IMediator _mediator;

    public List(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("/blogs")]
    [SwaggerOperation(Summary = "List all Blogs", Tags = new[] { "Blogs" })]
    public async Task<ActionResult<List<BlogDto>>> ListBlogs(CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new BlogListRequest(), cancellationToken);

        return Ok(response);
    }
}

public record BlogListRequest() : IRequest<List<BlogDto>>;

public class BlogListHandler : IRequestHandler<BlogListRequest, List<BlogDto>>
{
    private readonly IReadRepository<Blog> _readRepository;

    public BlogListHandler(IReadRepository<Blog> readRepository)
    {
        _readRepository = readRepository;
    }

    public async Task<List<BlogDto>> Handle(BlogListRequest request, CancellationToken cancellationToken)
    {
        var spec = new BlogSpec();
        var result = await _readRepository.ProjectToListAsync<BlogDto>(spec, cancellationToken);

        return result;
    }
}