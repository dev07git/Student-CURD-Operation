using IdentityInCore3.DAL.Models;
using IdentityInCore3.Repository;
using IdentityInCore3.Services.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IdentityInCore3.Services
{
  public interface IStudentService
    {
        Task<Result<Contracts.StudentsViewModel>> Validate(Contracts.StudentsViewModel model);
        Task<Result<Contracts.StudentsViewModel>> Create(Contracts.StudentsViewModel model);
        Task<Result<Contracts.StudentsViewModel>> Update(Contracts.StudentsViewModel model);
        Task<Result<Contracts.StudentsViewModel>> Update_Using_SP(Contracts.StudentsViewModel model);
        Task<Result<IList<Contracts.StudentsViewModel>>> GetAll();

        Task<Result<Contracts.StudentsViewModel>> Get(long Id);

        Task<Result<string>> Delete(long Id);
        Task<Result<string>> Delete_Using_SP(long Id);
        Task<Result<string>> ReadUpdateFromJson();

    }
}
