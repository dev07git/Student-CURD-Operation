using IdentityInCore3.DAL.Models;
using IdentityInCore3.Repository;
using IdentityInCore3.Services.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IdentityInCore3.Services
{
    public class StudentService : IStudentService
    {
        readonly IDataRepository<Students> _studentDataRepository;
        readonly IStudentRepository _studentCustomRepository;

        public StudentService(IDataRepository<Students> studentDataRepository, IStudentRepository studentCustomRepository/*, IWebHostEnvironment env*/)
        {
            this._studentDataRepository = studentDataRepository;
            this._studentCustomRepository = studentCustomRepository;
            //_env = env;
        }

        public async Task<Result<Contracts.StudentsViewModel>> Validate(Contracts.StudentsViewModel model)
        {
            Result<Contracts.StudentsViewModel> returnModel = new Result<Contracts.StudentsViewModel>();
            Students domainStudent = null;
            returnModel.isSuccess = true;
            if (model == null)
            {
                returnModel.isSuccess = false;
                returnModel.ErrorCode = (int)HttpStatusCode.BadRequest;
                returnModel.ErrorMessage = returnModel.ErrorDetail = Contracts.Utility.Constants.InvalidRequestObject;
                return returnModel;
            }
            if (string.IsNullOrEmpty(model.Name))
            {
                returnModel.isSuccess = false;
                returnModel.ErrorCode = (int)HttpStatusCode.Conflict;
                returnModel.ErrorMessage = returnModel.ErrorDetail = string.Format(Contracts.Utility.Constants.ItemRequired,
Contracts.Utility.Constants.lblStudentName);
                return returnModel;
            }
            domainStudent = await _studentCustomRepository.GetDomainObjectByName(model.Name, model.Id);
            if (domainStudent != null)
            {
                returnModel.isSuccess = false;
                returnModel.ErrorCode = (int)HttpStatusCode.Conflict;
                returnModel.ErrorMessage = returnModel.ErrorDetail = string.Format(Contracts.Utility.Constants.Validation,
                    domainStudent.Name);
                return returnModel;
            }



            returnModel.GetResult(model);
            return returnModel;
        }
        public async Task<Result<Contracts.StudentsViewModel>> Create(Contracts.StudentsViewModel model)
        {
            Result<Contracts.StudentsViewModel> returnModel = new Result<Contracts.StudentsViewModel>();

            returnModel = await Validate(model);
            if (!returnModel.isSuccess)
                return returnModel;
            model.CreatedOn = DateTimeOffset.UtcNow;
            StudentTypeAdapter studentTypeAdapter = new StudentTypeAdapter();

            Students domainStudent = studentTypeAdapter.Adapt<Students>(model);
            domainStudent = await _studentDataRepository.Add(domainStudent);
            returnModel.GetResult(studentTypeAdapter.Adapt<Contracts.StudentsViewModel>(domainStudent));
            returnModel.SuccessMessage = string.Format(Contracts.Utility.Constants.CreatedSuccessfully,
                      Contracts.Utility.Constants.lblStudent);
            return returnModel;

        }
        public async Task<Result<Contracts.StudentsViewModel>> Update_Using_SP(Contracts.StudentsViewModel model)
        {
            Result<Contracts.StudentsViewModel> returnModel = new Result<Contracts.StudentsViewModel>();
            if (model.Id <= 0)
            {
                returnModel.ErrorCode = (int)HttpStatusCode.NotAcceptable;
                returnModel.ErrorMessage = string.Format(Contracts.Utility.Constants.ItemRequired, Contracts.Utility.Constants.lblStudentId);
                return returnModel;
            }
            returnModel = await Validate(model);
            if (!returnModel.isSuccess)
                return returnModel;

            var result = await _studentCustomRepository.UpdateDomainObjectUsing_SP(model);
            if (result)
            {
                returnModel.SuccessMessage = string.Format(Contracts.Utility.Constants.UpdatedSuccessfully,
                       Contracts.Utility.Constants.lblStudent);
                returnModel.GetResult(model);
            }
            else
            {
                returnModel.ErrorCode = (int)HttpStatusCode.BadRequest;
                returnModel.ErrorMessage = string.Format(Contracts.Utility.Constants.FailedToUpdate,
                        Contracts.Utility.Constants.lblStudent);
            }
            return returnModel;
        }
        public async Task<Result<Contracts.StudentsViewModel>> Update(Contracts.StudentsViewModel model)
        {
            Result<Contracts.StudentsViewModel> returnModel = new Result<Contracts.StudentsViewModel>();
            if (model.Id <= 0)
            {
                returnModel.ErrorCode = (int)HttpStatusCode.NotAcceptable;
                returnModel.ErrorMessage = string.Format(Contracts.Utility.Constants.ItemRequired, Contracts.Utility.Constants.lblStudentId);
                return returnModel;
            }
            returnModel = await Validate(model);
            if (!returnModel.isSuccess)
                return returnModel;
            Students domainStudent = await _studentCustomRepository.GetDomainObject(model.Id);
            domainStudent.Name = model.Name;
            domainStudent.PhoneNumber = model.PhoneNumber;
            domainStudent.Address = model.Address;
            domainStudent.PostalCode = model.PostalCode;
            domainStudent.ModifiedOn = DateTimeOffset.UtcNow;

            Subjects domainSubject;
            foreach (var contractSubject in model.Subjects)
            {
                domainSubject = domainStudent.Subjects.Where(x => x.Id.Equals(contractSubject.Id)).FirstOrDefault();
                if (domainSubject == null)
                    domainSubject = domainStudent.Subjects.Where(x => x.SubjectMasterId.Equals(contractSubject.SubjectMasterId)).FirstOrDefault();
                if (domainSubject == null)
                {
                    returnModel.ErrorCode = (int)HttpStatusCode.NotFound;
                    returnModel.ErrorMessage = string.Format(Contracts.Utility.Constants.InvalidField, Contracts.Utility.Constants.lblSubject);
                    return returnModel;
                }
                domainSubject.Marks = contractSubject.Marks;
                domainSubject.ModifiedOn = DateTimeOffset.UtcNow;
            }

            await _studentDataRepository.Update(domainStudent);
            returnModel.GetResult(new StudentTypeAdapter().Adapt<Contracts.StudentsViewModel>(domainStudent));
            returnModel.SuccessMessage = string.Format(Contracts.Utility.Constants.UpdatedSuccessfully,
                      Contracts.Utility.Constants.lblStudent);
            return returnModel;

        }

        public async Task<Result<IList<Contracts.StudentsViewModel>>> GetAll()
        {
            Result<IList<Contracts.StudentsViewModel>> returnModel = new Result<IList<Contracts.StudentsViewModel>>();
            List<Students> domainStudentList = await _studentCustomRepository.GetDomainObjects();
            returnModel.GetResult(new StudentTypeAdapter().AdaptList<Contracts.StudentsViewModel>(domainStudentList));
            return returnModel;
        }
        public async Task<Result<Contracts.StudentsViewModel>> Get(long Id)
        {
            Result<Contracts.StudentsViewModel> returnModel = new Result<Contracts.StudentsViewModel>();
            if (Id < 0)
            {
                returnModel.isSuccess = false;
                returnModel.ErrorCode = (int)HttpStatusCode.BadRequest;
                returnModel.ErrorMessage = returnModel.ErrorDetail = string.Format(Contracts.Utility.Constants.ItemRequired, Contracts.Utility.Constants.lblStudentId);
                return returnModel;
            }
            Students domainStudent = await _studentCustomRepository.GetDomainObject(Id);
            if (domainStudent == null)
            {
                returnModel.isSuccess = false;
                returnModel.ErrorCode = (int)HttpStatusCode.BadRequest;
                returnModel.ErrorMessage = returnModel.ErrorDetail = string.Format(Contracts.Utility.Constants.RecordNotFound, Contracts.Utility.Constants.lblStudent);
                return returnModel;
            }
            returnModel.GetResult(new StudentTypeAdapter().Adapt<Contracts.StudentsViewModel>(domainStudent));
            return returnModel;
        }

        public async Task<Result<string>> Delete(long Id)
        {
            Result<string> returnModel = new Result<string>();
            Students domainStudent = await _studentDataRepository.FindById(Id);
            if (domainStudent == null)
            {
                returnModel.isSuccess = false;
                returnModel.ErrorCode = (int)HttpStatusCode.BadRequest;
                returnModel.ErrorMessage = returnModel.ErrorDetail = string.Format(Contracts.Utility.Constants.RecordNotFound, Contracts.Utility.Constants.lblStudent);
                return returnModel;
            }
            await _studentDataRepository.Remove(Id);
            returnModel.SuccessMessage = string.Format(Contracts.Utility.Constants.RemovedSuccessfully,
                      Contracts.Utility.Constants.lblStudent);
            returnModel.isSuccess = true;
            return returnModel;
        }
        public async Task<Result<string>> Delete_Using_SP(long Id)
        {
            Result<string> returnModel = new Result<string>();
            Students domainStudent = await _studentDataRepository.FindById(Id);
            if (domainStudent == null)
            {
                returnModel.isSuccess = false;
                returnModel.ErrorCode = (int)HttpStatusCode.BadRequest;
                returnModel.ErrorMessage = returnModel.ErrorDetail = string.Format(Contracts.Utility.Constants.RecordNotFound, Contracts.Utility.Constants.lblStudent);
                return returnModel;
            }
            var result = await _studentCustomRepository.DeleteDomainObjectUsing_SP(Id);
            if (result)
            {
                returnModel.SuccessMessage = string.Format(Contracts.Utility.Constants.RemovedSuccessfully,
                         Contracts.Utility.Constants.lblStudent);
                returnModel.isSuccess = true;
            }
            else
            {
                returnModel.ErrorCode = (int)HttpStatusCode.BadRequest;
                returnModel.ErrorMessage = string.Format(Contracts.Utility.Constants.FailedToRemove,
                        Contracts.Utility.Constants.lblStudent);
            }

            return returnModel;
        }

        public async Task<Result<string>> ReadUpdateFromJson()
        {
            Result<string> returnModel = new Result<string>();
            var filepath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\{"Students.json"}");
            var jsonText = System.IO.File.ReadAllText(filepath);
            var studentsFromJsonFile = JsonConvert.DeserializeObject<Contracts.StudentsJsonReaderModel>(jsonText);
            IList<long> studentIds = studentsFromJsonFile.Students.Select(x => x.Id).ToList();

            IList<Students> domainExistingStudentsList = await _studentCustomRepository.GetDomainObjects(studentIds);
            Students domainStudent;

            IList<Contracts.StudentsViewModel> contractStudentList = new List<Contracts.StudentsViewModel>();
            bool anyNewStudentAdded = false, anyStudentUpdated = false;
            StudentTypeAdapter studentTypeAdapter = new StudentTypeAdapter();
            if (domainExistingStudentsList != null)
            {
                foreach (var contractStudent in studentsFromJsonFile.Students)
                {

                    domainStudent = domainExistingStudentsList.Where(x => x.Id.Equals(contractStudent.Id)).FirstOrDefault();
                    if (domainStudent != null)
                    {
                        domainStudent.Name = contractStudent.Name;
                        domainStudent.PhoneNumber = contractStudent.PhoneNumber;
                        domainStudent.Address = contractStudent.Address;
                        domainStudent.PostalCode = contractStudent.PostalCode;
                        domainStudent.ModifiedOn = DateTimeOffset.UtcNow;

                        Subjects domainSubject;
                        foreach (var contractSubject in contractStudent.Subjects)
                        {
                            domainSubject = domainStudent.Subjects.Where(x => x.Id.Equals(contractSubject.Id)).FirstOrDefault();
                            if (domainSubject == null)
                                domainSubject = domainStudent.Subjects.Where(x => x.SubjectMasterId.Equals(contractSubject.SubjectMasterId)).FirstOrDefault();
                            if (domainSubject == null)
                            {
                                returnModel.ErrorCode = (int)HttpStatusCode.NotFound;
                                returnModel.ErrorMessage = string.Format(Contracts.Utility.Constants.InvalidField, Contracts.Utility.Constants.lblSubject);
                                return returnModel;
                            }
                            domainSubject.Marks = contractSubject.Marks;
                            domainSubject.ModifiedOn = DateTimeOffset.UtcNow;
                        }
                        anyStudentUpdated = true;
                    }
                    else
                    {
                        //NOTE: if Any new student added in json file which Ids not existing in Database then it will create a new student with new Id
                        contractStudentList.Add(contractStudent);
                        anyNewStudentAdded = true;

                    }


                }
            }
            else
            {

                anyNewStudentAdded = true;
                contractStudentList = studentsFromJsonFile.Students;


            }
            if (anyStudentUpdated)
                await _studentDataRepository.BulkUpdate(domainExistingStudentsList);

            if (anyNewStudentAdded)
            {

                IList<Students> domainNewStudentsList = studentTypeAdapter.AdaptList<Students>(contractStudentList);
                await _studentDataRepository.BulkAdd(domainNewStudentsList);
            }
            if (anyStudentUpdated)
                returnModel.SuccessMessage = string.Format(Contracts.Utility.Constants.RecordCreatedUpdatedSuccessfully, Contracts.Utility.Constants.lblStudent + "(s)");
            else
                returnModel.SuccessMessage = string.Format(Contracts.Utility.Constants.RecordCreatedSuccessfully, Contracts.Utility.Constants.lblStudent + "(s)");
            return returnModel;

        }

    }
}
