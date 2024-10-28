using AutoMapper;
using Blog.Core.Data;
using Blog.Core.Data.DTOs;
using Blog.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private ApplicationDbContext _context { get; set; }
        private readonly IMapper _mapper;

        public PostController(ApplicationDbContext context, IMapper mapper)
        {
            _context= context;
            _mapper= mapper;

        }
        [AllowAnonymous]
        [HttpGet("GetAllPosts")]
        public async Task<ActionResult<IEnumerable<Post>>> GetAllPosts()
        {
            var posts = await _context.Posts
               .Include(p => p.Comentarios)
               .Include(p => p.Autor).ToListAsync();

            return posts;
        }

        [AllowAnonymous]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Post>> GetPost(int id)
        {
            var post = await _context.Posts
                .Include(p => p.Comentarios)
                .Include(p => p.Autor)
                .FirstOrDefaultAsync(p => p.Id == id); 
            
            return post;
        }

        [HttpPost("CreatePost")]
        public async Task<ActionResult<Post>> CreatePost(PostDTO postDto)
        {
            var post = _mapper.Map<PostDTO, Post>(postDto);
            
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPost), new { id = post.Id }, post);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdatePost(int id, PostDTO postDto)
        {
            if (id != postDto.Id) return BadRequest();


            var postMap = _mapper.Map<PostDTO, Post>(postDto);

            _context.Entry(postMap).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var post = await _context.Posts.FindAsync(id);

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
