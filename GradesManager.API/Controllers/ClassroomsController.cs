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
	public class ClassroomsController : ControllerBase
	{
		IClassroomService ClassroomService { get; }

		public ClassroomsController(IClassroomService classroomService)
		{
			ClassroomService = classroomService;
		}

		[HttpPost]
		public async Task<ActionResult<SchoolModel>> Save(ClassroomModel model)
		{
			var result = await ClassroomService.Save(model);
			if (result != null)
				return Ok(result);
			return StatusCode(StatusCodes.Status500InternalServerError);
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<ClassroomModel>>> All()
		{
			var result = await ClassroomService.FetchAll();
			if (result != null)
				return Ok(result);
			return StatusCode(StatusCodes.Status500InternalServerError);
		}

		[HttpGet("bySchool/{id}")]
		public async Task<ActionResult<IEnumerable<ClassroomModel>>> BySchool(long id)
		{
			var result = await ClassroomService.FetchBySchoolId(id);
			if (result != null)
				return Ok(result);
			return StatusCode(StatusCodes.Status500InternalServerError);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<ClassroomModel>> ById(long id)
		{
			var result = await ClassroomService.FetchById(id);
			if (result != null)
				return Ok(result);
			return StatusCode(StatusCodes.Status500InternalServerError);
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult> Delete(long id)
		{
			await ClassroomService.Delete(id);
			return Ok();
		}

		[HttpPut]
		public async Task<ActionResult> Update(ClassroomModel model)
		{
			await ClassroomService.Update(model);
			return Ok();
		}
	}
}
