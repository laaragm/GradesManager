using Base.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Base.Infra.Abstractions.Repositories
{
	public interface IRepository<T> where T : Entity
	{
		IEnumerable<T> All();
		void Delete(long id);
		T FetchByID(long id);
	}
}
