﻿using AutoMapper;
using Blog.Core.Data.DTOs;
using Blog.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.AutoMapper
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
			//CreateMap<Post, PostDTO>().ReverseMap();

			CreateMap<PostDTO, Post>()
		  .ForMember(dest => dest.Id, opt => opt.Ignore()).ReverseMap();
		}
    }
}
