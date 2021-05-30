using GradesManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GradesManager.Services.Abstractions
{
	public interface IStudentService
	{
		Task<StudentModel> Save(StudentModel model);
		Task<IEnumerable<StudentModel>> FetchAll();
		Task<StudentModel> FetchById(long id);
		Task Delete(long id);
		Task Update(StudentModel model);

	}
}
