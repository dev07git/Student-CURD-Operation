using IdentityInCore3.DAL.Enum;
using IdentityInCore3.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IdentityInCore3.Services
{
   public class StudentTypeAdapter
    {
        public TTarget Adapt<TTarget>(object requestSource) where TTarget : class, new()
        {
            return Get<TTarget>(requestSource);
        }
        public IList<TTarget> AdaptList<TTarget>(object requestSource) where TTarget : class, new()
        {
            return GetList<TTarget>(requestSource);
        }
        private TTarget Get<TTarget>(object source) where TTarget : class
        {
            if (source is Contracts.StudentsViewModel && typeof(TTarget) == typeof(Students))
            {
                var contractStudents = source as Contracts.StudentsViewModel;
                var domainStudents = new Students
                {
                    Name = contractStudents.Name,
                    PhoneNumber=contractStudents.PhoneNumber,
                    Address = contractStudents.Address,
                    PostalCode = contractStudents.PostalCode,
                    CreatedOn = contractStudents.CreatedOn,
                    ModifiedOn = contractStudents.ModifiedOn,
                    Gender=(EnumGender)contractStudents.Gender,
                };
                var domainsubjectList = new SubjectTypeAdapter().AdaptList<Subjects>(contractStudents.Subjects);
                if (domainsubjectList != null && domainsubjectList.Count > 0)
                    domainStudents.Subjects = domainsubjectList;

                return domainStudents as TTarget;
            }
            else if (source is Students && typeof(TTarget) == typeof(Contracts.StudentsViewModel))
            {
                Students domainStudents = source as Students;

                var contractStudents = new Contracts.StudentsViewModel
                {
                    Id = domainStudents.Id,
                    Name = domainStudents.Name,
                    PhoneNumber = domainStudents.PhoneNumber,
                    Address = domainStudents.Address,
                    PostalCode = domainStudents.PostalCode,
                    CreatedOn = domainStudents.CreatedOn,
                    ModifiedOn = domainStudents.ModifiedOn,
                    Gender = (Contracts.Enum.EnumGender)domainStudents.Gender,
                };
                contractStudents.Subjects = new SubjectTypeAdapter().AdaptList<Contracts.SubjectsViewModel>(domainStudents.Subjects.ToList());
                return contractStudents as TTarget;

            }
            else
                return null;

        }
        private IList<TTarget> GetList<TTarget>(object source) where TTarget : class
        {
            if (source is IList<Students> && typeof(TTarget) == typeof(Contracts.StudentsViewModel))
            {
                var contractStudentsList = new List<Contracts.StudentsViewModel>();
                Contracts.StudentsViewModel contractStudents = null;

                IList<Students> domainStudentsList = source as List<Students>;
                foreach (var student in domainStudentsList)
                {
                    contractStudents = Adapt<Contracts.StudentsViewModel>(student);
                    contractStudentsList.Add(contractStudents);
                }
                return contractStudentsList as List<TTarget>;

            }
            if (source is IList<Contracts.StudentsViewModel> && typeof(TTarget) == typeof(Students))
            {
                var domainStudentList = new List<Students>();
                Contracts.StudentsViewModel contractStudents = null;
                Students domainStudent;
                IList<Contracts.StudentsViewModel> contractStudentsList = source as List<Contracts.StudentsViewModel>;
                foreach (var student in contractStudentsList)
                {
                    domainStudent = Adapt<Students>(student);
                    domainStudentList.Add(domainStudent);
                }
                return domainStudentList as List<TTarget>;

            }
            else return null;
        }
    }
}
