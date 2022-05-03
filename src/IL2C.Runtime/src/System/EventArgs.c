////////////////////////////////////////////////////////////////////////////
//
// IL2C - A translator for ECMA-335 CIL/MSIL to C language.
// Copyright (c) Kouji Matsui (@kozy_kekyo, @kekyo@mastodon.cloud)
//
// Licensed under Apache-v2: https://opensource.org/licenses/Apache-2.0
//
////////////////////////////////////////////////////////////////////////////

#include "il2c_private.h"

/////////////////////////////////////////////////////////////
// System.EventArgs

/////////////////////////////////////////////////
// VTable and runtime type info declarations

IL2C_RUNTIME_TYPE_BEGIN(
    System_EventArgs,
    "System.EventArgs",
    IL2C_TYPE_REFERENCE,
    sizeof(System_EventArgs),
    System_Object,
    0, 0)
IL2C_RUNTIME_TYPE_END();