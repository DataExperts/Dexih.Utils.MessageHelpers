using Dexih.Utils.MessageHelpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Dexih.Utils.MessageHelpersTests
{
    public class ReturnValueMultipleTypesTests
    {
        private ReturnValueMultiple<string> CreateReturnValueMultiple()
        {
            var returnValue1 = new ReturnValue<string>(true, "The message 1", new Exception("The exception 1"), "value 1");
            var returnValue2 = new ReturnValue<string>(true, "The message 2", new Exception("The exception 2"), "value 2");

            var returnValueMultiple = new ReturnValueMultiple<string>();
            returnValueMultiple.Add(returnValue1);
            returnValueMultiple.Add(returnValue2);
            return returnValueMultiple;
        }
        [Fact]
        public void ReturnValueMultipleSimple()
        {
            var returnValueMultiple = CreateReturnValueMultiple();

            Assert.Equal(true, returnValueMultiple.Success);
            Assert.True(returnValueMultiple.Message.Contains("The message 1"));
            Assert.True(returnValueMultiple.Message.Contains("The message 2"));
            Assert.Equal("value 1", returnValueMultiple.Value[0]);
            Assert.Equal("value 2", returnValueMultiple.Value[1]);

            // add a value, false should change the overall result to false.
            var returnValue3 = new ReturnValue<string>(false, "The message 3", new Exception("The exception 3"), "value 3");
            returnValueMultiple.Add(returnValue3);

            Assert.Equal(false, returnValueMultiple.Success);
            Assert.True(returnValueMultiple.Message.Contains("The message 3"));
            Assert.Equal("value 3", returnValueMultiple.Value[2]);
        }

        [Fact]
        public void ReturnValueMultipleSerialize()
        {
            var returnValueMultiple = CreateReturnValueMultiple();

            var serrialized = JsonConvert.SerializeObject(returnValueMultiple);

            //deserialize as a basic ReturnValue
            var returnValue = JsonConvert.DeserializeObject<ReturnValue>(serrialized);

            Assert.Equal(true, returnValueMultiple.Success);
            Assert.True(returnValueMultiple.Message.Contains("The message 1"));
            Assert.True(returnValueMultiple.Message.Contains("The message 2"));
            Assert.Equal("value 1", returnValueMultiple.Value[0]);
            Assert.Equal("value 2", returnValueMultiple.Value[1]);

            //deserialize as a ReturnValueMultiple
            var returnValueMultiple2 = JsonConvert.DeserializeObject<ReturnValueMultiple>(serrialized);

            Assert.Equal(true, returnValueMultiple2.Success);
            Assert.True(returnValueMultiple2.Message.Contains("The message 1"));
            Assert.True(returnValueMultiple2.Message.Contains("The message 2"));
            Assert.Equal("value 1", returnValueMultiple.Value[0]);
            Assert.Equal("value 2", returnValueMultiple.Value[1]);
            Assert.Equal(2, returnValueMultiple2.ReturnValues.Count);
        }
    }
}
