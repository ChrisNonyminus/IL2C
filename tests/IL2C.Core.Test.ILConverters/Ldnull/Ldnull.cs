////////////////////////////////////////////////////////////////////////////
//
// IL2C - A translator for ECMA-335 CIL/MSIL to C language.
// Copyright (c) Kouji Matsui (@kozy_kekyo, @kekyo@mastodon.cloud)
//
// Licensed under Apache-v2: https://opensource.org/licenses/Apache-2.0
//
////////////////////////////////////////////////////////////////////////////

using System;
using System.Runtime.CompilerServices;

namespace IL2C.ILConverters
{
    [TestCase(0, "IntPtrZero")]
    [TestCase((uint)0, "UIntPtrZero")]
    [TestCase(null, "NullReference")]
    public sealed class Ldnull
    {
        [MethodImpl(MethodImplOptions.ForwardRef)]
        public static extern IntPtr IntPtrZero();

        [MethodImpl(MethodImplOptions.ForwardRef)]
        public static extern UIntPtr UIntPtrZero();

        [MethodImpl(MethodImplOptions.ForwardRef)]
        public static extern object NullReference();
    }
}
