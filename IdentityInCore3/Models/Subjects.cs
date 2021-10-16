using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityInCore3.DAL.Models
{
    public class Subjects: DomainEntity
    {
        //public override long Id { get; set; }
        public long StudentId { get; set; }

        public long SubjectMasterId { get; set; }
        public float Marks { get; set; }

        public virtual Students Student { get; set; }
        public virtual SubjectMaster SubjectMaster { get; set; }


    }
}
