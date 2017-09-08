using Dexih.Utils.MessageHelpers;
using System;
using Xunit;
using Newtonsoft;
using Newtonsoft.Json;

namespace Dexih.Utils.MessageHelpersTests
{
    public class RemoteValueTypeTests
    {
        [Fact]
        public void ReturnValueWithValue()
        {
            var returnValue = new ReturnValue<string>(true, "hello");

            Assert.Equal(true, returnValue.Success);
            Assert.Equal("hello", returnValue.Value);
            Assert.Equal("", returnValue.Message);
            Assert.Equal(null, returnValue.Exception);
        }

        [Fact]
        public void ReturnValueTypeWithMessage()
        {
            var returnValue = new ReturnValue<string>(false, "The message", new Exception("The exception"));

            Assert.Equal(false, returnValue.Success);
            Assert.Equal("The message", returnValue.Message);
            Assert.Equal("The exception", returnValue.Exception.Message);
            Assert.Null(returnValue.Value);
        }

        [Fact]
        public void ReturnValueTypeWithException()
        {
            var returnValue = new ReturnValue<string>(new Exception("The exception"));

            Assert.Equal(false, returnValue.Success);
            Assert.Equal("The exception", returnValue.Exception.Message);
            Assert.True(returnValue.Message.Contains("The exception"));
            Assert.Null(returnValue.Value);
        }

        /// <summary>
        /// Test the ExceptionDetails captures the full exception and inner exceptions.
        /// </summary>
        [Fact]
        public void ReturnValueTypeCheckExceptionDetails()
        {
            var exception = new Exception("The inner exception");
            var aggregateException = new AggregateException("The outer exception", exception);

            var returnValue = new ReturnValue<string>(aggregateException);

            Assert.True(returnValue.ExceptionDetails.Contains("The inner exception"));
            Assert.True(returnValue.ExceptionDetails.Contains("The outer exception"));
        }

        [Fact]
        public void ReturnValueTypeSerialize()
        {
            var returnValue = new ReturnValue<string>(false, "The message", new Exception("The exception"), "the value");

            var serialized = JsonConvert.SerializeObject(returnValue);
            var returnValue2 = JsonConvert.DeserializeObject<ReturnValue<string>>(serialized);

            Assert.Equal(false, returnValue2.Success);
            Assert.Equal("the value", returnValue2.Value);
            Assert.Equal("The message", returnValue2.Message);
            Assert.Null(returnValue2.Exception);
            Assert.True(returnValue2.ExceptionDetails.Contains("The exception"));

        }

        [Fact]
        public void ReturnValueTypeFromOtherReturnValue()
        {
            var returnValue = new ReturnValue(true, "the message", new Exception("the exception"));

            var returnTypeValue = new ReturnValue<string>(returnValue);

            Assert.Equal(true, returnTypeValue.Success);
            Assert.Equal("the message", returnTypeValue.Message);
            Assert.Equal("the exception", returnTypeValue.Exception.Message);
        }
    }
}
