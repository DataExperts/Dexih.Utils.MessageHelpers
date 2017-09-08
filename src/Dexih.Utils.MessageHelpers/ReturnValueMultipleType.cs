using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dexih.Utils.MessageHelpers
{
    public class ReturnValueMultiple<T> : ReturnValueMultiple
    {
        public override List<ReturnValue> ReturnValues
        {
            get => ReturnValueTypes.ToList<ReturnValue>();
        }

        public List<ReturnValue<T>> ReturnValueTypes { protected set; get; } = new List<ReturnValue<T>>();

        public void Add(ReturnValue<T> returnValue)
        {
            ReturnValueTypes.Add(returnValue);
        }

        public T[] Value
        {
            get
            {
                return ReturnValueTypes.Select(c => c.Value).ToArray();
            }
        }
    }
}
