////////////////////////////////////////////////////////////////////////////
//
// IL2C - A translator for ECMA-335 CIL/MSIL to C language.
// Copyright (c) Kouji Matsui (@kozy_kekyo, @kekyo@mastodon.cloud)
//
// Licensed under Apache-v2: https://opensource.org/licenses/Apache-2.0
//
////////////////////////////////////////////////////////////////////////////

using System.Runtime.CompilerServices;

namespace IL2C.ILConverters
{
    [TestCase(long.MaxValue, "MaxValue")]
    [TestCase(long.MinValue, "MinValue")]
    public sealed class Ldc_i8
    {
        [MethodImpl(MethodImplOptions.ForwardRef)]
        public static extern long MaxValue();

        [MethodImpl(MethodImplOptions.ForwardRef)]
        public static extern long MinValue();
    }
}
