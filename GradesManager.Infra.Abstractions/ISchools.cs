using Base.Infra.Abstractions.Repositories;
using GradesManager.Domain.Entities;
using GradesManager.Infra.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GradesManager.Infra.Abstractions
{
	public interface ISchools : IRepository<School>
	{
		void Update(SchoolModel model);

	}
}
