using Base.Infra.Abstractions.Repositories;
using GradesManager.Domain.Entities;
using System;

namespace GradesManager.Infra.Abstractions
{
	public interface IStudents : IRepository<Student>
	{

	}
}
