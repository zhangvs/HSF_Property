﻿using Microsoft.Practices.Unity.InterceptionExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ruanmou.Framework.AOP.Behavior
{
    public class ExceptionLoggingBehavior : IInterceptionBehavior
    {
        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            Console.WriteLine("ExceptionLoggingBehavior");
            return getNext().Invoke(input, getNext);
        }

        public bool WillExecute
        {
            get { return true; }
        }
    }
}
