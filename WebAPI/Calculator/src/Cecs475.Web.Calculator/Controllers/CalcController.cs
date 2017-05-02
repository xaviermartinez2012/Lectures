using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
namespace Cecs475.Web.Calculator.Controllers {
	public class BinaryOperands<T> {
		public T X { get; set; }
		public T Y { get; set; }
	}

	// This simple class will demo attribute-based routing, as opposed to convention-based routing.
	[RoutePrefix("calc")]
	public class CalcController : ApiController {
		// Our last example used convention-based routing, in which ASP.NET automatically detected 
		// methods like Get, Post, and Delete and routed them to the conventional HTTP verbs. That
		// worked for a nice simple example in which all methods involved CRUD-like operations.

		// Then we write a method and add an attribute to it for the type of request it can handle.
		[HttpGet]
		// And we also define the route for this method.
		[Route("add")]
		public int Add(int x, int y) {
			// GET methods are called using URL parameters, as in /calc/add?x=10&y=5
			return x + y;
		}

		// POST methods are called with a body that contains the arguments. Only one argument
		// can come from the body, so if we want to take multiple values in an argument, we need
		// a package type to contain them.
		[HttpPost]
		[Route("multiply")]
		public int Multiply([FromBody]BinaryOperands<int> ops) {
			return ops.X * ops.Y;
		}

		[HttpPost]
		[Route("divide")]
		public double Divide([FromBody]BinaryOperands<double> ops) {
			return ops.X / ops.Y;
		}
	}
}
