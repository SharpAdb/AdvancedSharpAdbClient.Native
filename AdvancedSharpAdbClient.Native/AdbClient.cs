using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

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

        [UnmanagedCallersOnly(EntryPoint = "AdbClientDispose")]
        public static void Dispose() => instance = null;
    }
}
