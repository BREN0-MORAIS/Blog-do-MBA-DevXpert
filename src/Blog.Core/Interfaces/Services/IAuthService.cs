using Blog.Core.Data.DTOs;
using Blog.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Interfaces.Services
{
    public interface  IAuthService
    {
         Task<IdentityResult> Register(Autor user, string password);
        Task<string> GenerateJwt(string email);
        Task<SignInResult> Login(string email, string password);
    }
}
