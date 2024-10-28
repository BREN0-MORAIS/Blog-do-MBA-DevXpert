using Blog.Api.Models;
using Blog.Core.Entities;
using Blog.Core.Interfaces.Services;
using Blog.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Controllers
{
  
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private  IAuthService _authService {  get; set; }
        private SignInManager<Autor> _signInManager {  get; set; }
        public AuthController(IAuthService authService, SignInManager<Autor> signInManager)
        {
            _authService = authService;
            _signInManager = signInManager;
            
        }

        [HttpPost("registrar")]
        public async Task<ActionResult> Registrar(RegisterUserViewModel registerUser)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var user = new Autor
            {
                UserName = registerUser.Email,
                Email = registerUser.Email,
                EmailConfirmed = true
            };

            var result = await _authService.Register(user, registerUser.Password); 

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);


                return Ok(await  _authService.GenerateJwt(registerUser.Email));
            }

            return Problem("Falha ao registrar o usuário");
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginUserViewModel loginUser)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var result = await _authService.Login(loginUser.Email, loginUser.Password); 


            if (result.Succeeded)
            {
                return Ok(await _authService.GenerateJwt(loginUser.Email));
            }

            return Problem("Usuário ou senha incorretos");
        }

    }
}
