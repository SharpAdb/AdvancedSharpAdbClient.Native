using AdvancedSharpAdbClient.Native.Models;
using System.Linq;
using System.Runtime.InteropServices;

namespace AdvancedSharpAdbClient.Native
{
    public static class AdbClient
    {
        private static IAdbClient? instance;
        public static IAdbClient Instance
        {
            get
            {
                instance ??= new ManagedAdbClient();
                return instance;
            }
        }

        /// <summary>
        /// Ask the ADB server for its internal version number.
        /// </summary>
        /// <returns>The ADB version number.</returns>
        [UnmanagedCallersOnly(EntryPoint = "AdbClientGetAdbVersion")]
        public static int GetAdbVersion() => Instance.GetAdbVersion();

        /// <summary>
        /// Ask the ADB server to quit immediately. This is used when the
        /// ADB client detects that an obsolete server is running after an
        /// upgrade.
        /// </summary>
        [UnmanagedCallersOnly(EntryPoint = "AdbClientKillAdb")]
        public static void KillAdb() => Instance.KillAdb();

        /// <summary>
        /// Gets the devices that are available for communication.
        /// </summary>
        /// <returns>A list of devices that are connected.</returns>
        /// <example>
        /// <para>The following example list all Android devices that are currently connected to this PC:</para>
        /// <code>
        /// var devices = new AdbClient().GetDevices();
        /// foreach (var device in devices)
        /// {
        ///     Console.WriteLine(device.Name);
        /// }
        /// </code>
        /// </example>
        [UnmanagedCallersOnly(EntryPoint = "AdbClientGetDevices")]
        public static ArrayHost<DeviceData> GetDevices() => Instance.GetDevices().Select<ManagedDeviceData, DeviceData>(x => x).ToArray();

        [UnmanagedCallersOnly(EntryPoint = "AdbClientDispose")]
        public static void Dispose() => instance = null;
    }
}
