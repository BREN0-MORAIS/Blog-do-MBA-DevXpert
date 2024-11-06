using AutoMapper;
using Blog.Core.Data;
using Blog.Core.Data.DTOs;
using Blog.Core.Entities;
using Blog.Core.Interfaces.Repositories;
using Blog.Core.Interfaces.Services;
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
		private readonly IPostsRepository _postRepository;
		private readonly IPostsService _postsService;


		public PostController(ApplicationDbContext context, IMapper mapper, UserManager<Autor> userManager, IPostsRepository postRepository, IPostsService postService)
		{
			_postRepository = postRepository;
			_postsService = postService;
		}

		[AllowAnonymous]
		[HttpGet("GetAllPosts")]
		public async Task<IEnumerable<Post>> GetAllPosts()
		{
			return await _postRepository.GetAll("Comentarios,Autor");
		}

		[AllowAnonymous]
		[HttpGet("{id:int}")]
		public async Task<IActionResult> GetPost(int id)
		{
			return Ok(await _postRepository.GetById(id, "Comentarios,Autor"));
		}

		[HttpPost("CreatePost")]
		public async Task<IActionResult> CreatePost(PostDTO postDto)
		{
			if (postDto == null || !ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var post = await _postsService.Create(postDto, User);

			return CreatedAtAction(nameof(GetPost), new { id = post.Id }, post);

		}

		[HttpPut("{id:int}")]
		public async Task<IActionResult> UpdatePost(int id, PostDTO postDto)
		{
			if (postDto == null || !ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (!await _postsService.Update(id, postDto, User))
			{
				return Forbid();
			}

			return NoContent();
		}


		[HttpDelete("{id:int}")]
		public async Task<IActionResult> DeletePost(int id)
		{
			var userPost = await _postRepository.GetById(id);

			if (!await _postsService.UserHasPermissionPost(id, userPost, User))
			{
				return Forbid();
			}
		    _postRepository.Remove(userPost);
			await _postRepository.SaveAsync();

			return NoContent();
		}
	}
}
