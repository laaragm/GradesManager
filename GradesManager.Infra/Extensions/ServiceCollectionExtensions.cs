using GradesManager.Infra.Abstractions.Repositories;
using GradesManager.Infra.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace GradesManager.Infra.Extensions
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddRepositories(this IServiceCollection services)
		{
			return services
					.AddScoped<ISchools, Schools>()
					.AddScoped<IClassroomDisciplines, ClassroomDisciplines>()
					.AddScoped<IDisciplines, Disciplines>()
					.AddScoped<IClassrooms, Classrooms>()
					.AddScoped<IGrades, Grades>()
					.AddScoped<ILegalRepresentatives, LegalRepresentatives>()
					.AddScoped<IStudents, Students>();
		}

	}
}
