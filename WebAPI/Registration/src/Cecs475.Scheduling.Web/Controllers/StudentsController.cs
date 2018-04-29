using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Cecs475.Scheduling.Web.Controllers {
	// PROBLEM: EntityFramework objects cannot be serialized into Json. Their object relations don't 
	// play nice with a serializer. So instead we create a Data Transfer Object class, and manually (ugh)
	// map entities to DTO instances.
	public class StudentDto {
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }

		// Serialization requires a default constructor and NO OTHERS, so we can't add a nice constructor
		// taking a Model.Student object. We'll make a static method instead.

		public static StudentDto From(Model.Student s) {
			return new StudentDto() {
				Id = s.Id,
				FirstName = s.FirstName,
				LastName = s.LastName
			};
		}

	}
	/// <summary>
	/// A controller for Student objects from the Entity Framework context.
	/// </summary>
	[RoutePrefix("api/students")]
	public class StudentsController : ApiController {
		// One Context per controller is fine -- they share the same database connection.
		private Model.CatalogContext mContext = new Model.CatalogContext();

		[HttpGet]
		[Route("")]
		public IEnumerable<StudentDto> Get() {
			return mContext.Students.Select(StudentDto.From);
		}

		[HttpGet]
		[Route("{id:int}")]
		public StudentDto Get(int id) {
			return mContext.Students.Where(s => s.Id == id).Select(StudentDto.From)
				.FirstOrDefault();
		}
		
		[HttpGet]
		[Route("{name}")]
		public StudentDto Get(string name) {
			var result = mContext.Students.Where(s => s.FirstName + " " + s.LastName == name).Select(StudentDto.From)
				.FirstOrDefault();
			if (result == null) {
				throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound,
					$"Student named \"{name}\" not found"));
			}
			return result;
		}

		[HttpGet]
		[Route("{id}/transcript")]
		public IEnumerable<string> GetTranscript(int id) {
			var student = mContext.Students.Where(s => s.Id == id).FirstOrDefault();
			if (student != null) {
				return student.Transcript.Select(g => g.CourseSection.CatalogCourse.ToString());
			}
			return null;
		}

		[HttpPost]
		[Route("")]
		public void Post([FromBody]StudentDto value) {
			mContext.Students.Add(new Model.Student() {
				FirstName = value.FirstName,
				LastName = value.LastName
			});
			mContext.SaveChanges();
		}

		[HttpPut]
		[Route("{id}")]
		public void Put(int id, [FromBody]StudentDto value) {
			var student = mContext.Students.Where(s => s.Id == id).FirstOrDefault();
			if (student != null) {
				student.FirstName = value.FirstName;
				student.LastName = value.LastName;
				mContext.SaveChanges();
			}
		}

		[HttpDelete]
		[Route("{id}")]
		public void Delete(int id) {
			var student = mContext.Students.Where(s => s.Id == id).FirstOrDefault();
			if (student != null) {
				mContext.Students.Remove(student);
				mContext.SaveChanges();
			}
		}
	}
}
