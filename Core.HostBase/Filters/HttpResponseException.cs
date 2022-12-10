using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.HostBase.Filters
{
    /// <summary>
    /// The derived classes should define at least three constructors: 
    /// one parameterless constructor, 
    /// one that sets the message property, 
    /// and one that sets both the Message and InnerException properties
    /// </summary>
    public class HttpResponseException : Exception
    {
        public HttpResponseException() : base() { }
        public HttpResponseException(string message) :base(message) { }
        public HttpResponseException(string message,Exception inner) : base(message,inner) { }
    }
}
