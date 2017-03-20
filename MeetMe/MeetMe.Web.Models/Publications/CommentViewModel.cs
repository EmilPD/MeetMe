﻿using System;
using AutoMapper;
using MeetMe.Data.Models;
using MeetMe.Web.Models.Contracts;

namespace MeetMe.Web.Models.Publications
{
    public class CommentViewModel : ICustomMappings
    {
        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Author { get; set; }

        public byte[] Image { get; set; }

        public string AuthorImageUrl { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Comment, CommentViewModel>()
                    .ForMember(dest => dest.Author, opts => opts.MapFrom(src => $"{src.Author.FirstName} {src.Author.LastName}"))
                    .ForMember(dest => dest.Image, opts => opts.MapFrom(src => src.Author.ProfileImage.Content));
        }
    }
}