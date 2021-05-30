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
	public class SchoolsController : ControllerBase
	{
		ISchoolService SchoolService { get; }

		public SchoolsController(ISchoolService schoolService)
		{
			SchoolService = schoolService;
		}

		[HttpPost]
		public async Task<ActionResult<SchoolModel>> Save(SchoolModel model)
		{
			var result = await SchoolService.Save(model);
			if (result != null)
				return Ok(result);
			return StatusCode(StatusCodes.Status500InternalServerError);
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<SchoolModel>>> All()
		{
			var result = await SchoolService.FetchAll();
			if (result != null)
				return Ok(result);
			return StatusCode(StatusCodes.Status500InternalServerError);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<SchoolModel>> ById(long id)
		{
			var result = await SchoolService.FetchById(id);
			if (result != null)
				return Ok(result);
			return StatusCode(StatusCodes.Status500InternalServerError);
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult> Delete(long id)
		{
			await SchoolService.Delete(id);
			return Ok();
		}

		[HttpPut]
		public async Task<ActionResult> Update(SchoolModel model)
		{
			await SchoolService.Update(model);
			return Ok();
		}
	}
}
