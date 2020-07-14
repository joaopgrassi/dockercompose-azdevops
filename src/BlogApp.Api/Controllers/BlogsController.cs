using BlogApp.Api.Controllers.Models;
using BlogApp.Data;
using BlogApp.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace BlogApp.Api.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class BlogsController : ControllerBase
    {
        private readonly BlogDbContext _dbContext;

        public BlogsController(BlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("{id}", Name = nameof(GetById))]
        [ProducesResponseType(typeof(Blog), Status200OK)]
        [ProducesDefaultResponseType(typeof(string))]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var blog = await _dbContext.Blogs
                .Include(p => p.Posts)
                .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);

            if (blog == null)
                return NotFound("Blog not found");

            return Ok(blog);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Blog), Status201Created)]
        [ProducesDefaultResponseType(typeof(string))]
        public async Task<IActionResult> Create([FromBody] CreateBlogRequest createRequest, CancellationToken cancellationToken)
        {
            var blog = new Blog(Guid.NewGuid(), createRequest.Url);
            _dbContext.Add(blog);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return CreatedAtRoute(nameof(GetById), new { id = blog.Id }, blog);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Blog), Status204NoContent)]
        [ProducesDefaultResponseType(typeof(string))]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateBlogRequest updateRequest, CancellationToken cancellationToken)
        {
            var existingBlog = await _dbContext.Blogs
                .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);

            if (existingBlog == null)
                return NotFound("Blog not found");

            existingBlog.UpdateUrl(updateRequest.Url);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return NoContent();
        }
    }
}
