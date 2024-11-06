using AutoMapper;
using Blog.Core.Data;
using Blog.Core.Data.DTOs;
using Blog.Core.Data.Repository;
using Blog.Core.Data.Services;
using Blog.Core.Entities;
using Blog.Core.Interfaces.Repositories;
using Blog.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {

		private readonly ICommentsRepository _commentsRepository;
		private readonly ICommentsService _commentsService;

		public CommentsController(ApplicationDbContext context, IMapper mapper, UserManager<Autor> userManager, ICommentsRepository commentsRepository, ICommentsService commentsService)
		{
			_commentsRepository = commentsRepository;
			_commentsService = commentsService;
		}

		[AllowAnonymous]
        [HttpGet("GetAllComments")]
        public async Task<IEnumerable<Comentario>> GetAllComments()
        {
            return await _commentsRepository.GetAll();
        }

        [AllowAnonymous]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Comentario>> GetComment(int id)
        {
            if (id == 0)  return NotFound();

            return await _commentsRepository.GetById(id, "Autor,Post");
        }

        [HttpPost("CreateComments")]
        public async Task<ActionResult<Post>> CreateComments(CommentsDTO commentsDTO)
		{
			if (commentsDTO == null || !ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var comments = await _commentsService.Create(commentsDTO, User);

            return CreatedAtAction(nameof(GetComment), new { id = comments.Id }, comments);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateComments(int id, CommentsDTO commentsDTO)
        {
			if (commentsDTO == null || !ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			if (!await _commentsService.Update(id, commentsDTO, User))
			{
				return Forbid();
			}
			return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
			var userPost = await _commentsRepository.GetById(id);

			if (!await _commentsService.UserHasPermission(id, userPost, User))
			{
				return Forbid();
			}
			_commentsRepository.Remove(userPost);
			await _commentsRepository.SaveAsync();

			return NoContent();
		}
    }
}
