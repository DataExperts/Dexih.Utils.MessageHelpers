using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Runtime.Serialization;

namespace Dexih.Utils.MessageHelpers
{
    [DataContract]
    public class ReturnValueMultiple : ReturnValue
    {
        [DataMember(Order = 3)]
        public virtual List<ReturnValue> ReturnValues { get; protected set; } = new List<ReturnValue>();

        public void Add(ReturnValue returnValue)
        {
            ReturnValues.Add(returnValue);
        }

        public override bool Success
        {
            get
            {
                // if no returnValues return false
                if (ReturnValues.Count == 0) return false;

                // if any returnValue contains false, return false,
                return !ReturnValues.Exists(c => c.Success == false);
            }
        }

        public override string Message
        {
            get
            {
                var message = new StringBuilder();

                message.AppendLine($"{ReturnValues.Count(c => c.Success)} successful, {ReturnValues.Count(c => !c.Success)} failed.");

                foreach (var returnValue in ReturnValues.Where(c => !string.IsNullOrEmpty(c.Message)))
                {
                    message.AppendLine($"{returnValue.Success}: {returnValue.Message}.");
                }

                return message.ToString();
            }
        }

        public override string ExceptionDetails
        {
            get
            {
                var exceptionDetails = new StringBuilder();
                foreach (var returnValue in ReturnValues.Where(c => c.Exception != null))
                {
                    if (!string.IsNullOrEmpty(returnValue.ExceptionDetails))
                    {
                        exceptionDetails.AppendLine("Exception Details: " + returnValue.ExceptionDetails);
                    }
                }

                return exceptionDetails.ToString();
            }
        }

    }
}
