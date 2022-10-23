#ifndef _IL2C_PLATFORM_OTHER_H
#define _IL2C_PLATFORM_OTHER_H
#pragma once

#ifdef __cplusplus
extern "C" {
#endif
#include <stdint.h>


///////////////////////////////////////////////////
// 3DS
#if defined(__3DS__)

#include <3ds.h>
#define IL2C_USE_ITOW

#define IL2C_MCALLOC_THRESHOLD 32U
#include "heap.h"
#define IL2C_MCALLOC_THRESHOLD 32U


#include "gcc.h"
#include "strings.h"
#define IL2C_NO_THREADING
#include "no-threading.h"


extern void il2c_sleep(uint32_t milliseconds);

#endif
///////////////////////////////////////////////////
	

#ifdef __cplusplus
}
#endif

#endif
