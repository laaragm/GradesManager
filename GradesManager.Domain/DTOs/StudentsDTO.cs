using System;
using System.Collections.Generic;
using System.Text;

namespace GradesManager.Domain.DTOs
{
	public class StudentsDTO
	{
		public IEnumerable<long> Students { get; set; }
		public long School { get; set; }

	}
}
