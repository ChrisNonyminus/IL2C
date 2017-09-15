﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace il2c
{
    class Test
    {
        public static int main()
        {
            var a = 1;
            var b = 2;
            var c = a + b;
            return c;
        }
    }

    class Program
    {
        private static readonly Dictionary<ushort, OpCode> opCodes;

        static Program()
        {
            var opCodesType = typeof(OpCodes);
            var opCodeList = opCodesType.GetFields(
                BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);
            opCodes = opCodeList
                .Select(fi => (OpCode)fi.GetValue(null))
                .Where(opCode => opCode.OpCodeType != OpCodeType.Nternal)
                .ToDictionary(opCode => (ushort)opCode.Value, opCode => opCode);
        }

        private static IEnumerable<Tuple<OpCode, object>> DecodeAndEnumerateOpCodes(MethodInfo methodInfo)
        {
            var mainBody = methodInfo.GetMethodBody();
            var locals = mainBody.LocalVariables;
            var ilBytes = mainBody.GetILAsByteArray();

            var index = 0;
            while (index < ilBytes.Length)
            {
                var byte0 = ilBytes[index++];
                if (opCodes.TryGetValue(byte0, out var opCode) == false)
                {
                    var byte1 = ilBytes[index++];
                    var word = (ushort)(byte0 << 8 | byte1);
                    opCode = opCodes[word];
                }

                switch (opCode.OperandType)
                {
                    case OperandType.ShortInlineBrTarget:
                        object operand = ilBytes[index++];
                        yield return Tuple.Create(opCode, operand);
                        break;
                    default:
                        yield return Tuple.Create(opCode, default(object));
                        break;
                }
            }
        }

        static void Main(string[] args)
        {
            var testType = typeof(Test);
            var mainMethod = testType.GetMethods().First();

            foreach (var entry in DecodeAndEnumerateOpCodes(mainMethod))
            {
                Console.WriteLine(
                    "{0}{1}",
                    entry.Item1,
                    (entry.Item2 != null) ? (" " + entry.Item2) : "");
            }
        }
    }
}
