using GradesManager.Infra.Abstractions;
using GradesManager.Services.Abstractions;
using System;

namespace GradesManager.Services
{
	public class SchoolService : ISchoolService
	{
		ISchools Schools { get; }

		public SchoolService(ISchools schools)
		{
			Schools = schools;
		}

		public void Create()
		{

		}
	}
}
