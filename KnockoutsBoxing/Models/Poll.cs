using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace KnockoutsBoxing.Models
{
    public class Poll
    {

        //Primary Key
        public int PollID { get; set; }

        public string PollName { get; set; }

        public DateTime PollCreationDate { get; set; }

        public string PollBoxer1 { get; set; }

        public string PollBoxer2 { get; set; }

        public DateTime PollClosingDate { get; set; }

        public string PollCreatedBy { get; set; }

        //public virtual ICollection<ToyPurchaseDate> ToyPurchaseDates { get; set; }
        //Collection of foreign keys
        public virtual ICollection<YesOrNo> PollYesOrNoCollection { get; set; }
    }

    public class KnockoutsBoxingContext : DbContext
    {
        //create a set of Polls
        public DbSet<Poll> Polls { get; set; }
        public DbSet<YesOrNo> YesOrNos { get; set; }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}

/*
 *     public class CatContext : DbContext
    {
        public DbSet<Cat> Cats { get; set; }

        public System.Data.Entity.DbSet<WorkingWithData1.Models.ToyPurchaseDate> ToyPurchaseDates { get; set; }
    }
 * */

/*User Story Scenario.
•	Lets say, we have a user called Rigo. He has already registered. Fan Role. (please note, we have already written the code) 
•	Rigo would like to setup a Poll. 
•	Rigo would add a PollName
•	Poll will have a PollCreationDate
•	Poll is going to have Two Boxers – PollBoxer1 and PollBoxer2
•	Poll will have a PollVenue
•	Poll needs to have a PollClosingDate
•	Poll Yes or No called PollYesOrNoCollection
•	When the Poll is closed, we should be able to show, how many said yes, and how many said no. 

 */
