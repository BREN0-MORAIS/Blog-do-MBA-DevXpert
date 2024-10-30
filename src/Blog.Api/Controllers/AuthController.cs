using AutoMapper;
using Blog.Api.Models;
using Blog.Core.Data.DTOs;
using Blog.Core.Entities;
using Blog.Core.Interfaces.Services;
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
        private readonly UserManager<Autor> _userManager;

        private readonly IMapper _mapper;
        public AuthController(IAuthService authService, SignInManager<Autor> signInManager, IMapper mapper, UserManager<Autor> userManager)
        {
            _authService = authService;
            _signInManager = signInManager;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpPost("registrar")]
        public async Task<ActionResult> Registrar(UserDTO userDto)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var existingUser = await _userManager.FindByEmailAsync(userDto.Email);
            if (existingUser != null)
            {
                return BadRequest("Um usuário com esse email já está registrado.");
            }

            var user = new Autor
            {
                UserName = userDto.Email,
                Email = userDto.Email,
                EmailConfirmed = true
            };

            var result = await _authService.Register(user, userDto.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
                await _signInManager.SignInAsync(user, isPersistent: false);


                var token = await _authService.GenerateJwt(userDto.Email);
                return Ok(token);
            }

            // Retorna os erros se o registro falhar
            return Problem("Falha ao registrar o usuário: " + string.Join(", ", result.Errors.Select(e => e.Description)));
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
