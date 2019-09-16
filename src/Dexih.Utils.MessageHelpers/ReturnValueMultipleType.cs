using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Dexih.Utils.MessageHelpers
{
    [DataContract]
    public class ReturnValueMultiple<T> : ReturnValueMultiple
    {
        [DataMember(Order = 10)]
        public override List<ReturnValue> ReturnValues
        {
            get => ReturnValueTypes.ToList<ReturnValue>();
        }

        [DataMember(Order = 11)]
        public List<ReturnValue<T>> ReturnValueTypes { protected set; get; } = new List<ReturnValue<T>>();

        public void Add(ReturnValue<T> returnValue)
        {
            ReturnValueTypes.Add(returnValue);
        }

        [DataMember(Order = 12)]
        public T[] Value
        {
            get
            {
                return ReturnValueTypes.Select(c => c.Value).ToArray();
            }
        }
    }
}
