using Base.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Base.Infra.Abstractions.Repositories
{
	public interface IDapperRepository<T> : IRepository<T> where T : Entity
	{

	}
}
