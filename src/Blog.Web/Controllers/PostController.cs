using AutoMapper;
using Blog.Core.Data;
using Blog.Core.Data.DTOs;
using Blog.Core.Entities;
using Blog.Core.Interfaces.Repositories;
using Blog.Core.Interfaces.Services;
using Blog.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
    [Authorize]
	public class PostController : Controller
	{
		// GET: PostController

		private readonly IPostsRepository _postRepository;
		private readonly IPostsService _postsService;
		private readonly ICommentsService _commentsService;
		private readonly IMapper _mapper;
		private readonly UserManager<Autor> _userManager;

		public PostController(IPostsRepository postRepository, IPostsService postsService, ICommentsService commentsService, IMapper mapper, UserManager<Autor> userManager)
		{
			_postRepository = postRepository;
			_postsService = postsService;
			_commentsService = commentsService;
			_mapper = mapper;
			_userManager = userManager;
		}

		[AllowAnonymous]
		public async Task<ActionResult> Index(int id)
		{
			var PostComentarios = new PostComentarioViewModel();
			PostComentarios.Post = _postsService.GetPost(id);
			return View(PostComentarios);
		}

		public async Task<ActionResult> ListaPosts()
		{
			ListaPostViewModel listaPosts = new ListaPostViewModel();
			var userId = _userManager.GetUserId(User);
			var isAdmin = await _userManager.IsInRoleAsync(await _userManager.FindByIdAsync(userId), "Admin");
			var posts = await _postRepository.GetAll("Comentarios,Autor");
            if (!isAdmin)
				posts = posts.Where(x => x.AutorId == userId).OrderByDescending(x=> x.Id);
			listaPosts.Posts.AddRange(posts);

			return View("ListaPosts", listaPosts);
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Delete(int id)
		{
			try
			{
				var post = _postsService.GetPost(id);
				
				if(await _postsService.UserHasPermissionPost(id, post, User))
				{
					_postRepository.Remove(post);
					await _postRepository.SaveAsync();
				}
				return RedirectToAction("ListaPosts");
			}
			catch
			{
				return RedirectToAction("ListaPosts");
			}
		}

		public ActionResult UpInsert(int id)
		{
			if (id != 0)
			{
				var postDto = _mapper.Map<Post, PostDTO>(_postsService.GetPost(id));
				return View(postDto);
			}
			return View();
		}
		[HttpPost]
		public ActionResult CreateComment(string Comentario, int PostId)
		{
			var comentario = new CommentsDTO { PostId = PostId, Conteudo = Comentario};
			_commentsService.Create(comentario, User);
			return RedirectToAction("Index", "Post", new { id = PostId });
		}

		[HttpPost]
		public ActionResult UpInsert(PostDTO postDto)
		{
			if (ModelState.IsValid)
			{
		
				if (postDto.Id == 0)
					_postsService.Create(postDto, User);
				else
					_postsService.Update(postDto.Id,postDto, User);

				return RedirectToAction("Index", "Home"); 
			}
			return View("CreatePost", postDto); ;
		}
	}
}
