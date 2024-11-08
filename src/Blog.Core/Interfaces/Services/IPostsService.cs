using Blog.Core.Data.DTOs;
using Blog.Core.Entities;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Interfaces.Services
{
	public interface IPostsService
	{
	    Task<Post>	Create(PostDTO postDto, ClaimsPrincipal User);
		Task<bool> Update(int id, PostDTO postDto, ClaimsPrincipal User);
		Task<bool> UserHasPermissionPost(int id, Post userPost, ClaimsPrincipal User);
		Post? GetPost(int id);
	}
}
