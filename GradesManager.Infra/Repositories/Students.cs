using Base.Infra.Abstractions;
using Base.Infra.Repositories;
using GradesManager.Domain.Entities;
using GradesManager.Infra.Abstractions;
using GradesManager.Infra.Abstractions.Repositories;
using System;

namespace GradesManager.Infra.Repositories
{
	public class Students : DapperRepository<Student>, IStudents
	{
		public Students(IInfraSettings infraSettings) : base(infraSettings, "Student")
		{
		}

	}
}
