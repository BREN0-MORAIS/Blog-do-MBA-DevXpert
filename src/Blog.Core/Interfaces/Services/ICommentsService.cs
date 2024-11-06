using Blog.Core.Data.DTOs;
using Blog.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Interfaces.Services
{
	public interface ICommentsService
	{
	    Task<Comentario>	Create(CommentsDTO postDto, ClaimsPrincipal User);
		Task<bool> Update(int id, CommentsDTO postDto, ClaimsPrincipal User);
		Task<bool> UserHasPermission(int id, Comentario userPost, ClaimsPrincipal User);
	}
}
