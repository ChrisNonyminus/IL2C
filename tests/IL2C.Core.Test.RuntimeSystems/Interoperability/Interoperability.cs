﻿////////////////////////////////////////////////////////////////////////////
//
// IL2C - A translator for ECMA-335 CIL/MSIL to C language.
// Copyright (c) Kouji Matsui (@kozy_kekyo, @kekyo@mastodon.cloud)
//
// Licensed under Apache-v2: https://opensource.org/licenses/Apache-2.0
//
////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace IL2C.RuntimeSystems
{
    public struct NativePointerInside
    {
        public NativePointer Pointer;
    }

    [Description("These tests are verified the IL2C manages interoperability with the P/Invoke adn IL2C/Invoke method and internalcall method.")]
    public sealed class Interoperability
    {
        [NativeMethod("windows.h", SymbolName = "OutputDebugStringW", CharSet = NativeCharSet.Unicode)]
        [MethodImpl(MethodImplOptions.InternalCall)]
        private static extern void OutputDebugString1(string message);

        [TestCase(null, new[] { "InternalCallWithUnicodeStringArgument", "OutputDebugString1" }, "ABC", Assert = TestCaseAsserts.IgnoreValidateInvokeResult)]
        public static void InternalCallWithUnicodeStringArgument(string message)
        {
            OutputDebugString1(message);
        }

        [DllImport("kernel32", EntryPoint = "OutputDebugStringW")]
        private static extern void OutputDebugString2(string message);

        [TestCase(null, new[] { "DllImportWithUnicodeStringArgument", "OutputDebugString2" }, "ABC", Assert = TestCaseAsserts.IgnoreValidateInvokeResult)]
        public static void DllImportWithUnicodeStringArgument(string message)
        {
            OutputDebugString2(message);
        }

        [TestCase(12345678, "TransparencyForNativePointer", 12345678)]
        public static IntPtr TransparencyForNativePointer(IntPtr value)
        {
            NativePointer np = value;
            IntPtr ip = np;
            return ip;
        }

        [TestCase(12345678, "TransparencyForNativePointerInsideNativeType", 12345678, IncludeTypes = new[] { typeof(NativePointerInside) })]
        public static IntPtr TransparencyForNativePointerInsideNativeType(IntPtr value)
        {
            NativePointerInside npi;
            npi.Pointer = value;
            IntPtr ip = npi.Pointer;
            return ip;
        }

        private static IntPtr ConcatAndToObjRefHandle(string a, string b)
        {
            var c = a + b;
            var handle = GCHandle.Alloc(c, GCHandleType.Pinned);
            return GCHandle.ToIntPtr(handle);
        }

        [TestCase("ABCDEF", new[] { "BypassObjRefWithObjRefHandle", "ConcatAndToObjRefHandle" }, "ABC", "DEF")]
        public static string BypassObjRefWithObjRefHandle(string a, string b)
        {
            var objRefHandle = ConcatAndToObjRefHandle(a, b);
            var handle = GCHandle.FromIntPtr(objRefHandle);
            var result = (string)handle.Target!;
            handle.Free();
            return result;
        }
    }
}
