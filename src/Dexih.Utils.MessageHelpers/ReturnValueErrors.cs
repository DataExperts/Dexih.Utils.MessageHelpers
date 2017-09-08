//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace Dexih.Utils.MessageHelpers
//{
//    public class ReturnValueErrors<T> : ReturnValue
//    {
//        public ReturnValueErrors(bool success, T value, List<KeyValuePair<string, string>> errors)
//        {
//            Success = success;
//            Errors = errors;
//            Value = value;
//        }

//        public ReturnValueErrors(bool success, string message, Exception exception)
//        {
//            Success = success;
//            Message = message;
//            Exception = exception;
//        }

//        public ReturnValueErrors(ReturnValue returnValue)
//        {
//            Success = returnValue.Success;
//            Message = returnValue.Message;
//            Exception = returnValue.Exception;
//        }

//        public void Add(string name, string message)
//        {
//            if (Errors == null)
//                Errors = new List<KeyValuePair<string, string>>();

//            Errors.Add(new KeyValuePair<string, string>(name, message));
//        }

//        public List<KeyValuePair<string, string>> Errors { get; set; }
//        public T Value { get; set; }

//    }
//}
