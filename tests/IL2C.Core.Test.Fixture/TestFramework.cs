﻿using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;
using IL2C.Metadata;

namespace IL2C
{
    public static class TestFramework
    {
        private static readonly string il2cRuntimePath =
            Path.GetFullPath(
                Path.Combine(
                    Path.GetDirectoryName(typeof(TestFramework).Assembly.Location),
                    "..",
                    "..",
                    "..",
                    "..",
                    "..",
                    "IL2C.Runtime"));

        private static string GetCLanguageLiteralExpression(object value)
        {
            if (value == null) return "NULL";
            if (value.Equals(int.MinValue)) return "INT32_MIN";
            if (value.Equals(long.MinValue)) return "INT64_MIN";
            if (value is long) return string.Format("INT64_C({0})", value);
            if (value is ulong) return string.Format("UINT64_C({0})", value);
            if (value is uint) return string.Format("UINT32_C({0})", value);
            if (value is float) return string.Format("{0:g9}f", value);
            if (value is double) return string.Format("{0:g17}", value);
            if (value is bool) return (bool)value ? "true" : "false";
            if (value is char) return "L'" + value + "'";
            if (value is string) return "il2c_new_string(L\"" + value + "\")";
            return value.ToString();
        }

        private static string GetCLanguagePrintFormatFromType(ITypeInformation type)
        {
            return
                (type.IsByteType) ? "%u" :
                (type.IsSByteType) ? "%d" :
                (type.IsUInt16Type) ? "%u" :
                (type.IsInt16Type) ? "%d" :
                (type.IsUInt32Type) ? "%u" :
                (type.IsInt32Type) ? "%d" :
                (type.IsUInt64Type) ? "%lld" :
                (type.IsInt64Type) ? "%llu" :
                (type.IsSingleType) ? "%f" :
                (type.IsDoubleType) ? "%lf" :
                (type.IsIntPtrType) ? "0x%08x" :
                (type.IsUIntPtrType) ? "0x%08x" :
                (type.IsBooleanType) ? "%d" :
                (type.IsCharType) ? "'%c'" :
                (type.IsStringType) ? "\\\"%s\\\"" :
                "%s";
        }

        public static async Task ExecuteTestAsync(MethodInfo method, object expected, object[] args)
        {
            Assert.IsTrue(method.IsPublic && method.IsStatic);

            var translateContext = new TranslateContext(method.DeclaringType.Assembly.Location, false);

            var prepared = AssemblyPreparer.Prepare(
                translateContext, m => m.Name == method.Name);

            var targetMethod =
                translateContext.Assembly.Modules
                .First().Types
                .First(t => t.FriendlyName == method.DeclaringType.FullName).DeclaredMethods
                .First(m => m.Name == method.Name);

            var body = new StringWriter();
            AssemblyWriter.WriteConstStrings(
                body, translateContext);
            AssemblyWriter.InternalConvertFromMethod(
                body, translateContext, prepared, targetMethod, "  ", DebugInformationOptions.Full);

            var translatedPath = Path.GetFullPath(
                Path.Combine(
                    Path.GetDirectoryName(method.DeclaringType.Assembly.Location),
                    "translated",
                    targetMethod.DeclaringType.Name,
                    targetMethod.Name));
            await Task.Run(() =>
            {
                if (Directory.Exists(translatedPath))
                {
                    try
                    {
                        Directory.Delete(translatedPath);
                    }
                    catch
                    {
                    }
                }
                Directory.CreateDirectory(translatedPath);
            }).ConfigureAwait(false);

            var templatePath = Path.Combine(translatedPath, "test.vcxproj");
            using (var fs = new FileStream(templatePath, FileMode.Create, FileAccess.ReadWrite, FileShare.None, 65536, true))
            {
                using (var ts = typeof(TestFramework).Assembly.GetManifestResourceStream("IL2C.Templates.test.vcxproj"))
                {
                    await ts.CopyToAsync(fs);
                    await fs.FlushAsync();
                }
            }

            var sourceCode = new StringBuilder();
            using (var ts = typeof(TestFramework).Assembly.GetManifestResourceStream("IL2C.Templates.test.c"))
            {
                var tr = new StreamReader(ts);
                sourceCode.Append(await tr.ReadToEndAsync());
            }

            sourceCode.Replace("{body}", body.ToString());

            var expectedType = targetMethod.ReturnType;
            sourceCode.Replace("{isRefType}", (expectedType.IsByReference || expectedType.IsClass) ? "1" : "0");

            sourceCode.Replace("{type}", targetMethod.ReturnType.CLanguageTypeName);
            sourceCode.Replace("{expected}", GetCLanguageLiteralExpression(expected));
            sourceCode.Replace("{function}", targetMethod.CLanguageFunctionName);
            sourceCode.Replace("{arguments}", string.Join(", ", args.Select(GetCLanguageLiteralExpression)));
            sourceCode.Replace("{equality}", expectedType.IsStringType ? "wcscmp(expected->string_body__, actual->string_body__) == 0" : "expected == actual");
            sourceCode.Replace("{format}", GetCLanguagePrintFormatFromType(targetMethod.ReturnType));
            sourceCode.Replace("{expectedExpression}", expectedType.IsBooleanType ? "expected ? \"true\" : \"false\"" : "expected");
            sourceCode.Replace("{actualExpression}", expectedType.IsBooleanType ? "actual ? \"true\" : \"false\"" : "actual");

            var sourcePath = Path.Combine(translatedPath, "test.c");
            using (var fs = new FileStream(sourcePath, FileMode.Create, FileAccess.ReadWrite, FileShare.None, 65536, true))
            {
                var tw = new StreamWriter(fs);

                await tw.WriteAsync(sourceCode.ToString());
                await tw.FlushAsync();
            }

            // Step1: Test real IL code at this runtime.
            var rawResult = method.Invoke(null, args);
            Assert.AreEqual(expected, rawResult);

            // Step2: Test compiled C source code and execute.
            var il2cRuntimeSourcePath = Path.Combine(il2cRuntimePath, "il2c.c");
            var executedResult = await GccDriver.CompileAndRunAsync(sourcePath, new[] { il2cRuntimeSourcePath }, il2cRuntimePath);
            var sanitized = executedResult.Trim(' ', '\r', '\n');

            Assert.AreEqual("Success", sanitized);
        }
    }
}