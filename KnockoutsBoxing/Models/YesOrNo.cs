namespace KnockoutsBoxing.Models
{
    public class YesOrNo
    {
        //Primary Key
        public int YesOrNoID { get; set; }

        public bool FansSaidYesOrNO { get; set; }

        //Foreign Key
        public int PollID { get; set; }

        public virtual Poll Poll { get; set; }
    }
}