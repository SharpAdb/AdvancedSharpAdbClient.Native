using AdvancedSharpAdbClient.Native.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;

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
        /// Gets default encoding.
        /// </summary>
        [UnmanagedCallersOnly(EntryPoint = "AdbClientGetEncoding")]
        public static int GetEncoding() => ManagedAdbClient.Encoding.CodePage;

        /// <summary>
        /// Sets default encoding.
        /// </summary>
        [UnmanagedCallersOnly(EntryPoint = "AdbClientSetEncoding")]
        public static void SetEncoding(int value) =>
            ManagedAdbClient.Encoding = Encoding.GetEncoding(value);

        /// <summary>
        /// Gets the current port at which the Android Debug Bridge server listens.
        /// </summary>
        [UnmanagedCallersOnly(EntryPoint = "AdbClientGetAdbServerPort")]
        public static int GetAdbServerPort() => ManagedAdbClient.AdbServerPort;

        /// <summary>
        /// Gets the Default <see cref="EndPoint"/> at which the adb server is listening.
        /// </summary>
        [UnmanagedCallersOnly(EntryPoint = "AdbClientGetDefaultEndPoint")]
        public static nint GetDefaultEndPoint() =>
            Marshal.StringToHGlobalAuto(ManagedAdbClient.DefaultEndPoint.ToString());

        /// <summary>
        /// Gets the <see cref="EndPoint"/> at which the Android Debug Bridge server is listening.
        /// </summary>
        [UnmanagedCallersOnly(EntryPoint = "AdbClientGetEndPoint")]
        public static nint GetEndPoint() => Marshal.StringToHGlobalAuto(Instance.EndPoint.ToString());

        /// <summary>
        /// Create an ASCII string preceded by four hex digits. The opening "####"
        /// is the length of the rest of the string, encoded as ASCII hex(case
        /// doesn't matter).
        /// </summary>
        /// <param name="req">The request to form.</param>
        /// <returns>An array containing <c>####req</c>.</returns>
        [UnmanagedCallersOnly(EntryPoint = "AdbClientFormAdbRequest")]
        public static BufferHost FormAdbRequest(nint req) =>
            ManagedAdbClient.FormAdbRequest(Marshal.PtrToStringAuto(req)!);

        /// <summary>
        /// Creates the adb forward request.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <param name="port">The port.</param>
        /// <returns>This returns an array containing <c>"####tcp:{port}:{addStr}"</c>.</returns>
        [UnmanagedCallersOnly(EntryPoint = "AdbClientCreateAdbForwardRequest")]
        public static BufferHost CreateAdbForwardRequest(nint address, int port) =>
            ManagedAdbClient.CreateAdbForwardRequest(Marshal.PtrToStringAuto(address)!, port);

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
        public static ArrayHost<DeviceData> GetDevices() =>
            Instance.GetDevices().Select<ManagedDeviceData, DeviceData>(x => x).ToArray();

        /// <summary>
        /// Asks the ADB server to forward local connections from <paramref name="local"/>
        /// to the <paramref name="remote"/> address on the <paramref name="device"/>.
        /// </summary>
        /// <param name="device">The device on which to forward the connections.</param>
        /// <param name="local">
        /// <para>The local address to forward. This value can be in one of:</para>
        /// <list type="ordered">
        ///   <item>
        ///     <c>tcp:&lt;port&gt;</c>: TCP connection on localhost:&lt;port&gt;
        ///   </item>
        ///   <item>
        ///     <c>local:&lt;path&gt;</c>: Unix local domain socket on &lt;path&gt;
        ///   </item>
        /// </list>
        /// </param>
        /// <param name="remote">
        /// <para>The remote address to forward. This value can be in one of:</para>
        /// <list type="ordered">
        ///   <item>
        ///     <c>tcp:&lt;port&gt;</c>: TCP connection on localhost:&lt;port&gt; on device
        ///   </item>
        ///   <item>
        ///     <c>local:&lt;path&gt;</c>: Unix local domain socket on &lt;path&gt; on device
        ///   </item>
        ///   <item>
        ///     <c>jdwp:&lt;pid&gt;</c>: JDWP thread on VM process &lt;pid&gt; on device.
        ///   </item>
        /// </list>
        /// </param>
        /// <param name="allowRebind">If set to <see langword="true"/>, the request will fail if there is already a forward
        /// connection from <paramref name="local"/>.</param>
        /// <returns>If your requested to start forwarding to local port TCP:0, the port number of the TCP port
        /// which has been opened. In all other cases, <c>0</c>.</returns>
        [UnmanagedCallersOnly(EntryPoint = "AdbClientCreateForward")]
        public static int CreateForward(DeviceData device, nint local, nint remote, bool allowRebind) =>
            Instance.CreateForward(device, Marshal.PtrToStringAuto(local)!, Marshal.PtrToStringAuto(remote)!, allowRebind);

        /// <summary>
        /// Asks the ADB server to reverse forward local connections from <paramref name="remote"/>
        /// to the <paramref name="local"/> address on the <paramref name="device"/>.
        /// </summary>
        /// <param name="device">The device on which to reverse forward the connections.</param>
        /// <param name="remote">
        /// <para>The remote address to reverse forward. This value can be in one of:</para>
        /// <list type="ordered">
        ///   <item>
        ///     <c>tcp:&lt;port&gt;</c>: TCP connection on localhost:&lt;port&gt; on device
        ///   </item>
        ///   <item>
        ///     <c>local:&lt;path&gt;</c>: Unix local domain socket on &lt;path&gt; on device
        ///   </item>
        ///   <item>
        ///     <c>jdwp:&lt;pid&gt;</c>: JDWP thread on VM process &lt;pid&gt; on device.
        ///   </item>
        /// </list>
        /// </param>
        /// <param name="local">
        /// <para>The local address to reverse forward. This value can be in one of:</para>
        /// <list type="ordered">
        ///   <item>
        ///     <c>tcp:&lt;port&gt;</c>: TCP connection on localhost:&lt;port&gt;
        ///   </item>
        ///   <item>
        ///     <c>local:&lt;path&gt;</c>: Unix local domain socket on &lt;path&gt;
        ///   </item>
        /// </list>
        /// </param>
        /// <param name="allowRebind">If set to <see langword="true"/>, the request will fail if if the specified socket
        /// is already bound through a previous reverse command.</param>
        /// <returns>If your requested to start reverse to remote port TCP:0, the port number of the TCP port
        /// which has been opened. In all other cases, <c>0</c>.</returns>
        [UnmanagedCallersOnly(EntryPoint = "AdbClientCreateReverseForward")]
        public static int CreateReverseForward(DeviceData device, nint remote, nint local, bool allowRebind) =>
            Instance.CreateReverseForward(device, Marshal.PtrToStringAuto(remote)!, Marshal.PtrToStringAuto(local)!, allowRebind);

        /// <summary>
        /// Remove a reverse port forwarding between a remote and a local port.
        /// </summary>
        /// <param name="device">The device on which to remove the reverse port forwarding</param>
        /// <param name="remote">Specification of the remote that was forwarded</param>
        [UnmanagedCallersOnly(EntryPoint = "AdbClientRemoveReverseForward")]
        public static void RemoveReverseForward(DeviceData device, nint remote) =>
            Instance.RemoveReverseForward(device, Marshal.PtrToStringAuto(remote)!);

        /// <summary>
        /// Removes all reverse forwards for a given device.
        /// </summary>
        /// <param name="device">The device on which to remove all reverse port forwarding</param>
        [UnmanagedCallersOnly(EntryPoint = "AdbClientRemoveAllReverseForwards")]
        public static void RemoveAllReverseForwards(DeviceData device) =>
            Instance.RemoveAllReverseForwards(device);

        /// <summary>
        /// Remove a port forwarding between a local and a remote port.
        /// </summary>
        /// <param name="device">The device on which to remove the port forwarding.</param>
        /// <param name="localPort">Specification of the local port that was forwarded.</param>
        [UnmanagedCallersOnly(EntryPoint = "AdbClientRemoveForward")]
        public static void RemoveForward(DeviceData device, int localPort) =>
            Instance.RemoveForward(device, localPort);

        /// <summary>
        /// Removes all forwards for a given device.
        /// </summary>
        /// <param name="device">The device on which to remove the port forwarding.</param>
        [UnmanagedCallersOnly(EntryPoint = "AdbClientRemoveAllForwards")]
        public static void RemoveAllForwards(DeviceData device) =>
            Instance.RemoveAllForwards(device);

        /// <summary>
        /// Executes a command on the adb server.
        /// </summary>
        /// <param name="target">The target of command, such as <c>shell</c>, <c>remount</c>, <c>dev</c>, <c>tcp</c>, <c>local</c>,
        /// <c>localreserved</c>, <c>localabstract</c>, <c>jdwp</c>, <c>track-jdwp</c>, <c>sync</c>, <c>reverse</c> and so on.</param>
        /// <param name="command">The command to execute.</param>
        [UnmanagedCallersOnly(EntryPoint = "AdbClientExecuteServerCommand")]
        public static void ExecuteServerCommand(nint target, nint command) =>
            Instance.ExecuteServerCommand(Marshal.PtrToStringAuto(target)!, Marshal.PtrToStringAuto(command)!);

        /// <summary>
        /// Executes a shell command on the device.
        /// </summary>
        /// <param name="command">The command to execute.</param>
        /// <param name="device">The device on which to run the command.</param>
        [UnmanagedCallersOnly(EntryPoint = "AdbClientExecuteRemoteCommand")]
        public static void ExecuteRemoteCommand(nint command, DeviceData device) =>
            Instance.ExecuteRemoteCommand(Marshal.PtrToStringAuto(command)!, device);

        /// <summary>
        /// Executes a command on the adb server.
        /// </summary>
        /// <param name="target">The target of command, such as <c>shell</c>, <c>remount</c>, <c>dev</c>, <c>tcp</c>, <c>local</c>,
        /// <c>localreserved</c>, <c>localabstract</c>, <c>jdwp</c>, <c>track-jdwp</c>, <c>sync</c>, <c>reverse</c> and so on.</param>
        /// <param name="command">The command to execute.</param>
        /// <param name="predicate">Optionally, a <see cref="delegate*{String, Boolean}"/> that processes the command output.</param>
        /// <param name="encoding">The encoding to use when parsing the command output.</param>
        [UnmanagedCallersOnly(EntryPoint = "AdbClientExecuteServerCommandWithPredicate")]
        public static unsafe void ExecuteServerCommand(nint target, nint command, delegate*<nint, bool> predicate, int encoding) =>
            Instance.ExecuteServerCommand(Marshal.PtrToStringAuto(target)!, Marshal.PtrToStringAuto(command)!, x => predicate(Marshal.StringToHGlobalAuto(x)), Encoding.GetEncoding(encoding));

        /// <summary>
        /// Executes a shell command on the device.
        /// </summary>
        /// <param name="command">The command to execute.</param>
        /// <param name="device">The device on which to run the command.</param>
        /// <param name="predicate">Optionally, a <see cref="delegate*{String, Boolean}"/> that processes the command output.</param>
        /// <param name="encoding">The encoding to use when parsing the command output.</param>
        [UnmanagedCallersOnly(EntryPoint = "AdbClientExecuteRemoteCommandWithPredicate")]
        public static unsafe void ExecuteRemoteCommand(nint command, DeviceData device, delegate*<nint, bool> predicate, int encoding) =>
            Instance.ExecuteRemoteCommand(Marshal.PtrToStringAuto(command)!, device, x => predicate(Marshal.StringToHGlobalAuto(x)), Encoding.GetEncoding(encoding));

        /// <summary>
        /// Executes a command on the adb server and returns the output.
        /// </summary>
        /// <param name="target">The target of command, such as <c>shell</c>, <c>remount</c>, <c>dev</c>, <c>tcp</c>, <c>local</c>,
        /// <c>localreserved</c>, <c>localabstract</c>, <c>jdwp</c>, <c>track-jdwp</c>, <c>sync</c>, <c>reverse</c> and so on.</param>
        /// <param name="command">The command to execute.</param>
        /// <param name="encoding">The encoding to use when parsing the command output.</param>
        /// <returns>A <see cref="IEnumerable{String}"/> of strings, each representing a line of output from the command.</returns>
        [UnmanagedCallersOnly(EntryPoint = "AdbClientExecuteServerCommandWithOutput")]
        public static unsafe nint ExecuteServerCommand(nint target, nint command, int encoding) =>
            Marshal.UnsafeAddrOfPinnedArrayElement(Instance.ExecuteServerCommand(Marshal.PtrToStringAuto(target)!, Marshal.PtrToStringAuto(command)!, Encoding.GetEncoding(encoding)).Select(Marshal.StringToHGlobalAuto).ToArray(), 0);

        /// <summary>
        /// Executes a shell command on the device and returns the output.
        /// </summary>
        /// <param name="command">The command to execute.</param>
        /// <param name="device">The device on which to run the command.</param>
        /// <param name="encoding">The encoding to use when parsing the command output.</param>
        /// <returns>A <see cref="IEnumerable{String}"/> of strings, each representing a line of output from the command.</returns>
        [UnmanagedCallersOnly(EntryPoint = "AdbClientExecuteRemoteCommandWithOutput")]
        public static unsafe nint ExecuteRemoteCommand(nint command, DeviceData device, int encoding) =>
            Marshal.UnsafeAddrOfPinnedArrayElement(Instance.ExecuteRemoteCommand(Marshal.PtrToStringAuto(command)!, device, Encoding.GetEncoding(encoding)).Select(Marshal.StringToHGlobalAuto).ToArray(), 0);

        /// <summary>
        /// Lists all features supported by the current device.
        /// </summary>
        /// <param name="device">The device for which to get the list of features supported.</param>
        /// <returns>A list of all features supported by the current device.</returns>
        [UnmanagedCallersOnly(EntryPoint = "AdbClientGetFeatureSet")]
        public static unsafe nint GetFeatureSet(DeviceData device) =>
            Marshal.UnsafeAddrOfPinnedArrayElement(Instance.GetFeatureSet(device).Select(Marshal.StringToHGlobalAuto).ToArray(), 0);

        [UnmanagedCallersOnly(EntryPoint = "AdbClientDispose")]
        public static void Dispose() => instance = null;
    }
}
