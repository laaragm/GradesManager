using GradesManager.Domain.DTOs;
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
	public class PerformanceAnalysisController : ControllerBase
	{
		IPerformanceAnalysisService PerformanceAnalysisService { get; }

		public PerformanceAnalysisController(IPerformanceAnalysisService performanceAnalysisService)
		{
			PerformanceAnalysisService = performanceAnalysisService;
		}

		[HttpGet("gradeAverageBySchoolLevels/{schoolID}")]
		public async Task<ActionResult<IDictionary<string, decimal?>>> GradeAverageByLevelsInSchool(long schoolID)
		{
			var result = await PerformanceAnalysisService.CalculateGradeAverageByLevelsInSchool(schoolID);
			if (result != null)
				return Ok(result);
			return StatusCode(StatusCodes.Status500InternalServerError);
		}

		[HttpPost("analyseStudentsPerformance")]
		public async Task<ActionResult<IEnumerable<StudentGradesModel>>> AnalyseStudentsPerformance(StudentsDTO studentsDTO)
		{
			var result = await PerformanceAnalysisService.AnalyseStudentsPerformance(studentsDTO);
			if (result != null)
				return Ok(result);
			return StatusCode(StatusCodes.Status500InternalServerError);
		}

	}
}
