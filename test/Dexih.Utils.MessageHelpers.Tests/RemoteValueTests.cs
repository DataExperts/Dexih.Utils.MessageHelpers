using Dexih.Utils.MessageHelpers;
using System;
using Xunit;
using Newtonsoft;
using Newtonsoft.Json;

namespace Dexih.Utils.MessageHelpersTests
{
    public class RemoteValueTests
    {
        [Fact]
        public void ReturnValueTrue()
        {
            var returnValue = new ReturnValue(true);

            Assert.Equal(true, returnValue.Success);
            Assert.Equal("", returnValue.Message);
            Assert.Equal(null, returnValue.Exception);
        }

        [Fact]
        public void ReturnValueWithMessage()
        {
            var returnValue = new ReturnValue(false, "The message", new Exception("The exception"));

            Assert.Equal(false, returnValue.Success);
            Assert.Equal("The message", returnValue.Message);
            Assert.Equal("The exception", returnValue.Exception.Message);
        }

        [Fact]
        public void ReturnValueWithException()
        {
            var returnValue = new ReturnValue(new Exception("The exception"));

            Assert.Equal(false, returnValue.Success);
            Assert.Equal("The exception", returnValue.Exception.Message);
            Assert.True(returnValue.Message.Contains("The exception"));
        }

        /// <summary>
        /// Test the ExceptionDetails captures the full exception and inner exceptions.
        /// </summary>
        [Fact]
        public void ReturnValueCheckExceptionDetails()
        {
            var exception = new Exception("The inner exception");
            var aggregateException = new AggregateException("The outer exception", exception);

            var returnValue = new ReturnValue(aggregateException);

            Assert.True(returnValue.ExceptionDetails.Contains("The inner exception"));
            Assert.True(returnValue.ExceptionDetails.Contains("The outer exception"));
        }

        /// <summary>
        /// Confirm the returnvalue will serialized/deserialize.
        /// Note the exception does not serialized, and moves the information to 
        /// the ExceptionDetails.
        /// </summary>
        [Fact]
        public void ReturnValueSerialize()
        {
            var returnValue = new ReturnValue(false, "The message", new Exception("The exception"));

            var serialized = JsonConvert.SerializeObject(returnValue);
            var returnValue2 = JsonConvert.DeserializeObject<ReturnValue>(serialized);

            Assert.Equal(false, returnValue2.Success);
            Assert.Equal("The message", returnValue2.Message);
            Assert.Null(returnValue2.Exception);
            Assert.True(returnValue2.ExceptionDetails.Contains("The exception"));

        }

    }
}
