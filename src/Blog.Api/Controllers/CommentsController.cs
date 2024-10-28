using AutoMapper;
using Blog.Core.Data;
using Blog.Core.Data.DTOs;
using Blog.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {

        private ApplicationDbContext _context { get; set; }
        private readonly IMapper _mapper;

        public CommentsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        [AllowAnonymous]
        [HttpGet("GetAllComments")]
        public async Task<ActionResult<IEnumerable<Comentario>>> GetAllComments()
        {
            return await _context.Comentarios.ToListAsync();
        }

        [AllowAnonymous]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Comentario>> GetComment(int id)
        {
            var comentario = await _context.Comentarios
                .Include(p => p.Autor)
                .Include(p => p.Post)
                .FirstOrDefaultAsync(p => p.Id == id);

            return comentario;
        }

        [HttpPost("CreateComments")]
        public async Task<ActionResult<Post>> CreateComments(CommentsDTO commentsDTO)
        {
            var comments = _mapper.Map<CommentsDTO, Comentario>(commentsDTO);

            _context.Comentarios.Add(comments);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetComment), new { id = comments.Id }, comments);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateComments(int id, CommentsDTO commentsDTO)
        {
            if (id != commentsDTO.Id) return BadRequest();


            var postMap = _mapper.Map<CommentsDTO, Comentario>(commentsDTO);

            _context.Entry(postMap).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var post = await _context.Comentarios.FindAsync(id);

            _context.Comentarios.Remove(post);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
