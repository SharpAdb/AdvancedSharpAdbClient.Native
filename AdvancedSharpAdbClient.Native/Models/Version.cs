using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace AdvancedSharpAdbClient.Native.Models
{
    public struct Version(int major, int minor, int build, int revision)
    {
        public int Major = major;

        public int Minor = minor;

        public int Build = build;

        public int Revision = revision;

        public override readonly string ToString() => ToString(4);

        /// <summary>
        /// Returns a string representation of a version with the format 'Major.Minor.Build.Revision'.
        /// </summary>
        /// <param name="significance">The number of version numbers to return, default is 4 for the full version number.</param>
        /// <returns>Version string of the format 'Major.Minor.Build.Revision'</returns>
        public readonly string ToString(int significance)
        {
            return significance switch
            {
                4 => Join(Major, Minor, Build, Revision),
                3 => Join(Major, Minor, Build),
                2 => Join(Major, Minor),
                1 => Join(Major),
                _ => throw new ArgumentOutOfRangeException(nameof(significance), "Value must be a value 1 through 4."),
            };
            static string Join(params int[] array) => string.Join('.', array.Where(x => x != -1));
        }

        /// <summary>
        /// Creates a new instance of the <see cref="Version"/> struct based on <see cref="ManagedVersion"/>.
        /// </summary>
        /// <param name="deviceData">The <see cref="ManagedVersion"/> to convert.</param>
        public static implicit operator Version(ManagedVersion version) =>
            new(version.Major, version.Minor, version.Build, version.Revision);

        [UnmanagedCallersOnly(EntryPoint = "VersionToString")]
        public static nint ToString(Version version) => Marshal.StringToHGlobalAuto(version.ToString());

        [UnmanagedCallersOnly(EntryPoint = "VersionToFormattedString")]
        public static nint ToString(Version version, int significance) => Marshal.StringToHGlobalAuto(version.ToString(significance));
    }
}
