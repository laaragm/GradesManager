using Base.Infra.Abstractions.Repositories;
using GradesManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GradesManager.Infra.Abstractions
{
	public interface IGrades : IRepository<Grade>
	{

	}
}
