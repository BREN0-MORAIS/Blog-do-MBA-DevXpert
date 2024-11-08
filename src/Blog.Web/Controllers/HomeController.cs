using AutoMapper;
using Blog.Core.Data;
using Blog.Core.Entities;
using Blog.Core.Interfaces.Repositories;
using Blog.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Blog.Web.Controllers
{
    public class HomeController : Controller
    {

        private readonly IPostsRepository _postRepository;

        public HomeController(IPostsRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<IActionResult> IndexAsync()
        {
            ListaPostViewModel listaPosts = new ListaPostViewModel();

            var posts = await _postRepository.GetAll("Comentarios,Autor");

            listaPosts.Posts.AddRange(posts);


            return View(listaPosts);

        
        }


       
    }
}

