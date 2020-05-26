using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Interception.InterceptionBehaviors;
using Unity.Interception.PolicyInjection.Pipeline;

namespace Hsf.Framework.AOP
{
    public class LogBehavior : IInterceptionBehavior
    {
        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            Console.WriteLine("LogBehavior before");
            IMethodReturn method = getNext()(input, getNext);
            Console.WriteLine("LogBehavior after");
            return method;
        }

        public bool WillExecute
        {
            get { return true; }
        }
    }
}
