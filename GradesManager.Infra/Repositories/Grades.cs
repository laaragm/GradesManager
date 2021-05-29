using Base.Infra.Abstractions;
using Base.Infra.Repositories;
using GradesManager.Domain.Entities;
using GradesManager.Infra.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace GradesManager.Infra.Repositories
{
	public class Grades : DapperRepository<Grade>, IGrades
	{
		public Grades(IInfraSettings infraSettings) : base(infraSettings, "Grade")
		{
		}
	}
}
