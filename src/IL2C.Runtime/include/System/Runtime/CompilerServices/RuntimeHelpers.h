////////////////////////////////////////////////////////////////////////////
//
// IL2C - A translator for ECMA-335 CIL/MSIL to C language.
// Copyright (c) Kouji Matsui (@kozy_kekyo, @kekyo@mastodon.cloud)
//
// Licensed under Apache-v2: https://opensource.org/licenses/Apache-2.0
//
////////////////////////////////////////////////////////////////////////////

#ifndef System_Runtime_CompilerServices_RuntimeHelpers_H__
#define System_Runtime_CompilerServices_RuntimeHelpers_H__

#pragma once

#include <il2c.h>

#ifdef __cplusplus
extern "C" {
#endif

/////////////////////////////////////////////////////////////
// System.Runtime.CompilerServices.RuntimeHelpers

IL2C_DECLARE_RUNTIME_TYPE(System_Runtime_CompilerServices_RuntimeHelpers);

extern /* static */ int32_t System_Runtime_CompilerServices_RuntimeHelpers_GetHashCode__System_Object(
    System_Object* o);
extern /* static */ void System_Runtime_CompilerServices_RuntimeHelpers_InitializeArray__System_Array_System_RuntimeFieldHandle(
    System_Array* array, System_RuntimeFieldHandle fldHandle);

#ifdef __cplusplus
}
#endif

#endif