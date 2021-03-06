﻿using System;

namespace Training.Domain
{
    public class Post : Entity
    {
        public Post()
        {
            CreationDateTimeUtc = DateTime.UtcNow;
        }

        public string Title { get; set; }

        public string Body { get; set; }

        public DateTime CreationDateTimeUtc { get; set; }

        public virtual Category Category { get; set; }
    }
}
