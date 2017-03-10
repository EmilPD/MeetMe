﻿using MeetMe.Data.Models;
using MeetMe.Web.ViewModels.Contracts;
using System;
using System.Collections.Generic;
using AutoMapper;

namespace MeetMe.Web.ViewModels.Home
{
    public class PublicationViewModel : ICustomMappings
    {
        public string Content { get; set; }
        
        public string Author { get; set; }

        public string AuthorImageUrl { get; set; }

        public DateTime CreatedOn { get; set; }

        public int Likes { get; set; }

        public int Dislikes { get; set; }

        public IEnumerable<Comment> Comments { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Publication, PublicationViewModel>()
                    .ForMember(dest => dest.Author, opts => opts.MapFrom(src => $"{src.Author.FirstName} {src.Author.LastName}"));
        }
    }
}