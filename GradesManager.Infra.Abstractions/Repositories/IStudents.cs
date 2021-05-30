using Base.Infra.Abstractions.Repositories;
using GradesManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GradesManager.Infra.Abstractions.Repositories
{
	public interface IStudents : IRepository<Student>
	{
		Task<Student> Save(Student student);
		Task Update(Student student);

	}
}
