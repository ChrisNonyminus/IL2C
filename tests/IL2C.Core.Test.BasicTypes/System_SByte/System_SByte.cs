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
    [TestCase(true, "IsValueType")]
    [TestCase(1, "SizeOf")]
    [TestCase("127", "ToString", sbyte.MaxValue)]
    [TestCase("-128", "ToString", sbyte.MinValue)]
    [TestCase(sbyte.MaxValue, "TryParse", "127")]
    [TestCase(sbyte.MinValue, "TryParse", "-128")]
    public sealed class System_SByte
    {
        [MethodImpl(MethodImplOptions.ForwardRef)]
        public static extern bool IsValueType();

        [MethodImpl(MethodImplOptions.ForwardRef)]
        public static extern int SizeOf();

        public static string ToString(sbyte value)
        {
            return value.ToString();
        }

        public static sbyte TryParse(string str)
        {
            sbyte.TryParse(str, out var value);
            return value;
        }
    }
}
