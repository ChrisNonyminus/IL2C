////////////////////////////////////////////////////////////////////////////
//
// IL2C - A translator for ECMA-335 CIL/MSIL to C language.
// Copyright (c) Kouji Matsui (@kozy_kekyo, @kekyo@mastodon.cloud)
//
// Licensed under Apache-v2: https://opensource.org/licenses/Apache-2.0
//
////////////////////////////////////////////////////////////////////////////

#ifndef System_Threading_ThreadStart_H__
#define System_Threading_ThreadStart_H__

#pragma once

#include <il2c.h>

#ifdef __cplusplus
extern "C" {
#endif

/////////////////////////////////////////////////////////////
// System.Threading.ThreadStart

typedef System_MulticastDelegate System_Threading_ThreadStart;

typedef System_MulticastDelegate_VTABLE_DECL__ System_Threading_ThreadStart_VTABLE_DECL__;

#define System_Threading_ThreadStart_VTABLE__ System_MulticastDelegate_VTABLE__

IL2C_DECLARE_RUNTIME_TYPE(System_Threading_ThreadStart);

extern /* public sealed */ void System_Threading_ThreadStart_Invoke(System_Threading_ThreadStart* this__);

#ifdef __cplusplus
}
#endif

#endif
