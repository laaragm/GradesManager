using GradesManager.Domain.Entities;
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
	public class GradesController : ControllerBase
	{
		IGradeService GradesService { get; }

		public GradesController(IGradeService gradesService)
		{
			GradesService = gradesService;
		}

		[HttpPost]
		public async Task<ActionResult<GradeModel>> Save(GradeModel model)
		{
			var result = await GradesService.Save(model);
			if (result != null)
				return Ok(result);
			return StatusCode(StatusCodes.Status500InternalServerError);
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<GradeModel>>> All()
		{
			var result = await GradesService.FetchAll();
			if (result != null)
				return Ok(result);
			return StatusCode(StatusCodes.Status500InternalServerError);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<GradeModel>> ById(long id)
		{
			var result = await GradesService.FetchById(id);
			if (result != null)
				return Ok(result);
			return StatusCode(StatusCodes.Status500InternalServerError);
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult> Delete(long id)
		{
			await GradesService.Delete(id);
			return Ok();
		}

		[HttpPut]
		public async Task<ActionResult> Update(GradeModel model)
		{
			await GradesService.Update(model);
			return Ok();
		}
	}
}
