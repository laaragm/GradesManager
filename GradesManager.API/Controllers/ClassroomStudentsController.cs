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
	public class ClassroomStudentsController : ControllerBase
	{
		IClassroomStudentService ClassroomStudentService { get; }

		public ClassroomStudentsController(IClassroomStudentService classroomStudentService)
		{
			ClassroomStudentService = classroomStudentService;
		}

		[HttpPost]
		public async Task<ActionResult<ClassroomStudentModel>> Save(ClassroomStudentModel model)
		{
			var result = await ClassroomStudentService.Save(model);
			if (result != null)
				return Ok(result);
			return StatusCode(StatusCodes.Status500InternalServerError);
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<ClassroomStudentModel>>> All()
		{
			var result = await ClassroomStudentService.FetchAll();
			if (result != null)
				return Ok(result);
			return StatusCode(StatusCodes.Status500InternalServerError);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<ClassroomStudentModel>> ById(long id)
		{
			var result = await ClassroomStudentService.FetchById(id);
			if (result != null)
				return Ok(result);
			return StatusCode(StatusCodes.Status500InternalServerError);
		}

		[HttpGet("studentCountPerLevel/{schoolID}")]
		public async Task<ActionResult<XyChartModel>> StudentCountPerLevel(long schoolID)
		{
			var result = await ClassroomStudentService.StudentCountPerLevel(schoolID);
			if (result != null)
				return Ok(result);
			return StatusCode(StatusCodes.Status500InternalServerError);
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult> Delete(long id)
		{
			await ClassroomStudentService.Delete(id);
			return Ok();
		}

		[HttpPut]
		public async Task<ActionResult> Update(ClassroomStudentModel model)
		{
			await ClassroomStudentService.Update(model);
			return Ok();
		}
	}
}
