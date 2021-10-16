using IdentityInCore3.DAL.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityInCore3.DAL.Models
{
   public class Students:DomainEntity
    {
        public Students()
        {
            Subjects = new HashSet<Subjects>();
        }
        //public override long Id { get; set; }

        public string Name { get; set; }
        public EnumGender Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }

        public virtual ICollection<Subjects> Subjects { get; set; }

    }
}
