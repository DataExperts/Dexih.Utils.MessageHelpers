using System;
using System.Runtime.Serialization;

namespace Dexih.Utils.MessageHelpers
{
    /// <summary>
    /// <see cref="ReturnValue"/>
    /// </summary>
    /// <typeparam name="T">Type to return along with the ReturnValue</typeparam>
    [DataContract]
    public class ReturnValue<T> : ReturnValue
    {
        public ReturnValue() { }

        public ReturnValue(bool success, string message, Exception exception, T value)
        {
            Success = success;
            Message = message;
            Exception = exception;
            Value = value;
        }

        public ReturnValue(bool success, string message, Exception exception)
        {
            Success = success;
            Message = message;
            Exception = exception;
        }

        public ReturnValue(bool success, T value)
        {
            Success = success;
            Value = value;
            Message = "";
        }

        public ReturnValue(Exception ex)
        {
            var returnValue = new ReturnValue(ex);
            Success = returnValue.Success;
            Exception = returnValue.Exception;
            Message = returnValue.Message;
        }

        private void SetReturnValue(ReturnValue returnValue)
        {
            Success = returnValue.Success;
            Message = returnValue.Message;
            Exception = returnValue.Exception;
            if (returnValue.Exception == null)
            {
                ExceptionDetails = returnValue.ExceptionDetails;
            }
        }

        /// <summary>
        /// Sets the message from another returnValue.  
        /// Note the Value property is not set.
        /// </summary>
        /// <param name="returnValue"></param>
        public ReturnValue(ReturnValue returnValue)
        {
            SetReturnValue(returnValue);
        }

        /// <summary>
        /// Sets the message from another ReturnValue
        /// </summary>
        /// <param name="returnValue"></param>
        public ReturnValue(ReturnValue<object> returnValue)
        {
            SetReturnValue(returnValue);
            Value = (T)returnValue.Value;
        }

//        /// <summary>
//        /// Deserializes the value of a JToken value.
//        /// </summary>
//        /// <param name="returnValue"></param>
//        public ReturnValue(ReturnValue<JToken> returnValue)
//        {
//            SetReturnValue(returnValue);
//            Value = returnValue.Value.ToObject<T>();
//        }
//
//        /// <summary>
//        /// Creates a new ReturnValue with the Value serialized to a JToken.
//        /// </summary>
//        /// <returns></returns>
//        public ReturnValue<JToken> GetJToken()
//        {
//            JToken jValue = null;
//            if (Value != null)
//            {
//                jValue = JToken.FromObject(Value); // Json.JTokenFromObject(Value, "");
//            }
//            var result = new ReturnValue<JToken>(Success, Message, Exception, jValue);
//            return result;
//        }

        [DataMember(Order =3)]
        public virtual T Value { get; set; }

    }
}
