namespace LLP.Specification.Api.Blogs.BlogEndpoints;

[ApiController]
public class Create : ControllerBase
{
    private readonly IMediator _mediator;

    public Create(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("/blogs")]
    [SwaggerOperation(Summary = "Creates a new Blog", Tags = new[] { "Blogs" })]
    public async Task<ActionResult<BlogDto>> CreateBlog(BlogCreateDto blogCreateDto, CancellationToken cancellationToken)
    {
        if (blogCreateDto is null) return BadRequest();

        var response = await _mediator.Send(new BlogCreateRequest(blogCreateDto), cancellationToken);

        return Ok(response);
    }
}

public record BlogCreateRequest(BlogCreateDto BlogCreateDto) : IRequest<BlogDto>;

public class BlogCreateHandler : IRequestHandler<BlogCreateRequest, BlogDto>
{
    private readonly IRepository<Blog> _repository;
    private readonly IMapper _mapper;

    public BlogCreateHandler(IRepository<Blog> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<BlogDto> Handle(BlogCreateRequest request, CancellationToken cancellationToken)
    {
        var newBlog = _mapper.Map<Blog>(request.BlogCreateDto);

        await _repository.AddAsync(newBlog, cancellationToken);

        var response = _mapper.Map<BlogDto>(newBlog);

        return response;
    }
}