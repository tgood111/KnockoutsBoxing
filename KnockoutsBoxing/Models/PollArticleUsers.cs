using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KnockoutsBoxing.Models
{
    public class PollArticleUsers
    {
        //public virtual ICollection<Comment> Comments { get; set; }

        public int PollArticleUsersID { get; set; }

        public virtual ICollection<Poll> Polls { get; set; }
        public virtual ICollection<Article> Articles { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}