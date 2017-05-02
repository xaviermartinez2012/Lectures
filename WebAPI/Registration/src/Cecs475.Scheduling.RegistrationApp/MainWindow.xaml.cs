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

		private void mValidateBtn_Click(object sender, RoutedEventArgs e) {
			var client = new RestClient(mApiText.Text);
			var request = new RestRequest("api/students/{name}", Method.GET);
			request.AddUrlSegment("name", mStudentText.Text);

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


			var client = new RestClient(mApiText.Text);
			var request = new RestRequest("api/students/{name}", Method.GET);
			request.AddUrlSegment("name", mStudentText.Text);
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
						CourseName = courseSplit[0],
						SectionNumber = sectionNum,
						SemesterTermName = "Fall 2017"
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
	}
}
