using GradesManager.Services.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace GradesManager.Services.Extensions
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddServices(this IServiceCollection services)
		{
			return services
					.AddScoped<ISchoolService, SchoolService>()
					.AddScoped<IClassroomService, ClassroomService>()
					.AddScoped<ILegalRepresentativeService, LegalRepresentativeService>()
					.AddScoped<IStudentService, StudentService>()
					.AddScoped<IDisciplineService, DisciplineService>()
					.AddScoped<IGradeService, GradeService>();
		}

	}
}
