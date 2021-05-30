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
	public class DisciplinesController : ControllerBase
	{
		IDisciplineService DisciplineService { get; }

		public DisciplinesController(IDisciplineService disciplineService)
		{
			DisciplineService = disciplineService;
		}

		[HttpPost]
		public async Task<ActionResult<DisciplineModel>> Save(DisciplineModel model)
		{
			var result = await DisciplineService.Save(model);
			if (result != null)
				return Ok(result);
			return StatusCode(StatusCodes.Status500InternalServerError);
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<DisciplineModel>>> All()
		{
			var result = await DisciplineService.FetchAll();
			if (result != null)
				return Ok(result);
			return StatusCode(StatusCodes.Status500InternalServerError);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<DisciplineModel>> ById(long id)
		{
			var result = await DisciplineService.FetchById(id);
			if (result != null)
				return Ok(result);
			return StatusCode(StatusCodes.Status500InternalServerError);
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult> Delete(long id)
		{
			await DisciplineService.Delete(id);
			return Ok();
		}

		[HttpPut]
		public async Task<ActionResult> Update(DisciplineModel model)
		{
			await DisciplineService.Update(model);
			return Ok();
		}
	}
}
