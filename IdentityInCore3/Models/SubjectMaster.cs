using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityInCore3.DAL.Models
{
   public class SubjectMaster:DomainEntity
    {
        //public override long Id { get; set; }
        public string Name { get; set; }

        public SubjectMaster()
        { 
            Subjects = new HashSet<Subjects>();
        }

        public virtual ICollection<Subjects> Subjects { get; set; }

    }
}
