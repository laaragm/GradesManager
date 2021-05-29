using Base.Infra.Abstractions;
using Base.Infra.Repositories;
using GradesManager.Domain.Entities;
using GradesManager.Infra.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace GradesManager.Infra
{
	public class Schools : DapperRepository<School>, ISchools
	{
		public Schools(IInfraSettings infraSettings) : base(infraSettings, "School")
		{
		}
	}
}
