using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cecs475.Scheduling.RegistrationApp {
	public class RegistrationViewModel {
		/// <summary>
		/// A URL path to the registration web service.
		/// </summary>
		public string ApiUrl { get; set; }
		/// <summary>
		/// The full name of the student who is registering.
		/// </summary>
		public string FullName { get; set; }
	}
}
