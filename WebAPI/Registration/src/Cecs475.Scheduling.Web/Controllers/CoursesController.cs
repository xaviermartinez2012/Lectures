using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Cecs475.Scheduling.Web.Controllers {
	public class CatalogCourseDto {
		public int Id { get; set; }
		public string DepartmentName { get; set; }
		public string CourseNumber { get; set; }
		public IEnumerable<int> PrerequisiteCourseIds { get; set; }

		public static CatalogCourseDto From(Model.CatalogCourse course) {
			return new CatalogCourseDto {
				Id = course.Id,
				DepartmentName = course.DepartmentName,
				CourseNumber = course.CourseNumber,
				PrerequisiteCourseIds = course.Prerequisites.Select(c => c.Id)
			};
		}
	}

	[RoutePrefix("api/courses")]
	public class CoursesController : ApiController {
		private Model.CatalogContext mContext = new Model.CatalogContext();

		[HttpGet]
		[Route("")]
		public IEnumerable<CatalogCourseDto> GetCourses() {
			return mContext.Courses.Select(CatalogCourseDto.From);
		}

		[HttpGet]
		[Route("{id:int}")]
		public CatalogCourseDto GetCourse(int id) {
			var course = mContext.Courses.Where(c => c.Id == id).SingleOrDefault();
			if (course == null) {
				throw new HttpResponseException(Request.CreateErrorResponse(
				HttpStatusCode.NotFound, $"No course with id {id} found"));
			}
			return CatalogCourseDto.From(course);
		}

		[HttpGet]
		[Route("{name}")]
		public CatalogCourseDto GetCourse(string name) {
			var course = mContext.Courses.Where(c => c.DepartmentName + " " + c.CourseNumber == name)
				.SingleOrDefault();
			if (course == null) {
				throw new HttpResponseException(Request.CreateErrorResponse(
				HttpStatusCode.NotFound, $"No course with name {name} found"));
			}
			return CatalogCourseDto.From(course);
		}
	}
}