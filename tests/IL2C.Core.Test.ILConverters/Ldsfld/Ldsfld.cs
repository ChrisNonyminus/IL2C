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
    public static class Ldsfld_Field
    {
        public static readonly bool TrueValue = true;
        public static readonly bool FalseValue = false;
        public static readonly byte ByteValue = byte.MaxValue;
        public static readonly short Int16Value = short.MaxValue;
        public static readonly int Int32Value = int.MaxValue;
        public static readonly long Int64Value = long.MaxValue;
        public static readonly sbyte SByteValue = sbyte.MaxValue;
        public static readonly ushort UInt16Value = ushort.MaxValue;
        public static readonly uint UInt32Value = uint.MaxValue;
        public static readonly ulong UInt64Value = ulong.MaxValue;
        public static readonly IntPtr IntPtrValue = (IntPtr)int.MaxValue;
        public static readonly UIntPtr UIntPtrValue = (UIntPtr)uint.MaxValue;
        public static readonly float SingleValue = 3.14159274f;
        public static readonly double DoubleValue = 3.1415926535897931;
        public static readonly char CharValue = 'A';
        public static readonly string StringValue = "ABC";
    }

    public sealed class Ldsfld
    {
        [TestCase(true, "True", IncludeTypes = new[] { typeof(Ldsfld_Field) })]
        [MethodImpl(MethodImplOptions.ForwardRef)]
        public static extern bool True();

        [TestCase(false, "False", IncludeTypes = new[] { typeof(Ldsfld_Field) })]
        [MethodImpl(MethodImplOptions.ForwardRef)]
        public static extern bool False();

        [TestCase(byte.MaxValue, "Byte", IncludeTypes = new[] { typeof(Ldsfld_Field) })]
        [MethodImpl(MethodImplOptions.ForwardRef)]
        public static extern byte Byte();

        [TestCase(short.MaxValue, "Int16", IncludeTypes = new[] { typeof(Ldsfld_Field) })]
        [MethodImpl(MethodImplOptions.ForwardRef)]
        public static extern short Int16();

        [TestCase(int.MaxValue, "Int32", IncludeTypes = new[] { typeof(Ldsfld_Field) })]
        [MethodImpl(MethodImplOptions.ForwardRef)]
        public static extern int Int32();

        [TestCase(long.MaxValue, "Int64", IncludeTypes = new[] { typeof(Ldsfld_Field) })]
        [MethodImpl(MethodImplOptions.ForwardRef)]
        public static extern long Int64();

        [TestCase(sbyte.MaxValue, "SByte", IncludeTypes = new[] { typeof(Ldsfld_Field) })]
        [MethodImpl(MethodImplOptions.ForwardRef)]
        public static extern sbyte SByte();

        [TestCase(ushort.MaxValue, "UInt16", IncludeTypes = new[] { typeof(Ldsfld_Field) })]
        [MethodImpl(MethodImplOptions.ForwardRef)]
        public static extern ushort UInt16();

        [TestCase(uint.MaxValue, "UInt32", IncludeTypes = new[] { typeof(Ldsfld_Field) })]
        [MethodImpl(MethodImplOptions.ForwardRef)]
        public static extern uint UInt32();

        [TestCase(ulong.MaxValue, "UInt64", IncludeTypes = new[] { typeof(Ldsfld_Field) })]
        [MethodImpl(MethodImplOptions.ForwardRef)]
        public static extern ulong UInt64();

            [MethodImpl(MethodImplOptions.ForwardRef)]
            private static extern IntPtr IntPtrImpl();

        [TestCase(int.MaxValue, "IntPtr", IncludeTypes = new[] { typeof(Ldsfld_Field) })]
        public static int IntPtr() =>
            IntPtrImpl().ToInt32();

            [MethodImpl(MethodImplOptions.ForwardRef)]
            private static extern UIntPtr UIntPtrImpl();

        [TestCase(uint.MaxValue, "UIntPtr", IncludeTypes = new[] { typeof(Ldsfld_Field) })]
        public static uint UIntPtr() =>
            UIntPtrImpl().ToUInt32();

        [TestCase(3.14159274f, "Single", IncludeTypes = new[] { typeof(Ldsfld_Field) })]
        [MethodImpl(MethodImplOptions.ForwardRef)]
        public static extern float Single();

        [TestCase(3.1415926535897931, "Double", IncludeTypes = new[] { typeof(Ldsfld_Field) })]
        [MethodImpl(MethodImplOptions.ForwardRef)]
        public static extern double Double();

        [TestCase('A', "Char", IncludeTypes = new[] { typeof(Ldsfld_Field) })]
        [MethodImpl(MethodImplOptions.ForwardRef)]
        public static extern char Char();

        [TestCase("ABC", "String", IncludeTypes = new[] { typeof(Ldsfld_Field) })]
        [MethodImpl(MethodImplOptions.ForwardRef)]
        public static extern string String();
    }
}
