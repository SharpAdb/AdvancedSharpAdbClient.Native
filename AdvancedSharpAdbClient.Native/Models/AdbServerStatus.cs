// <copyright file="AdbServerStatus.cs" company="The Android Open Source Project, Ryan Conrad, Quamotion, yungd1plomat, wherewhere">
// Copyright (c) The Android Open Source Project, Ryan Conrad, Quamotion, yungd1plomat, wherewhere. All rights reserved.
// </copyright>

using System.Runtime.InteropServices;

namespace AdvancedSharpAdbClient.Native.Models
{
    /// <summary>
    /// Represents the status of the adb server.
    /// </summary>
    /// <param name="isRunning">The value indicating whether the server is currently running.</param>
    /// <param name="version">The version of the server when it is running.</param>
    public struct AdbServerStatus(bool isRunning, Version version)
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AdbServerStatus"/> struct.
        /// </summary>
        public AdbServerStatus() : this(false, default) { }

        /// <summary>
        /// Gets a value indicating whether the server is currently running.
        /// </summary>
        public bool IsRunning = isRunning;

        /// <summary>
        /// Gets the version of the server when it is running.
        /// </summary>
        public Version Version = version;

        /// <summary>
        /// Gets a <see cref="string"/> that represents the current <see cref="AdbServerStatus"/> object.
        /// </summary>
        /// <returns>A <see cref="string"/> that represents the current <see cref="AdbServerStatus"/> object.</returns>
        public override readonly string ToString() =>
            IsRunning ? $"Version {Version} of the adb daemon is running." : "The adb daemon is not running.";

        /// <summary>
        /// Creates a new instance of the <see cref="AdbServerStatus"/> struct based on <see cref="ManagedAdbServerStatus"/>.
        /// </summary>
        /// <param name="deviceData">The <see cref="ManagedAdbServerStatus"/> to convert.</param>
        public static implicit operator AdbServerStatus(ManagedAdbServerStatus serverStatus) => new(serverStatus.IsRunning, serverStatus.Version!);

        /// <summary>
        /// Gets a <see cref="string"/> that represents the current <see cref="AdbServerStatus"/> object.
        /// </summary>
        /// <param name="serverStatus">The <see cref="AdbServerStatus"/> object to convert.</param>
        /// <returns>A <see cref="string"/> that represents the current <see cref="AdbServerStatus"/> object.</returns>
        [UnmanagedCallersOnly(EntryPoint = "AdbServerStatusToString")]
        public static nint ToString(AdbServerStatus serverStatus) => Marshal.StringToHGlobalAuto(serverStatus.ToString());
    }
}
