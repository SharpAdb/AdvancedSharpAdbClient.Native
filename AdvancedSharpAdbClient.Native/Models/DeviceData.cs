using AdvancedSharpAdbClient.Models;
using System.Runtime.InteropServices;

namespace AdvancedSharpAdbClient.Native.Models
{
    /// <summary>
    /// Represents a device that is connected to the Android Debug Bridge.
    /// </summary>
    public struct DeviceData
    {
        /// <summary>
        /// Gets or sets the device serial number.
        /// </summary>
        public nint Serial;

        /// <summary>
        /// Gets or sets the device state.
        /// </summary>
        public DeviceState State;

        /// <summary>
        /// Gets or sets the device model name.
        /// </summary>
        public nint Model;

        /// <summary>
        /// Gets or sets the device product name.
        /// </summary>
        public nint Product;

        /// <summary>
        /// Gets or sets the device name.
        /// </summary>
        public nint Name;

        /// <summary>
        /// Gets or sets the features available on the device.
        /// </summary>
        public nint Features;

        /// <summary>
        /// Gets or sets the USB port to which this device is connected. Usually available on Linux only.
        /// </summary>
        public nint Usb;

        /// <summary>
        /// Gets or sets the transport ID for this device.
        /// </summary>
        public nint TransportId;

        /// <summary>
        /// Gets or sets the device info message. Currently only seen for NoPermissions state.
        /// </summary>
        public nint Message;

        /// <inheritdoc/>
        public override readonly string? ToString() => Marshal.PtrToStringAuto(Serial);

        /// <summary>
        /// Creates a new instance of the <see cref="DeviceData"/> class based on
        /// data retrieved from the Android Debug Bridge.
        /// </summary>
        /// <param name="data">The data retrieved from the Android Debug Bridge that represents a device.</param>
        /// <returns>A <see cref="DeviceData"/> object that represents the device.</returns>
        [UnmanagedCallersOnly(EntryPoint = "DeviceDataCreateFromAdbData")]
        public static DeviceData CreateFromAdbData(nint data) =>
            new ManagedDeviceData(Marshal.PtrToStringAuto(data)!);

        /// <summary>
        /// Gets the device state from the string value.
        /// </summary>
        /// <param name="state">The device state string.</param>
        /// <returns>The device state.</returns>
        [UnmanagedCallersOnly(EntryPoint = "DeviceDataGetStateFromString")]
        public static DeviceState GetStateFromString(nint state) =>
            ManagedDeviceData.GetStateFromString(Marshal.PtrToStringAuto(state)!);

        /// <summary>
        /// Creates a new instance of the <see cref="DeviceData"/> struct based on <see cref="ManagedDeviceData"/>.
        /// </summary>
        /// <param name="deviceData">The <see cref="ManagedDeviceData"/> to convert.</param>
        public static implicit operator DeviceData(ManagedDeviceData deviceData) => new()
        {
            Serial = Marshal.StringToHGlobalAuto(deviceData.Serial),
            State = deviceData.State,
            Model = Marshal.StringToHGlobalAuto(deviceData.Model),
            Product = Marshal.StringToHGlobalAuto(deviceData.Product),
            Name = Marshal.StringToHGlobalAuto(deviceData.Name),
            Features = Marshal.StringToHGlobalAuto(deviceData.Features),
            Usb = Marshal.StringToHGlobalAuto(deviceData.Usb),
            TransportId = Marshal.StringToHGlobalAuto(deviceData.TransportId),
            Message = Marshal.StringToHGlobalAuto(deviceData.Message)
        };

        /// <summary>
        /// Creates a new instance of the <see cref="ManagedDeviceData"/> struct based on <see cref="DeviceData"/>.
        /// </summary>
        /// <param name="deviceData">The <see cref="DeviceData"/> to convert.</param>
        public static implicit operator ManagedDeviceData(DeviceData deviceData) => new()
        {
            Serial = Marshal.PtrToStringAuto(deviceData.Serial)!,
            State = deviceData.State,
            Model = Marshal.PtrToStringAuto(deviceData.Model)!,
            Product = Marshal.PtrToStringAuto(deviceData.Product)!,
            Name = Marshal.PtrToStringAuto(deviceData.Name)!,
            Features = Marshal.PtrToStringAuto(deviceData.Features)!,
            Usb = Marshal.PtrToStringAuto(deviceData.Usb)!,
            TransportId = Marshal.PtrToStringAuto(deviceData.TransportId)!,
            Message = Marshal.PtrToStringAuto(deviceData.Message)!
        };

        /// <summary>
        /// Gets a <see cref="string"/> that represents the current <see cref="DeviceData"/> object.
        /// </summary>
        /// <param name="deviceData">The <see cref="DeviceData"/> object to convert.</param>
        /// <returns>A <see cref="string"/> that represents the current <see cref="DeviceData"/> object.</returns>
        [UnmanagedCallersOnly(EntryPoint = "DeviceDataToString")]
        public static nint ToString(DeviceData deviceData) => deviceData.Serial;
    }
}
