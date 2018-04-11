using Cecs475.Scheduling.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test {
	class Program {
		static void Main(string[] args) {

			CatalogContext con = new CatalogContext();

			int choice = -1;
			do {
				Console.WriteLine("Menu:\n0. Quit\n1. Populate database\n2. Show courses\n3. Show course sections\n"
					+ "4. Print transcript\n6. Reflection and type demos");
				choice = Convert.ToInt32(Console.ReadLine());

				switch (choice) {
					case 1:
						// Add some courses to the catalog
						var cecs174 = new CatalogCourse() {
							DepartmentName = "CECS",
							CourseNumber = "174",
						};
						con.Courses.Add(cecs174);

						var cecs274 = new CatalogCourse() {
							DepartmentName = "CECS",
							CourseNumber = "274",
						};
						cecs274.Prerequisites.Add(cecs174);
						con.Courses.Add(cecs274);

						var cecs228 = new CatalogCourse() {
							DepartmentName = "CECS",
							CourseNumber = "228",
						};
						cecs228.Prerequisites.Add(cecs174);
						con.Courses.Add(cecs228);

						var cecs277 = new CatalogCourse() {
							DepartmentName = "CECS",
							CourseNumber = "277",
						};
						cecs277.Prerequisites.Add(cecs274);
						cecs277.Prerequisites.Add(cecs228);
						con.Courses.Add(cecs277);

						// Add a semester term
						var spring2017 = new SemesterTerm() {
							Name = "Spring 2017",
							StartDate = new DateTime(2017, 1, 23),
							EndDate = new DateTime(2017, 5, 26)
						};
						con.SemesterTerms.Add(spring2017);
						var fall2017 = new SemesterTerm() {
							Name = "Fall 2017",
							StartDate = new DateTime(2017, 8, 21),
							EndDate = new DateTime(2017, 12, 22)
						};
						con.SemesterTerms.Add(fall2017);

						// Add instructors
						var neal = new Instructor() {
							FirstName = "Neal",
							LastName = "Terrell",
						};
						con.Instructors.Add(neal);
						var anthony = new Instructor() {
							FirstName = "Anthony",
							LastName = "Giacalone"
						};
						con.Instructors.Add(anthony);

						// Add sections
						var cecs174_99 = new CourseSection() {
							CatalogCourse = cecs174,
							SectionNumber = 1,
							Instructor = neal,
							MeetingDays = DaysOfWeek.Monday | DaysOfWeek.Wednesday,
							StartTime = new DateTime(2017, 1, 1, 8, 0, 0), // 9 am
							EndTime = new DateTime(2017, 1, 1, 8, 50, 0),
						};
						spring2017.CourseSections.Add(cecs174_99);

						var cecs228_99 = new CourseSection() {
							CatalogCourse = cecs228,
							SectionNumber = 99,
							Instructor = anthony,
							MeetingDays = DaysOfWeek.Friday,
							StartTime = new DateTime(2017, 1, 1, 10, 0, 0), // 9 am
							EndTime = new DateTime(2017, 1, 1, 11, 50, 0),
						};
						spring2017.CourseSections.Add(cecs228_99);

						var cecs228_01 = new CourseSection() {
							CatalogCourse = cecs228,
							SectionNumber = 1,
							Instructor = neal,
							MeetingDays = DaysOfWeek.Tuesday | DaysOfWeek.Thursday,
							StartTime = new DateTime(2017, 1, 1, 9, 0, 0), // 9 am
							EndTime = new DateTime(2017, 1, 1, 9, 50, 0),
						};
						fall2017.CourseSections.Add(cecs228_01);

						//var cecs228_03 = new CourseSection() {
						//	CatalogCourse = cecs228,
						//	SectionNumber = 1,
						//	Instructor = neal,
						//	MeetingDays = DaysOfWeek.Monday | DaysOfWeek.Wednesday,
						//	StartTime = new DateTime(2017, 1, 1, 12, 30, 0), // 9 am
						//	EndTime = new DateTime(2017, 1, 1, 13, 20, 0),
						//};
						//fall2017.CourseSections.Add(cecs228_03);

						var cecs274_05 = new CourseSection() {
							CatalogCourse = cecs274,
							SectionNumber = 5,
							Instructor = anthony,
							MeetingDays = DaysOfWeek.Tuesday | DaysOfWeek.Thursday,
							StartTime = new DateTime(2017, 1, 1, 9, 30, 0), // 9 am
							EndTime = new DateTime(2017, 1, 1, 10, 20, 0),
						};
						fall2017.CourseSections.Add(cecs274_05);

						var cecs274_11 = new CourseSection() {
							CatalogCourse = cecs274,
							SectionNumber = 11,
							Instructor = anthony,
							MeetingDays = DaysOfWeek.Monday| DaysOfWeek.Wednesday | DaysOfWeek.Friday,
							StartTime = new DateTime(2017, 1, 1, 13, 0, 0), // 1 pm
							EndTime = new DateTime(2017, 1, 1, 13, 50, 0),
						};
						fall2017.CourseSections.Add(cecs274_11);

						Student s1 = new Student() {
							FirstName = "A",
							LastName = "B",
						};
						s1.Transcript.Add(new CourseGrade() {
							CourseSection = cecs174_99,
							GradeEarned = GradeTypes.A
						});
						s1.Transcript.Add(new CourseGrade() {
							CourseSection = cecs228_99,
							GradeEarned = GradeTypes.D
						});
						con.Students.Add(s1);

						Student s2 = new Student() {
							FirstName = "C",
							LastName = "D",
						};
						con.Students.Add(s2);

						con.SaveChanges();
						break;
					case 2:
						// Print all courses in the catalog
						foreach (var course in con.Courses.OrderBy(c => c.CourseNumber)) {
							Console.Write($"{course.DepartmentName} {course.CourseNumber}");
							if (course.Prerequisites.Count > 0) {
								Console.Write(" (Prerequisites: ");
								Console.Write(string.Join(", ", course.Prerequisites));
								Console.Write(")");
							}
							Console.WriteLine();
						}

						break;

					case 3:
						// Print all offered sections for Fall 2017
						var fallSem = con.SemesterTerms.Where(s => s.Name == "Fall 2017").FirstOrDefault();
						if (fallSem == null) {
							break;
						}

						Console.WriteLine($"{fallSem.Name}: {fallSem.StartDate.ToString("MMM dd")} - {fallSem.EndDate.ToString("MMM dd")}");
						foreach (var section in fallSem.CourseSections) {
							Console.WriteLine($"{section.CatalogCourse}-{section.SectionNumber.ToString("D2")} -- " +
								$"{section.Instructor.FirstName[0]} {section.Instructor.LastName} -- " +
								$"{section.MeetingDays}, {section.StartTime.ToShortTimeString()} to {section.EndTime.ToShortTimeString()}");
						}
						break;

					case 4:
						Console.WriteLine("Enter a name:");
						string name = Console.ReadLine();
						string[] split = name.Split(' ');
						string f = split[0], l = split[1];
						Student tStudent = con.Students.Where(s => s.FirstName == f && s.LastName == l)
							.FirstOrDefault();
						if (tStudent == null) {
							Console.WriteLine("Not found");
							break;
						}
						Console.WriteLine($"{tStudent.FirstName} {tStudent.LastName}");
						Console.WriteLine("Transcript: {0}",
							string.Join(", ", tStudent.Transcript));

						break;

					case 5:
						Console.WriteLine("Enter a name:");
						string ename = Console.ReadLine();
						string[] esplit = ename.Split(' ');
						string ef = esplit[0], el = esplit[1];
						Student eStudent = con.Students.Where(s => s.FirstName == ef && s.LastName == el)
							.FirstOrDefault();
						if (eStudent == null) {
							Console.WriteLine("Not found");
							break;
						}

						Console.WriteLine("Enter a course section for Fall 2017:");
						string cName = Console.ReadLine();
						string[] csplit = cName.Split(new char[] { ' ', '-' });
						string dept = csplit[0];
						string num = csplit[1];
						int sec = Convert.ToInt32(csplit[2]);

						SemesterTerm f2017 = con.SemesterTerms.Where(s => s.Name == "Fall 2017").First();
						CourseSection sec2 = f2017.CourseSections.Where(
							c => c.CatalogCourse.DepartmentName == dept &&
							c.CatalogCourse.CourseNumber == num &&
							c.SectionNumber == sec).FirstOrDefault();

						if (sec2 == null) {
							Console.WriteLine("not found");
							break;
						}

						var register = eStudent.CanRegisterForCourseSection(sec2);
						Console.WriteLine(register);
						break;

					case 6:

						break;
				}
				Console.WriteLine();
				Console.WriteLine();

			} while (choice != 0);
		}



	}
}