using GradesManager.Domain.Models;
using GradesManager.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradesManager.API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class StudentsController : ControllerBase
	{
		IStudentService StudentService { get; }

		public StudentsController(IStudentService studentService)
		{
			StudentService = studentService;
		}

		[HttpPost]
		public async Task<ActionResult<StudentModel>> Save(StudentModel model)
		{
			var result = await StudentService.Save(model);
			if (result != null)
				return Ok(result);
			return StatusCode(StatusCodes.Status500InternalServerError);
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<StudentModel>>> All()
		{
			var result = await StudentService.FetchAll();
			if (result != null)
				return Ok(result);
			return StatusCode(StatusCodes.Status500InternalServerError);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<StudentModel>> ById(long id)
		{
			var result = await StudentService.FetchById(id);
			if (result != null)
				return Ok(result);
			return StatusCode(StatusCodes.Status500InternalServerError);
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult> Delete(long id)
		{
			await StudentService.Delete(id);
			return Ok();
		}

		[HttpPut]
		public async Task<ActionResult> Update(StudentModel model)
		{
			await StudentService.Update(model);
			return Ok();
		}
	}
}
