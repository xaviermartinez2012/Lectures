using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Cecs475.Scheduling.RegistrationApp {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window {
		public MainWindow() {
			InitializeComponent();
		}

		public RegistrationViewModel ViewModel => 
			FindResource("ViewModel") as RegistrationViewModel;

		private void mValidateBtn_Click(object sender, RoutedEventArgs e) {
			var client = new RestClient(ViewModel.ApiUrl);
			var request = new RestRequest("api/students/{name}", Method.GET);
			request.AddUrlSegment("name", ViewModel.FullName);

			var response = client.Execute(request);
			if (response.StatusCode == System.Net.HttpStatusCode.NotFound) {
				MessageBox.Show("Student not found");
			}
			else {
				MessageBox.Show("Success!");
			}
		}

		private void mRegisterBtn_Click(object sender, RoutedEventArgs e) {
			string[] courseSplit = mCourseText.Text.Split('-');
			int sectionNum = Convert.ToInt32(courseSplit[1]);
			string[] nameSplit = courseSplit[0].Split(' ');

			var client = new RestClient(ViewModel.ApiUrl);
			var request = new RestRequest("api/students/{name}", Method.GET);
			request.AddUrlSegment("name", ViewModel.FullName);
			var response = client.Execute(request);
			if (response.StatusCode == System.Net.HttpStatusCode.NotFound) {
				MessageBox.Show("Student not found");
			}
			else {
				request = new RestRequest("api/register", Method.POST);

				JObject obj = JObject.Parse(response.Content);
				request.AddJsonBody(new {
					StudentId = (int)obj["Id"],
					CourseSection = new {
						SemesterTermId = 2, // hard-code Fall 2017
						CatalogCourse = new {
							DepartmentName = nameSplit[0],
							CourseNumber = nameSplit[1]
						},
						SectionNumber = sectionNum,
					}
				});

				response = client.Execute(request);
				if (response.StatusCode == System.Net.HttpStatusCode.NotFound) {
					MessageBox.Show("Course not found");
				}
				else {
					int result = Convert.ToInt32(response.Content);
					MessageBox.Show(result.ToString());
				}
			}
		}

		private async void mAsyncBtn_Click(object sender, RoutedEventArgs e) {
			string[] courseSplit = mCourseText.Text.Split('-');
			int sectionNum = Convert.ToInt32(courseSplit[1]);
			string[] nameSplit = courseSplit[0].Split(' ');


			var client = new RestClient(ViewModel.ApiUrl);
			var request = new RestRequest("api/students/{name}", Method.GET);
			request.AddUrlSegment("name", ViewModel.FullName);

			// When an function could cause a blocking operation, it is marked as "async".
			// If we call that function, it no longer directly returns its return type;
			// instead, it returns a Task object representing the operation. Tasks can be run
			// on a different thread than the current function, if the .NET runtime decides it
			// makes sense.

			// By tradition, such methods that return Tasks are named with a "Async" or 
			// "TaskAsync" suffix.
			var task = client.ExecuteTaskAsync(request);
			// If the task is run on a different thread, then the next line of this body will
			// execute before the task is done, allowing us to do other code while our expensive
			// or blocking task is executing.

			// At some point, we need to wait for the task to actually finish, because we need
			// its return value. We can force this by awaiting the task.
			var response = await task;
			// The next line will only run after the task has finished.

			// The nice part about this setup is that our UI will continue to function while
			// the request is being made to the web server. By marking our event handler
			// (this function) as async, WPF will allow its work to be completed on a background
			// thread, freeing the UI thread to listen to new UI events.

			if (response.StatusCode == System.Net.HttpStatusCode.NotFound) {
				MessageBox.Show("Student not found");
			}
			else {
				request = new RestRequest("api/register", Method.POST);
				JObject obj = JObject.Parse(response.Content);
				request.AddJsonBody(new {
					StudentId = (int)obj["Id"],
					CourseSection = new {
						SemesterTermId = 2, // hard-code Fall 2017
						CatalogCourse = new {
							DepartmentName = nameSplit[0],
							CourseNumber = nameSplit[1]
						},
						SectionNumber = sectionNum,
					}
				});

				// You don't need to make a Task variable; you can await a function call directly.
				response = await client.ExecuteTaskAsync(request);
				if (response.StatusCode == System.Net.HttpStatusCode.NotFound) {
					MessageBox.Show("Course not found");
				}
				else {
					int result = Convert.ToInt32(response.Content);
					MessageBox.Show(result.ToString());
				}
			}
		}
	}
}
