using AutoMapper;
using Blog.Core.Data.DTOs;
using Blog.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.AutoMapper
{
    public class CommentsProfile : Profile
    {
        public CommentsProfile()
        {
            CreateMap<Comentario, CommentsDTO>().ReverseMap();
        }
    }
}
