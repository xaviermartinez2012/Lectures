using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;

namespace Cecs475.Scheduling.Web.Controllers {
	public class CourseSectionDto {
		public string CourseName { get; set; }
		public int SectionNumber { get; set; }
		public string SemesterTermName { get; set; }
	}

	public class RegistrationDto {
		public int StudentID { get; set; }
		public CourseSectionDto CourseSection { get; set; }
	}

	[RoutePrefix("api/register")]
	public class RegisterController : ApiController {
		private Model.CatalogContext mContext = new Model.CatalogContext();

		[HttpPost]
		[Route("")]
		public Model.RegistrationResults RegisterForCourse([FromBody]RegistrationDto studentCourse) {
			Model.Student student = mContext.Students.Where(s => s.Id == studentCourse.StudentID).FirstOrDefault();
			// Simulate a slow connection / complicated operation by sleeping.
			Thread.Sleep(3000);

			if (student == null) {
				throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound,
					$"Student id \"{studentCourse.StudentID}\" not found"));
			}

			Model.SemesterTerm term = mContext.SemesterTerms.Where(
				t => t.Name == studentCourse.CourseSection.SemesterTermName)
				.FirstOrDefault();

			if (term == null) {
				throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound,
					$"Semester named \"{studentCourse.CourseSection.SemesterTermName}\" not found"));
			}

			Model.CourseSection section = term.CourseSections.Where(
				c => c.CatalogCourse.ToString() == studentCourse.CourseSection.CourseName
					  && c.SectionNumber == studentCourse.CourseSection.SectionNumber)
				.FirstOrDefault();
			if (section == null) {
				throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound,
					$"Course named \"{studentCourse.CourseSection.CourseName}-" +
					$"{studentCourse.CourseSection.SectionNumber}\" not found"));
			}

			var regResult = student.CanRegisterForCourseSection(section);
			if (regResult == Model.RegistrationResults.Success) {
				section.EnrolledStudents.Add(student);
				mContext.SaveChanges();
			}

			return regResult;
		}
	}
}
