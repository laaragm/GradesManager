using System;
using System.Collections.Generic;
using System.Text;

namespace GradesManager.Domain.Models
{
	public class XyChartModel
	{
		public IEnumerable<string> Categories { get; set; }
		public IEnumerable<decimal> Values { get; set; }

	}
}
