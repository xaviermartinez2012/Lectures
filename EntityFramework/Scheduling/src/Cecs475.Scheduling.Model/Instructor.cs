using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cecs475.Scheduling.Model {
	/// <summary>
	/// Represents an instructor who can teach course sections.
	/// </summary>
	public class Instructor {
		public int Id { get; set; }
		[MaxLength(32)]
		[Required]
		public string FirstName { get; set; }
		[MaxLength(32)]
		[Required]
		public string LastName { get; set; }

		public virtual ICollection<CourseSection> CourseSections { get; set; } = new List<CourseSection>();
	}
}
