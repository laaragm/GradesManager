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
	public class ClassroomDisciplinesController : ControllerBase
	{
		IClassroomDisciplineService ClassroomDisciplineService { get; }

		public ClassroomDisciplinesController(IClassroomDisciplineService classroomDisciplineService)
		{
			ClassroomDisciplineService = classroomDisciplineService;
		}

		[HttpPost]
		public async Task<ActionResult<ClassroomDisciplineModel>> Save(ClassroomDisciplineModel model)
		{
			var result = await ClassroomDisciplineService.Save(model);
			if (result != null)
				return Ok(result);
			return StatusCode(StatusCodes.Status500InternalServerError);
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<ClassroomDisciplineModel>>> All()
		{
			var result = await ClassroomDisciplineService.FetchAll();
			if (result != null)
				return Ok(result);
			return StatusCode(StatusCodes.Status500InternalServerError);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<ClassroomDisciplineModel>> ById(long id)
		{
			var result = await ClassroomDisciplineService.FetchById(id);
			if (result != null)
				return Ok(result);
			return StatusCode(StatusCodes.Status500InternalServerError);
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult> Delete(long id)
		{
			await ClassroomDisciplineService.Delete(id);
			return Ok();
		}

		[HttpPut]
		public async Task<ActionResult> Update(ClassroomDisciplineModel model)
		{
			await ClassroomDisciplineService.Update(model);
			return Ok();
		}
	}
}
