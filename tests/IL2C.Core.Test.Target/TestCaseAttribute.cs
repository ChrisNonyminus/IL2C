﻿using System;
using System.Linq;

namespace IL2C
{
    public enum TestCaseAsserts
    {
        PerfectMatch,
        IgnoreInvokeResult,
        CauseBreak
    }

    // It's test case attribute contains expected value, method name and argument values at overall.
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class TestCaseAttribute : Attribute
    {
        public TestCaseAttribute(object expected, string methodName, params object[] args)
        {
            this.MethodName = methodName;
            this.AdditionalMethodNames = new string[0];
            this.Expected = expected;
            this.Arguments = args;
            this.Assert = TestCaseAsserts.PerfectMatch;
        }

        // This overload contains additional methods, those are used from the test method (first methodName entry.)
        public TestCaseAttribute(object expected, string[] methodNames, params object[] args)
        {
            this.MethodName = methodNames[0];   // test method
            this.AdditionalMethodNames = methodNames.Skip(1).ToArray();   // additionals
            this.Expected = expected;
            this.Arguments = args;
            this.Assert = TestCaseAsserts.PerfectMatch;
        }

        public string MethodName { get; }
        public string[] AdditionalMethodNames { get; }
        public object Expected { get; }
        public object[] Arguments { get; }

        public TestCaseAsserts Assert { get; set; }
    }
}