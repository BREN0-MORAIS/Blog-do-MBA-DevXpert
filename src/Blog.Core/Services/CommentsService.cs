using AutoMapper;
using Blog.Core.Data.DTOs;
using Blog.Core.Entities;
using Blog.Core.Interfaces.Repositories;
using Blog.Core.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Blog.Core.Data.Services
{
	public class CommentsService : ICommentsService
	{
		private readonly ICommentsRepository _commentsRepository;
		private readonly UserManager<Autor> _userManager;
		private readonly IMapper _mapper;

		public CommentsService(ICommentsRepository commentsRepository, UserManager<Autor> userManager, IMapper mapper)
		{
			_commentsRepository = commentsRepository;
			_userManager = userManager;
			_mapper = mapper;
		}
		public async Task<Comentario> Create(CommentsDTO commentsDTO, ClaimsPrincipal User)
		{
			var userId = _userManager.GetUserId(User);
			var comments = _mapper.Map<CommentsDTO, Comentario>(commentsDTO);
			comments.AutorId = userId;
			comments.CreatedDate();
			comments.ChangedDate();
			await _commentsRepository.Create(comments);
			await _commentsRepository.SaveAsync();

			return comments;
		}

		public async Task<bool> Update(int id, CommentsDTO commentsDTO, ClaimsPrincipal User)
		{
			var UserComments = await _commentsRepository.GetById(id);

			if (!await UserHasPermission(id, UserComments, User))  return false;

			_mapper.Map(commentsDTO, UserComments);

			UserComments.ChangedDate();

			_commentsRepository.Update(UserComments);
			await _commentsRepository.SaveAsync();

			return true;
		}

		public async Task<bool> UserHasPermission(int  id,Comentario UserComments , ClaimsPrincipal User)
		{

			var userId = _userManager.GetUserId(User);
			var isAdmin = await _userManager.IsInRoleAsync(await _userManager.FindByIdAsync(userId), "Admin");

			if (UserComments == null)
			{
				return false;
			}

			if (!UserComments.AutorId.Equals(userId) && !isAdmin)
			{
				return false;
			}

			return true;
		}
			

	}
}
