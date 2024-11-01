using AutoMapper;
using Blog.Core.Data;
using Blog.Core.Entities;
using Blog.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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

        public ActionResult Index()
		{
			return View();
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
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(IFormCollection collection)
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
		public ActionResult Delete(int id, IFormCollection collection)
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


        [HttpPost]
        public ActionResult Enviar(DadosViewModel dados)
        {
            if (ModelState.IsValid)
            {
                // Processa os dados, por exemplo, salvando em um banco de dados
                ViewBag.Mensagem = "Dados recebidos com sucesso!";
                return View("Index", dados); // Redireciona para uma página de confirmação
            }

            // Caso o modelo seja inválido, retorna para a view original com os dados e mensagens de erro
            return View("Index", dados);
        }

        public ActionResult EnviarDados()
        {
            return View();
        }
    }
}
