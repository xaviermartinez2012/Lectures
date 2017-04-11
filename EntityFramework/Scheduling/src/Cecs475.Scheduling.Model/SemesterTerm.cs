using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cecs475.Scheduling.Model {
	/// <summary>
	/// Represents one semester of an academic year.
	/// </summary>
	public class SemesterTerm {
		// [Key] denotes a primary key. EntityFramework will infer "Id" or "[typename]Id" fields
		// to be keys, but you can also manually specify it.
		[Key]
		public int Id { get; set; }

		[MaxLength(50)]
		[Required]
		public string Name { get; set; }
		[Required]
		public DateTime StartDate { get; set; }
		[Required]
		public DateTime EndDate { get; set; }

		[Required]
		public virtual ICollection<CourseSection> CourseSections { get; set; } = new List<CourseSection>();
	}
}
