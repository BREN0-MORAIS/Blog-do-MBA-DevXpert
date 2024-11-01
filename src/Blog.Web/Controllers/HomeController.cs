using AutoMapper;
using Blog.Core.Data;
using Blog.Core.Entities;
using Blog.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Blog.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ApplicationDbContext _context { get; set; }
        private readonly IMapper _mapper;
        private readonly UserManager<Autor> _userManager;

        public HomeController(ApplicationDbContext context, IMapper mapper, UserManager<Autor> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }
       

        public  IActionResult Index()
        {
            ListaPostViewModel listaPosts = new ListaPostViewModel();
            
            var posts = _context.Posts
                        .Include(p => p.Comentarios)
                        .Include(p => p.Autor).ToList().OrderByDescending(x=> x.Id);

            listaPosts.ListaPosts.AddRange(posts);


            return View(listaPosts);

        
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

       
    }
}

