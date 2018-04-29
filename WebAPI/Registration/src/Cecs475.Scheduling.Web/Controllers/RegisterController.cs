using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;

namespace Cecs475.Scheduling.Web.Controllers {
	public class CourseSectionDto {
		public int Id { get; set; }
		public int SemesterTermId { get; set; }
		public CatalogCourseDto CatalogCourse { get; set; }
		public int SectionNumber { get; set; }

		public static CourseSectionDto From(Model.CourseSection section) {
			return new CourseSectionDto {
				Id = section.Id,
				SemesterTermId = section.Semester.Id,
				SectionNumber = section.SectionNumber,
				CatalogCourse = CatalogCourseDto.From(section.CatalogCourse)
			};
		}
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
				t => t.Id == studentCourse.CourseSection.SemesterTermId)
				.SingleOrDefault();

			if (term == null) {
				throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound,
					$"Semester id \"{studentCourse.CourseSection.SemesterTermId}\" not found"));
			}

			Model.CourseSection section = term.CourseSections.SingleOrDefault(
				c => c.CatalogCourse.DepartmentName == studentCourse.CourseSection.CatalogCourse.DepartmentName
					  && c.CatalogCourse.CourseNumber == studentCourse.CourseSection.CatalogCourse.CourseNumber
					  && c.SectionNumber == studentCourse.CourseSection.SectionNumber);
			if (section == null) {
				throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound,
					$"Course named \"{studentCourse.CourseSection.CatalogCourse.DepartmentName}" +
					$"{studentCourse.CourseSection.CatalogCourse.CourseNumber}\"-" +
					$"{studentCourse.CourseSection.SectionNumber} not found"));
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
