#pragma once
#ifndef _DEVICESTATE_H_
#define _DEVICESTATE_H_
namespace AdvancedSharpAdbClient::Models
{
    /// <summary>
    /// Defines the state of an Android device connected to the Android Debug Bridge.
    /// </summary>
    enum DeviceState
    {
        /// <summary>
        /// The instance is not connected to adb or is not responding.
        /// </summary>
        Offline = 0x00,

        /// <summary>
        /// The device is in bootloader mode
        /// </summary>
        BootLoader = 0x41,

        /// <summary>
        /// The instance is now connected to the adb server. Note that this state does not imply that the Android system is
        /// fully booted and operational, since the instance connects to adb while the system is still booting.
        /// However, after boot-up, this is the normal operational state of an emulator/device instance.
        /// </summary>
        Online = 0x01,

        /// <summary>
        /// The device is the adb host.
        /// </summary>
        Host = 0x81,

        /// <summary>
        /// The device is in recovery mode.
        /// </summary>
        Recovery = 0x11,

        /// <summary>
        /// Insufficient permissions to communicate with the device.
        /// </summary>
        NoPermissions = 0x03,

        /// <summary>
        /// The device is in sideload mode.
        /// </summary>
        Sideload = 0x21,

        /// <summary>
        /// The device is connected to adb, but adb is not authorized for remote debugging of this device.
        /// </summary>
        Unauthorized = 0x09,

        /// <summary>
        /// The device is connected to adb, but adb authorizing for remote debugging of this device.
        /// </summary>
        Authorizing = 0x05,

        /// <summary>
        /// The device state is unknown.
        /// </summary>
        Unknown = -0x01
    };
}
#endif