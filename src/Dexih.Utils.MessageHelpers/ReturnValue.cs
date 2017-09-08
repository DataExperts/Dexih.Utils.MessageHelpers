using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Dexih.Utils.MessageHelpers
{

    /// <summary>
    /// Provide a simple structure that can be used as a function return value containing
    /// the success, message and exception details.
    /// </summary>
    public class ReturnValue
    {
        public ReturnValue()
        {
        }

        public ReturnValue(bool success)
        {
            Success = success;
            Message = "";
            Exception = null;
        }
        public ReturnValue(bool success, string message, Exception exception)
        {
            Success = success;
            Message = message;
            Exception = exception;
        }

        public ReturnValue(Exception exception)
        {
            Success = false;
            Exception = exception;

            var message = new StringBuilder();

            if(exception == null)
            {
                message.Append( "An error was returned with no exception.");
            }
            else
            {
                message.Append("The following error occurred: " + exception.Message);

                if(exception.InnerException != null)
                {
                    message.AppendLine("An inner exception also occurred: " + exception.InnerException.Message);
                }
            }

            Message = message.ToString();
        }

        public bool ContainsError()
        {
            return Exception != null;
        }

#if DEBUG
        private string _stackTrace;
#endif

        private bool _success;
        public virtual bool Success {
            get => _success;
            set
            {
                _success = value;
#if DEBUG
                if(value == false)
                {
                     _stackTrace = Environment.StackTrace.ToString();
                }
#endif
            }
        }

        /// <summary>
        /// Message detailing the return status.
        /// </summary>
        public virtual string Message { get; set; }

        /// <summary>
        /// Exception if provided.  Note, this is not serialized.
        /// </summary>
        [JsonIgnore]
        public Exception Exception { get; set; }

        private string _exceptionDetails {get;set;} = "";

        /// <summary>
        /// Full trace of the exception.  This can either be set to a value, or 
        /// will be constructed from the exception.
        /// </summary>
        public virtual string ExceptionDetails
        {
            set => _exceptionDetails = value;
            get
            {
                if (Exception == null)
                {
                    if(string.IsNullOrEmpty(_exceptionDetails))
					{
#if DEBUG
                        return _stackTrace;
#else
                        return null;
#endif
                    }
                    return _exceptionDetails;
                }
                var properties = Exception.GetType().GetProperties();
                var fields = properties
                    .Select(property => new {
                        property.Name,
                        Value = property.GetValue(Exception, null)
                    })
                    .Select(x => string.Format(
                        "{0} = {1}",
                        x.Name,
                        x.Value != null ? x.Value.ToString() : string.Empty
                    ));
                return Message + "\n" + string.Join("\n", fields);
            }
        }
    }



}
