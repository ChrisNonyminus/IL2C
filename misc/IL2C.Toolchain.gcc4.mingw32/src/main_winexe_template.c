////////////////////////////////////////////////////////////////////////////
//
// IL2C - A translator for ECMA-335 CIL/MSIL to C language.
// Copyright (c) Kouji Matsui (@kozy_kekyo, @kekyo@mastodon.cloud)
//
// Licensed under Apache-v2: https://opensource.org/licenses/Apache-2.0
//
////////////////////////////////////////////////////////////////////////////

#include <il2c.h>
#include <{headerName}>

#ifdef __cplusplus
extern "C"
#endif
int WINAPI WinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, LPSTR lpCmdLine, int nCmdShow)
{
    ((void)hInstance);
    ((void)hPrevInstance);
    ((void)lpCmdLine);
    ((void)nCmdShow);

#if defined(_MSC_VER) && defined(_WIN32) && defined(_DEBUG)
    _crtBreakAlloc = -1;
#endif

    il2c_initialize();

#if {mainIsVoid}
    {mainSymbol}();

    il2c_shutdown();
    return 0;
#else
    const int r = {mainSymbol}();

    il2c_shutdown();
    return r;
#endif
}