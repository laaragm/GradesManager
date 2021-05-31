using Base.Domain;
using GradesManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GradesManager.Domain.Models
{
	public class StudentModel
	{
		public long ID { get; set; }
		public string Name { get; set; }
		public LegalRepresentativeModel LegalRepresentative { get; set; }
		public DateTime? Birthday { get; set; }
		public string Address { get; set; }
		public DateTime? Creation { get; }

		public StudentModel(Student student)
		{
			ID = student.ID;
			Name = student.Name;
			LegalRepresentative = GetLegalRepresentativeModel(student);
			Birthday = student.Birthday;
			Address = student.Address;
			Creation = student.Creation;
		}

		public StudentModel()
		{
		}

		public Student ToEntity()
		{
			return new Student
			{
				ID = ID,
				Name = Name,
				LegalRepresentative = LegalRepresentative.ToEntity(),
				Birthday = Birthday,
				Address = Address,
				Creation = Creation
			};
		}

		private LegalRepresentativeModel GetLegalRepresentativeModel(Student student)
			=> student.LegalRepresentative == null ? new LegalRepresentativeModel() : new LegalRepresentativeModel(student.LegalRepresentative);

	}
}
