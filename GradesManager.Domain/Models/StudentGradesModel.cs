using System;
using System.Collections.Generic;
using System.Text;

namespace GradesManager.Domain.Models
{
	public class StudentGradesModel
	{
		public long StudentID { get; set; }
		public string StudentName { get; set; }
		public long ClassroomID { get; set; }
		public string ClassroomName { get; set; }
		public IDictionary<string, decimal> GradeAverageByLevel { get; set; }

	}
}
