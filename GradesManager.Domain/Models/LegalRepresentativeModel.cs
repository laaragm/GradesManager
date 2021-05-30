using GradesManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GradesManager.Domain.Models
{
	public class LegalRepresentativeModel
	{
		public long ID { get; set; }
		public string Name { get; set; }
		public string PhoneNumber { get; set; }
		public DateTime? Creation { get; }

		public LegalRepresentativeModel()
		{
		}

		public LegalRepresentativeModel(LegalRepresentative legalRepresentative)
		{
			ID = legalRepresentative.ID;
			Name = legalRepresentative.Name;
			PhoneNumber = legalRepresentative.PhoneNumber;
			Creation = legalRepresentative.Creation;
		}

		public LegalRepresentative ToEntity()
		{
			return new LegalRepresentative
			{
				ID = ID,
				Name = Name,
				PhoneNumber = PhoneNumber,
				Creation = Creation,
			};
		}
	}
}
