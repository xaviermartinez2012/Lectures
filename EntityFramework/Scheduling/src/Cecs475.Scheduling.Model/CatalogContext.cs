using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cecs475.Scheduling.Model {
	public class CatalogContext : DbContext {
		public DbSet<SemesterTerm> SemesterTerms { get; set; }
		public DbSet<Instructor> Instructors { get; set; }
		public DbSet<CatalogCourse> Courses { get; set; }

		// The parameter to the base constructor is the name of the ConnectionString in app.config
		public CatalogContext() : base("name=Catalog") { }

		protected override void OnModelCreating(DbModelBuilder modelBuilder) {
			base.OnModelCreating(modelBuilder);
			
			// This "Fluid" statement specifies that every CatalogCourse has many Prerequisites,
			// each of which may have many other Prerequisites. The table used to model the relation
			// will be called CatalogCourse_Prerequisite.
			modelBuilder.Entity<CatalogCourse>()
				.HasMany(t => t.Prerequisites)
				.WithMany()
				.Map(m =>  m.ToTable("CatalogCourse_Prerequisite")
								.MapLeftKey("CatalogCourse_Id")
								.MapRightKey("PrerequisiteCourse_Id"));
		}
	}
}
