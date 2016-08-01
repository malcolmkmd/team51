﻿using Bursify.Data.User;

namespace Bursify.Data.EF.SponsorUser
{
    //public class SponsorshipRequirement : IEntity
    public class SponsorshipRequirement : IBridgeEntity
    {
        //public int ID { get; set; }
        public int SponsorshipId { get; set; }
        public int SubjectId { get; set; }
        public int RequiredMark { get; set; }

        public virtual Sponsorship Sponsorship { get; set; }
        public virtual Subject Subject { get; set; }

        public int leftId
        {
            get { return SponsorshipId; }
        }

        public int rightId
        {
            get { return SubjectId; }
        }
    }
}