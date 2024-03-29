﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace BLL
{
   public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<DAL.Users, DTO.UserDTO>();
                cfg.CreateMap<DAL.Posts, DTO.PostDTO>();
                cfg.CreateMap<DAL.Images, DTO.ImageDTO>();
                cfg.CreateMap<DAL.Comments, DTO.CommentDTO>()
                .ForMember(x => x.UserNickname, y => y.MapFrom(x => x.Users.NickName))
                .ForMember(x => x.DateString, y => y.MapFrom(x => x.Date.ToString("dd.MM.yyyy HH:mm")));
                cfg.CreateMap<DAL.Likes, DTO.LikesDTO>();
                
                cfg.CreateMap<DAL.PostTags, DTO.PostTagsDTO>();
                cfg.CreateMap<DAL.Roles, DTO.RoleDTO>();
                cfg.CreateMap<DAL.Love, DTO.Love>();
                cfg.CreateMap<DAL.User_Chat, DTO.ChatDTO>();
                cfg.CreateMap<DAL.Subscribtions, DTO.SubscribtionsDTO>();
                
                cfg.CreateMap<DTO.UserDTO, DAL.Users>();
                cfg.CreateMap<DTO.PostDTO, DAL.Posts>();
                cfg.CreateMap<DTO.ImageDTO, DAL.Images>();
                cfg.CreateMap<DTO.CommentDTO, DAL.Comments>();
                cfg.CreateMap<DTO.LikesDTO, DAL.Likes>();
                cfg.CreateMap<DTO.PostTagsDTO, DAL.PostTags>();
                cfg.CreateMap<DTO.RoleDTO, DAL.Roles>();
                cfg.CreateMap<DTO.Love, DAL.Love>();
                cfg.CreateMap<DTO.ChatDTO, DAL.User_Chat>();
                cfg.CreateMap<DTO.SubscribtionsDTO, DAL.Subscribtions>();
            }
            );
        }
    }
}
