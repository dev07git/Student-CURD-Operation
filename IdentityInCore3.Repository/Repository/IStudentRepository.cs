using IdentityInCore3.DAL.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IdentityInCore3.Repository
{
   public interface IStudentRepository
    {
        Task<Students> GetDomainObjectByName(string name, long id);
        Task<Students> GetDomainObject(long id);
        Task<List<Students>> GetDomainObjects();
        Task<List<Students>> GetDomainObjects(IList<long> studentIds);
        Task<bool> UpdateDomainObjectUsing_SP(Contracts.StudentsViewModel students);
        Task<bool> DeleteDomainObjectUsing_SP(long id);
    }
}