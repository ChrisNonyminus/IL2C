////////////////////////////////////////////////////////////////////////////
//
// IL2C - A translator for ECMA-335 CIL/MSIL to C language.
// Copyright (c) Kouji Matsui (@kozy_kekyo, @kekyo@mastodon.cloud)
//
// Licensed under Apache-v2: https://opensource.org/licenses/Apache-2.0
//
////////////////////////////////////////////////////////////////////////////

/*
 * Original Module Name: ConsoleControl.h
 * Abstract: Abstraction of a Text mode or GOP/UGA screen
 */

/* $FreeBSD: releng/11.1/sys/boot/efi/include/eficonsctl.h 293724 2016-01-12 02:17:39Z smh $ */

#ifndef _EFI_CONS_CTL_H
#define _EFI_CONS_CTL_H

#define EFI_CONSOLE_CONTROL_PROTOCOL_GUID \
    { 0xf42f7782, 0x12e, 0x4c12, {0x99, 0x56, 0x49, 0xf9, 0x43, 0x4, 0xf7, 0x21} }

typedef struct _EFI_CONSOLE_CONTROL_PROTOCOL   EFI_CONSOLE_CONTROL_PROTOCOL;


typedef enum {
  EfiConsoleControlScreenText,
  EfiConsoleControlScreenGraphics,
  EfiConsoleControlScreenMaxValue
} EFI_CONSOLE_CONTROL_SCREEN_MODE;


typedef
EFI_STATUS
(EFIAPI *EFI_CONSOLE_CONTROL_PROTOCOL_GET_MODE) (
  IN  EFI_CONSOLE_CONTROL_PROTOCOL      *This,
  OUT EFI_CONSOLE_CONTROL_SCREEN_MODE   *Mode,
  OUT BOOLEAN                           *GopUgaExists,  OPTIONAL
  OUT BOOLEAN                           *StdInLocked    OPTIONAL
  )
/*++

  Routine Description:
    Return the current video mode information. Also returns info about existence
    of Graphics Output devices or UGA Draw devices in system, and if the Std In
    device is locked. All the arguments are optional and only returned if a non
    NULL pointer is passed in.

  Arguments:
    This         - Protocol instance pointer.
    Mode         - Are we in text of grahics mode.
    GopUgaExists - TRUE if Console Spliter has found a GOP or UGA device
    StdInLocked  - TRUE if StdIn device is keyboard locked

  Returns:
    EFI_SUCCESS     - Mode information returned.

--*/
;


typedef
EFI_STATUS
(EFIAPI *EFI_CONSOLE_CONTROL_PROTOCOL_SET_MODE) (
  IN  EFI_CONSOLE_CONTROL_PROTOCOL      *This,
  IN  EFI_CONSOLE_CONTROL_SCREEN_MODE   Mode
  )
/*++

  Routine Description:
    Set the current mode to either text or graphics. Graphics is
    for Quiet Boot.

  Arguments:
    This  - Protocol instance pointer.
    Mode  - Mode to set the

  Returns:
    EFI_SUCCESS     - Mode information returned.

--*/
;


typedef
EFI_STATUS
(EFIAPI *EFI_CONSOLE_CONTROL_PROTOCOL_LOCK_STD_IN) (
  IN  EFI_CONSOLE_CONTROL_PROTOCOL      *This,
  IN CHAR16                             *Password
  )
/*++

  Routine Description:
    Lock Std In devices until Password is typed.

  Arguments:
    This     - Protocol instance pointer.
    Password - Password needed to unlock screen. NULL means unlock keyboard

  Returns:
    EFI_SUCCESS      - Mode information returned.
    EFI_DEVICE_ERROR - Std In not locked

--*/
;



struct _EFI_CONSOLE_CONTROL_PROTOCOL {
  EFI_CONSOLE_CONTROL_PROTOCOL_GET_MODE           GetMode;
  EFI_CONSOLE_CONTROL_PROTOCOL_SET_MODE           SetMode;
  EFI_CONSOLE_CONTROL_PROTOCOL_LOCK_STD_IN        LockStdIn;
};

extern EFI_GUID gEfiConsoleControlProtocolGuid;

#endif