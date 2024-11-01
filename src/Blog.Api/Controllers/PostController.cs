using AutoMapper;
using Blog.Core.Data;
using Blog.Core.Data.DTOs;
using Blog.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Security.Claims;

namespace Blog.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private ApplicationDbContext _context { get; set; }
        private readonly IMapper _mapper;
        private readonly UserManager<Autor> _userManager;

        public PostController(ApplicationDbContext context, IMapper mapper , UserManager<Autor> userManager)
        {
            _context= context;
            _mapper= mapper;
            _userManager = userManager;

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
        public async Task<IActionResult> GetPost(int id)
        {
            var post = await _context.Posts
                .Include(p => p.Comentarios)
                .Include(p => p.Autor)
                .FirstOrDefaultAsync(p => p.Id == id); 
            
            return Ok(post);
        }

        [HttpPost("CreatePost")]
        public async Task<IActionResult> CreatePost(PostDTO postDto)
        {

            var userId = _userManager.GetUserId(User);


            var post = _mapper.Map<PostDTO, Post>(postDto);
            post.AutorId = userId;
                post.CreatedDate();
                post.ChangedDate();
                _context.Posts.Add(post);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetPost), new { id = post.Id }, post);
          
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdatePost(int id, PostDTO postDto)
        {

            var userId = _userManager.GetUserId(User);
            var isAdmin = await _userManager.IsInRoleAsync(await _userManager.FindByIdAsync(userId), "Admin");
            var userPost = await _context.Posts.FindAsync(id); // Remova AsNoTracking aqui

            if (userPost == null)
            {
                return NotFound();
            }

           
            if (!userPost.AutorId.Equals(userId) && !isAdmin)
            {
                return Forbid(); 
            }


            _mapper.Map(postDto, userPost); 

            userPost.ChangedDate(); 

            try
            {
                await _context.SaveChangesAsync(); 
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict("Os dados foram alterados por outro usuário. Tente novamente."); // Retorna 409 Conflict
            }

            return NoContent(); // Retorna 204 No Content se a atualização for bem-sucedida
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var userId = _userManager.GetUserId(User);
            var isAdmin = await _userManager.IsInRoleAsync(await _userManager.FindByIdAsync(userId), "Admin");
            var userPost = await _context.Posts.FindAsync(id);



            if (!userPost.AutorId.Equals(userId) && !isAdmin)
            {
                return Forbid();
            }


            var post = await _context.Posts.FindAsync(id);

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
