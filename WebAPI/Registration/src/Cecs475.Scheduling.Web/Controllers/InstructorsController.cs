using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity; // for Async EF methods
using Cecs475.Scheduling.Model;
using System.Threading.Tasks;

namespace Cecs475.Scheduling.Web.Controllers {
	public class InstructorDto {
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public static InstructorDto From(Model.Instructor instr) {
			return new InstructorDto {
				Id = instr.Id,
				FirstName = instr.FirstName,
				LastName = instr.LastName
			};
		}
	}

	[RoutePrefix("api/instructors")]
	public class InstructorsController : ApiController {
		private Model.CatalogContext mContext = new Model.CatalogContext();

		[HttpGet]
		[Route("")]
		public async Task<IEnumerable<InstructorDto>> GetInstructors() {
			// Normally, calling Select on a DbSet will translate to a blocking (non-async) operation.
			// Instead, we use ToListAsync and await the result to perform a non-blocking call to the database.
			// Once we have the full results, we map them to DTOs and return.
			var instructors = await mContext.Instructors.ToListAsync();
			return instructors.Select(InstructorDto.From);
		}

		[HttpGet]
		[Route("{id:int}")]
		public async Task<InstructorDto> GetInstructor(int id) {
			// SingleOrDefaultAsync is an async version of SingleOrDefault.
			var instructor = await mContext.Instructors.SingleOrDefaultAsync(i => i.Id == id);
			if (instructor != null) {
				return InstructorDto.From(instructor);
			}
			throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound,
				$"Instructor id \"{instructor.Id}\" not found"));
		}

		[HttpPost]
		[Route("")]
		public async Task<InstructorDto> CreateInstructor([FromBody]InstructorDto instructor) {
			// Make sure the name is unique.
			var existing = await mContext.Instructors.SingleOrDefaultAsync(i => i.FirstName == instructor.FirstName
				&& i.LastName == instructor.LastName);

			if (existing != null) {
				throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.Forbidden,
					$"Instructor named \"{instructor.FirstName} {instructor.LastName}\" already exists"));
			}

			mContext.Instructors.Add(new Instructor() {
				FirstName = instructor.FirstName,
				LastName = instructor.LastName
			});

			// Save the changes to the db asynchronously.
			int records = await mContext.SaveChangesAsync();
			if (records == 1) {
				var loaded = await mContext.Instructors.SingleOrDefaultAsync(i => i.FirstName == instructor.FirstName
					&& i.LastName == instructor.LastName);
				return InstructorDto.From(loaded);
			}

			throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError,
				"Could not save that instructor"));
		}
	}
}
