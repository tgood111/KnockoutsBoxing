namespace KnockoutsBoxing.Models
{
    public class Comment
    {
        public int CommentID { get; set; }

        public string CommentContent { get; set; }

        public string CommentAuthor { get; set; }

        //Foreign Key
        public int ArticleID { get; set; }

        public virtual Article Article { get; set; }
    }
}