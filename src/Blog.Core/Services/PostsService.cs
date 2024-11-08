using AutoMapper;
using Blog.Core.Data.DTOs;
using Blog.Core.Entities;
using Blog.Core.Interfaces.Repositories;
using Blog.Core.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Blog.Core.Data.Services
{
	public class PostsService : IPostsService
	{
		private readonly IPostsRepository _postRepository;
		private readonly UserManager<Autor> _userManager;
		private readonly IMapper _mapper;
		private readonly ApplicationDbContext _context;

		public PostsService(IPostsRepository postRepository, UserManager<Autor> userManager, IMapper mapper, ApplicationDbContext context)
		{
			_postRepository = postRepository;
			_userManager = userManager;
			_mapper = mapper;
			_context = context;
		}
		public Post? GetPost(int id)
		{
			return _context.Posts.
					Include(p => p.Comentarios)
						.ThenInclude(c => c.Autor)
					.Include(p => p.Autor).Where(x => x.Id == id).FirstOrDefault();
		}
		public async Task<Post> Create(PostDTO postDto, ClaimsPrincipal User)
		{
			var userId = _userManager.GetUserId(User);
			var post = _mapper.Map<PostDTO, Post>(postDto);

			post.AutorId = userId;

			post.CreatedDate();
			post.ChangedDate();
			await _postRepository.Create(post);
			await _postRepository.SaveAsync();

			return post;
		}

		public async Task<bool> Update(int id,PostDTO postDto, ClaimsPrincipal User)
		{

			var userPost = await _postRepository.GetById(id);

			if (!await UserHasPermissionPost(id, userPost, User))  return false;

			_mapper.Map(postDto, userPost);

			userPost.ChangedDate();

			_postRepository.Update(userPost);
			await _postRepository.SaveAsync();

			return true;

		}

		public async Task<bool> UserHasPermissionPost(int  id,Post userPost , ClaimsPrincipal User)
		{
			var userId = _userManager.GetUserId(User);
			var isAdmin = await _userManager.IsInRoleAsync(await _userManager.FindByIdAsync(userId), "Admin");
			if (userPost == null)
			{
				return false;
			}

			if (!userPost.AutorId.Equals(userId) && !isAdmin)
			{
				return false;
			}

			return true;
		}
			

	}
}
