﻿using MeetMe.Data.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace MeetMe.Data.Contracts
{
    public interface IMeetMeDbContext
    {
        int SaveChanges();

        IDbSet<CustomUser> CustomUsers { get; set; }

        IDbSet<UserImage> UserImages { get; set; }

        IDbSet<ProfileImage> ProfileImages { get; set; }

        IDbSet<Comment> Comments { get; set; }

        IDbSet<Publication> Publications { get; set; }

        IDbSet<T> Set<T>() where T : class;

        DbEntityEntry<T> Entry<T>(T entity) where T : class;

        IEntryState<T> GetState<T>(T entity) where T : class;
    }
}
