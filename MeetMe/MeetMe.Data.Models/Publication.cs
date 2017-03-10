﻿using System;
using System.Collections.Generic;

namespace MeetMe.Data.Models
{
    public class Publication
    {
        public Publication()
        {
            this.Comments = new HashSet<Comment>();
        }

        public Publication(string content, int userId, DateTime createdOn)
            : this()
        {
            this.Content = content;
            this.CustomUserId = userId;
            this.CreatedOn = createdOn;
        }

        public int Id { get; set; }

        public string Content { get; set; }

        public int CustomUserId { get; set; }

        public virtual CustomUser Author { get; set; }

        public DateTime CreatedOn { get; set; }

        public int Likes { get; set; }

        public int Dislikes { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
