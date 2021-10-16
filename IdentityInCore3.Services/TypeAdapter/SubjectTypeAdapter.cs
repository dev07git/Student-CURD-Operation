using IdentityInCore3.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityInCore3.Services
{
   public class SubjectTypeAdapter
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
            if (source is Contracts.SubjectsViewModel && typeof(TTarget) == typeof(Subjects))
            {
                var contractSubjects = source as Contracts.SubjectsViewModel;
                var domainSubjects = new Subjects
                {
                    SubjectMasterId = contractSubjects.SubjectMasterId,
                    Marks=contractSubjects.Marks,
                    CreatedOn= DateTimeOffset.UtcNow,

                };

                return domainSubjects as TTarget;
            }
            else if (source is Subjects && typeof(TTarget) == typeof(Contracts.SubjectsViewModel))
            {
                Subjects domainSubjects = source as Subjects;

                var contractSubjects = new Contracts.SubjectsViewModel
                {
                    Id = domainSubjects.Id,
                    Marks = domainSubjects.Marks,
                    CreatedOn = domainSubjects.CreatedOn,
                    ModifiedOn = domainSubjects.ModifiedOn,
                    SubjectName = domainSubjects.SubjectMaster?.Name,
                    SubjectMasterId= domainSubjects.SubjectMasterId,
                    StudentId = domainSubjects.StudentId,
                };
                return contractSubjects as TTarget;

            }
            else
                return null;

        }
        private IList<TTarget> GetList<TTarget>(object source) where TTarget : class
        {
            if (source is IList<Subjects> && typeof(TTarget) == typeof(Contracts.SubjectsViewModel))
            {
                var contractSubjectsList = new List<Contracts.SubjectsViewModel>();
                Contracts.SubjectsViewModel contractSubjects = null;

                IList<Subjects> domainSubjectList = source as IList<Subjects>;
                foreach (var domainSubject in domainSubjectList)
                {
                    contractSubjects = Adapt<Contracts.SubjectsViewModel>(domainSubject);
                    contractSubjectsList.Add(contractSubjects);
                }
                return contractSubjectsList as List<TTarget>;

            }
            if (source is List<Contracts.SubjectsViewModel> && typeof(TTarget) == typeof(Subjects))
            {
                var  domainSubjectList = new List<Subjects>();
                Subjects domainSubjects = null;

                List<Contracts.SubjectsViewModel> contractSubjectsList = source as List<Contracts.SubjectsViewModel>;
                foreach (var subject in contractSubjectsList)
                {
                    domainSubjects = Adapt<Subjects>(subject);
                    domainSubjectList.Add(domainSubjects);
                }
                return domainSubjectList as List<TTarget>;

            }
            else return null;
        }
    }
}
