using AutoMapper;
using GradesManager.Domain.Entities;
using GradesManager.Domain.Models;

namespace GradesManager.Services
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<School, SchoolModel>();
			CreateMap<Classroom, ClassroomModel>();
			CreateMap<LegalRepresentative, LegalRepresentativeModel>();
		}
	}
}
