using AutoMapper;
using GradesManager.Domain.Entities;
using GradesManager.Domain.Models;
using GradesManager.Infra.Abstractions;
using GradesManager.Infra.Abstractions.Repositories;
using GradesManager.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradesManager.Services
{
	public class SchoolService : ISchoolService
	{
		ISchools Schools { get; }
		IMapper Mapper { get; }

		public SchoolService(ISchools schools, IMapper mapper)
		{
			Schools = schools;
			Mapper = mapper;
		}

		public async Task<SchoolModel> Save(SchoolModel model)
		{
			var result = await Schools.Save(model.ToEntity());
			return Mapper.Map<School, SchoolModel>(result);
		}

		public async Task<IEnumerable<SchoolModel>> FetchAll()
		{
			var result = await Schools.AllAsync();
			return result.Select(Mapper.Map<SchoolModel>).ToList();
		}

		public async Task<SchoolModel> FetchById(long id)
		{
			var result = await Schools.FetchByIDAsync(id);
			return Mapper.Map<School, SchoolModel>(result);
		}

		public async Task Delete(long id) => await Schools.DeleteAsync(id);

		public async Task Update(SchoolModel model) => await Schools.Update(model.ToEntity());

	}
}
