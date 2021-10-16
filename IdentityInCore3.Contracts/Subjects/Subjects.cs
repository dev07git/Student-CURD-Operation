using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityInCore3.Contracts
{
    public class SubjectsViewModel
    {
        public long StudentId { get; set; }

        public long SubjectMasterId { get; set; }
        public string SubjectName { get; set; }
        public float Marks { get; set; }
        public long Id { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
    }
}
