using Base.Domain;
using GradesManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GradesManager.Domain.Models
{
	public class DisciplineModel
	{
		public long ID { get; set; }
		public string Name { get; set; }
		public DateTime? Creation { get; }

		public DisciplineModel(Discipline discipline)
		{
			ID = discipline.ID;
			Name = discipline.Name;
			Creation = discipline.Creation;
		}

		public DisciplineModel()
		{
		}

		public Discipline ToEntity()
		{
			return new Discipline
			{
				ID = ID,
				Name = Name,
				Creation = Creation
			};
		}

	}
}
