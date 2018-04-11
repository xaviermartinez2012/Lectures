using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cecs475.Scheduling.Model {
	public enum GradeTypes {
		A, B, C, D, F
	}
	public class CourseGrade {
		public int Id { get; set; }
		public virtual Student StudentOfRecord { get; set; }
		public virtual CourseSection CourseSection { get; set; }
		public virtual GradeTypes GradeEarned { get; set; }

		public override string ToString() {
			return $"{CourseSection} ({GradeEarned})";
		}
	}
}
