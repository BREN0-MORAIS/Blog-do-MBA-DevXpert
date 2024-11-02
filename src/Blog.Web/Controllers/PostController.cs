using AutoMapper;
using Blog.Core.Data;
using Blog.Core.Data.DTOs;
using Blog.Core.Entities;
using Blog.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace Blog.Web.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        // GET: PostController

        private ApplicationDbContext _context { get; set; }
        private readonly IMapper _mapper;
        private readonly UserManager<Autor> _userManager;

        public PostController(ApplicationDbContext context, IMapper mapper, UserManager<Autor> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        [AllowAnonymous]
        public async Task<ActionResult> Index(int id)
        {
            var posCOmentario = new PostComentarioViewModel();
			var userId = _userManager.GetUserId(User);
			var isAdmin = await _userManager.IsInRoleAsync(await _userManager.FindByIdAsync(userId), "Admin");

			var post = _context.Posts
                       .Include(p => p.Comentarios)
                        .ThenInclude(c => c.Autor)
                       .Include(p => p.Autor)

                        .Where(x => x.Id == id).FirstOrDefault();


            posCOmentario.Post = post;


          





			return View(posCOmentario);
        }

        // GET: PostController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PostController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PostController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //	try
        //	{
        //		return RedirectToAction(nameof(Index));
        //	}
        //	catch
        //	{
        //		return View();
        //	}
        //}

        // GET: PostController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PostController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PostController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PostController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            var userId = _userManager.GetUserId(User);
            var isAdmin = await _userManager.IsInRoleAsync(await _userManager.FindByIdAsync(userId), "Admin");


            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult CreatePost()
        {
            return View();

        }
        [HttpPost]
        public ActionResult CreateComment(string Comentario, int PostId)
        {
            var userId = _userManager.GetUserId(User);

            var comentario = new Comentario { PostId = PostId, Conteudo = Comentario, AutorId = userId };

            comentario.CreatedDate();

            _context.Comentarios.Add(comentario);
            _context.SaveChanges();

            return RedirectToAction("Index", "Post", new { id = PostId });
        }

        [HttpPost]
        public ActionResult CreatePost(PostDTO postDto)
        {
            var userId = _userManager.GetUserId(User);
            if (ModelState.IsValid)
            {
                var post = _mapper.Map<PostDTO, Post>(postDto);
                post.AutorId = userId;

                post.CreatedDate();
                post.ChangedDate();


                _context.Posts.Add(post);
                _context.SaveChanges();
                // Processa os dados, por exemplo, salvando em um banco de dados
                ViewBag.Mensagem = "Dados recebidos com sucesso!";
                return RedirectToAction("Index", "Home"); // Redireciona para uma página de confirmação
            }

            // Caso o modelo seja inválido, retorna para a view original com os dados e mensagens de erro
            return View("CreatePost", postDto); ;
        }

        public ActionResult EnviarDados()
        {
            return View();
        }
    }
}
