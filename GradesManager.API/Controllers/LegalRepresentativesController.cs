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
	public class LegalRepresentativesController : ControllerBase
	{
		ILegalRepresentativeService LegalRepresentativeService { get; }

		public LegalRepresentativesController(ILegalRepresentativeService legalRepresentativeService)
		{
			LegalRepresentativeService = legalRepresentativeService;
		}

		[HttpPost]
		public async Task<ActionResult<LegalRepresentativeModel>> Save(LegalRepresentativeModel model)
		{
			var result = await LegalRepresentativeService.Save(model);
			if (result != null)
				return Ok(result);
			return StatusCode(StatusCodes.Status500InternalServerError);
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<LegalRepresentativeModel>>> All()
		{
			var result = await LegalRepresentativeService.FetchAll();
			if (result != null)
				return Ok(result);
			return StatusCode(StatusCodes.Status500InternalServerError);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<LegalRepresentativeModel>> ById(long id)
		{
			var result = await LegalRepresentativeService.FetchById(id);
			if (result != null)
				return Ok(result);
			return StatusCode(StatusCodes.Status500InternalServerError);
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult> Delete(long id)
		{
			await LegalRepresentativeService.Delete(id);
			return Ok();
		}

		[HttpPut]
		public async Task<ActionResult> Update(LegalRepresentativeModel model)
		{
			await LegalRepresentativeService.Update(model);
			return Ok();
		}
	}
}
