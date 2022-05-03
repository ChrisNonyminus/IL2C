﻿////////////////////////////////////////////////////////////////////////////
//
// IL2C - A translator for ECMA-335 CIL/MSIL to C language.
// Copyright (c) Kouji Matsui (@kozy_kekyo, @kekyo@mastodon.cloud)
//
// Licensed under Apache-v2: https://opensource.org/licenses/Apache-2.0
//
////////////////////////////////////////////////////////////////////////////

using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using IL2C.Metadata;

namespace IL2C.Writers
{
    internal static class TypeDependency
    {
        private sealed class Context
        {
            private readonly Dictionary<ITypeInformation, HashSet<ITypeInformation>> dependTypeCache =
                new Dictionary<ITypeInformation, HashSet<ITypeInformation>>();
            private readonly Dictionary<(ITypeInformation, ITypeInformation), bool> isDependCache =
                new Dictionary<(ITypeInformation, ITypeInformation), bool>();
            private readonly HashSet<IAssemblyInformation> regions;

            public Context(IEnumerable<IAssemblyInformation> regions) =>
                this.regions = new HashSet<IAssemblyInformation>(regions);

            private ISet<ITypeInformation> GetDependentTypes(ITypeInformation type, bool onlyPublic)
            {
                if (!dependTypeCache.TryGetValue(type, out var results))
                {
                    results = new HashSet<ITypeInformation>();
                    dependTypeCache.Add(type, results);

                    var methods = type.DeclaredMethods.
                        Where(t => !onlyPublic || t.IsPublic).
                        ToArray();

                    foreach (var t in
                        new[] { type, type.BaseType }.
                        Concat(type.InterfaceTypes.
                            Where(t => !onlyPublic || t.IsPublic)).
                        Concat(type.NestedTypes.
                            Where(t => !onlyPublic || t.IsPublic)).
                        Concat(type.Fields.
                            Where(t => !onlyPublic || t.IsPublic).
                            Select(field => field.FieldType)).
                        Concat(methods.
                            Select(method => method.ReturnType)).
                        Concat(methods.
                            SelectMany(method => method.Parameters.Select(parameter => parameter.TargetType))).
                        Concat(methods.
                            SelectMany(method => method.CodeStream?.Select(ci => ci.Operand).
                                OfType<ITypeInformation>() ??
                                Enumerable.Empty<ITypeInformation>())).
                        Where(t => t != null).
                        Select(MetadataUtilities.UnwrapCoveredType).
                        Distinct())
                    {
                        results.Add(t);
                    }
                }

                return results;
            }

            public bool IsDepend(ITypeInformation to, ITypeInformation from)
            {
                if (!isDependCache.TryGetValue((to, from), out var result))
                {
                    // Calculate dependency only public members if refer to external assemblies.
                    var fromInRegion = regions.Contains(from.DeclaringModule.DeclaringAssembly);
                    var fromTypes = GetDependentTypes(from, !fromInRegion);
                    result = fromTypes.Contains(to);

                    isDependCache.Add((to, from), result);
                }

                return result;
            }
        }

        public static IEnumerable<ITypeInformation> OrderByDependant(
            this IEnumerable<ITypeInformation> types,
            IEnumerable<IAssemblyInformation> regions)
        {
            var context = new Context(regions.Distinct());
            var list = new LinkedList<ITypeInformation>();
            foreach (var type in types.
                Select(MetadataUtilities.UnwrapCoveredType).
                Distinct())
            {
                var phase = 1;
                var node = list.Last;
                while (node != null)
                {
                    if (node.Value.Equals(type))
                    {
                        break;
                    }

                    if (phase == 1)
                    {
                        if (context.IsDepend(type, node.Value))
                        {
                            phase = 2;
                        }
                    }
                    if (context.IsDepend(node.Value, type))
                    {
                        list.AddAfter(node, type);
                        break;
                    }

                    node = node.Previous;
                }

                if (node == null)
                {
                    if (phase == 1)
                    {
                        list.AddLast(type);
                    }
                    else
                    {
                        list.AddFirst(type);
                    }
                }
            }

            return list;
        }

        public static IEnumerable<ITypeInformation> OrderByDependant(
            this IEnumerable<ITypeInformation> types,
            params IAssemblyInformation[] regions)
        {
            return types.OrderByDependant((IEnumerable<IAssemblyInformation>)regions);
        }
    }
}
