////////////////////////////////////////////////////////////////////////////
//
// IL2C - A translator for ECMA-335 CIL/MSIL to C language.
// Copyright (c) Kouji Matsui (@kozy_kekyo, @kekyo@mastodon.cloud)
//
// Licensed under Apache-v2: https://opensource.org/licenses/Apache-2.0
//
////////////////////////////////////////////////////////////////////////////

#ifndef System_IDisposable_H__
#define System_IDisposable_H__

#pragma once

#include <il2c.h>

#ifdef __cplusplus
extern "C" {
#endif

/////////////////////////////////////////////////////////////
// System.IDisposable

typedef struct System_IDisposable System_IDisposable;

typedef const struct
{
    intptr_t offset__; // Adjustor offset
    void (*Dispose)(void* this__);
} System_IDisposable_VTABLE_DECL__;

struct System_IDisposable
{
    System_IDisposable_VTABLE_DECL__* vptr0__;
};

IL2C_DECLARE_RUNTIME_TYPE(System_IDisposable);

#ifdef __cplusplus
}
#endif

#endif
