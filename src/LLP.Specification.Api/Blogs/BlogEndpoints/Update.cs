namespace LLP.Specification.Api.Blogs.BlogEndpoints;

[ApiController]
public class Update : ControllerBase
{
    private readonly IMediator _mediator;

    public Update(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPut("/blogs")]
    [SwaggerOperation(Summary = "Updates a new Blog", Tags = new[] { "Blogs" })]
    public async Task<ActionResult<BlogDto>> UpdateBlog(BlogUpdateDto blogUpdateDto, CancellationToken cancellationToken)
    {
        if (blogUpdateDto is null) return BadRequest();
        if (blogUpdateDto.Id.Equals(Guid.Empty)) return BadRequest();

        var response = await _mediator.Send(new BlogUpdateRequest(blogUpdateDto), cancellationToken);

        return Ok(response);
    }
}

public record BlogUpdateRequest(BlogUpdateDto BlogUpdateDto) : IRequest<BlogDto>;

public class BlogUpdateHandler : IRequestHandler<BlogUpdateRequest, BlogDto>
{
    private readonly IRepository<Blog> _repository;
    private readonly IMapper _mapper;

    public BlogUpdateHandler(IRepository<Blog> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<BlogDto> Handle(BlogUpdateRequest request, CancellationToken cancellationToken)
    {
        var spec = new BlogByIdSpec(request.BlogUpdateDto.Id);
        var blog = await _repository.FirstOrDefaultAsync(spec, cancellationToken);

        if (blog is null) throw new($"The blog with Id: {request.BlogUpdateDto.Id} is not found!");

        _mapper.Map(request.BlogUpdateDto, blog);

        await _repository.UpdateAsync(blog, cancellationToken);

        var response = _mapper.Map<BlogDto>(blog);

        return response;
    }
}