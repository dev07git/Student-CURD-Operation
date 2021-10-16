using IdentityInCore3.Contracts.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityInCore3.Contracts
{
   public class StudentsViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public EnumGender Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }

        public IList<SubjectsViewModel> Subjects { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
    }
}
