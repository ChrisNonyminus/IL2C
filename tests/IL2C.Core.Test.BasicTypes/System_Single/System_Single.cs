////////////////////////////////////////////////////////////////////////////
//
// IL2C - A translator for ECMA-335 CIL/MSIL to C language.
// Copyright (c) Kouji Matsui (@kozy_kekyo, @kekyo@mastodon.cloud)
//
// Licensed under Apache-v2: https://opensource.org/licenses/Apache-2.0
//
////////////////////////////////////////////////////////////////////////////

using System.Runtime.CompilerServices;

namespace IL2C.BasicTypes
{
    // Some tests are using the IgnoreInvokeResult flag, because float string format is bit different (but no problem.)
    [TestCase(true, "IsValueType")]
    [TestCase(4, "SizeOf")]
    [TestCase("3.4028235e+038", "ToString", float.MaxValue, Assert = TestCaseAsserts.IgnoreValidateInvokeResult)]    // Real value is 3.40282347E+38
    [TestCase("-3.4028235e+038", "ToString", float.MinValue, Assert = TestCaseAsserts.IgnoreValidateInvokeResult)]   // Real value is -3.40282347E+38
    [TestCase(float.MaxValue, "TryParse", "3.40282347E+38")]
    [TestCase(float.MinValue, "TryParse", "-3.40282347E+38")]
    public sealed class System_Single
    {
        [MethodImpl(MethodImplOptions.ForwardRef)]
        public static extern bool IsValueType();

        [MethodImpl(MethodImplOptions.ForwardRef)]
        public static extern int SizeOf();

        public static string ToString(float value)
        {
            return value.ToString();
        }

        public static float TryParse(string str)
        {
            float.TryParse(str, out var value);
            return value;
        }
    }
}
