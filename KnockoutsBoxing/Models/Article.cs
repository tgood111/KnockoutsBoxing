﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KnockoutsBoxing.Models
{
    public class Article
    {
        //this is the primary key
        public int ArticleID { get; set; }

        public string ArticleTitle { get; set; }

        public DateTime ArticleCreationDate { get; set; }

        public string ArticleAuthor { get; set; }

        public string ArticleContent { get; set; }

        public string ArticleCreatedBy { get; set; }

        public string ImageFileName { get; set; }

        [NotMapped]
        public HttpPostedFileBase imagefile { get; set; }

        //Collection of foreign keys
        public virtual ICollection<Comment> Comments { get; set; }
    }
}