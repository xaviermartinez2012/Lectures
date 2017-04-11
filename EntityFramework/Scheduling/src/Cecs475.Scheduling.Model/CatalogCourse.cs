using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cecs475.Scheduling.Model {
	/// <summary>
	/// Represents one course offered at CSULB.
	/// </summary>
	public class CatalogCourse {
		public int Id { get; set; }
		public string DepartmentName { get; set; }
		public string CourseNumber { get; set; }

		// We'll talk about the virtual part later.
		public virtual ICollection<CatalogCourse> Prerequisites { get; set; } = new List<CatalogCourse>();

		public override string ToString() {
			return DepartmentName + " " + CourseNumber;
		}
	}
}
