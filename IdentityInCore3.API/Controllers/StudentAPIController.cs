using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityInCore3.Services;
using IdentityInCore3.Services.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdentityInCore3.API.Controllers
{
    [Route("api/[controller]")]
     
    public class StudentAPIController : Controller
    {
        readonly IStudentService _studentService;
        public StudentAPIController(IStudentService studentService)
        {
            this._studentService = studentService;
        }
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] Contracts.StudentsViewModel model)
        {
            Result<Contracts.StudentsViewModel> returnModel = new Result<Contracts.StudentsViewModel>();
            returnModel = await _studentService.Create(model);
            return Ok(returnModel);
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update([FromBody] Contracts.StudentsViewModel model)
        {
            Result<Contracts.StudentsViewModel> returnModel = new Result<Contracts.StudentsViewModel>();
            returnModel = await _studentService.Update(model);
            return Ok(returnModel);
        }
        [HttpPut]
        [Route("Update_Using_SP")]
        public async Task<IActionResult> Update_Using_SP([FromBody] Contracts.StudentsViewModel model)
        {
            Result<Contracts.StudentsViewModel> returnModel = new Result<Contracts.StudentsViewModel>();
            returnModel = await _studentService.Update_Using_SP(model);
            return Ok(returnModel);
        }
        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            Result<IList<Contracts.StudentsViewModel>> returnModel = new Result<IList<Contracts.StudentsViewModel>>();
            returnModel = await _studentService.GetAll();
            return Ok(returnModel);
        }
        [HttpGet]
        [Route("Get/{Id}")]
        public async Task<IActionResult> Get(long Id)
        {
            Result<Contracts.StudentsViewModel> returnModel = new Result<Contracts.StudentsViewModel>();
            returnModel = await _studentService.Get(Id);
            return Ok(returnModel);
        }
        [HttpDelete]
        [Route("Delete/{Id}")]
        public async Task<IActionResult> Delete(long Id)
        {
            Result<string> returnModel = new Result<string>();
            returnModel = await _studentService.Delete(Id);
            return Ok(returnModel);
        }
        [HttpDelete]
        [Route("Delete_Using_SP/{Id}")]
        public async Task<IActionResult> Delete_Using_SP(long Id)
        {
            Result<string> returnModel = new Result<string>();
            returnModel = await _studentService.Delete_Using_SP(Id);
            return Ok(returnModel);
        }

        [HttpGet]
        [Route("ReadUpdateFromJson")]
        public async Task<IActionResult> ReadUpdateFromJson()
        {
            Result<string> returnModel = new Result<string>();
            returnModel = await _studentService.ReadUpdateFromJson();
            return Ok(returnModel);
        }
    }
}
