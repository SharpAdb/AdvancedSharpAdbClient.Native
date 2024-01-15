#pragma once
#ifndef _ADBSERVERSTATUS_H_
#define _ADBSERVERSTATUS_H_
#include "Version.h"
namespace AdvancedSharpAdbClient::Models::dllimport
{
    /// <summary>
    /// Represents the status of the adb server.
    /// </summary>
    struct _declspec(dllexport) AdbServerStatus
    {
        /// <summary>
        /// Gets a value indicating whether the server is currently running.
        /// </summary>
        bool IsRunning;

        /// <summary>
        /// Gets the version of the server when it is running.
        /// </summary>
        Version Version;
    };

    extern "C"
    {
        __declspec(dllimport) wchar_t* AdbServerStatusToString(AdbServerStatus);
    }
}
namespace AdvancedSharpAdbClient::Models
{
    /// <summary>
    /// Represents the status of the adb server.
    /// </summary>
    struct AdbServerStatus
    {
        AdbServerStatus() = default;
        AdbServerStatus(dllimport::AdbServerStatus status)
        {
            data = status;
        }

        bool IsRunning() const { return data.IsRunning; }
        void IsRunning(bool value) { data.IsRunning = value; }

        Version IsRunning() { return data.Version; }
        void IsRunning(Version value) { data.Version = value; }

        std::wstring ToString() const { return dllimport::AdbServerStatusToString(data); }

        operator dllimport::AdbServerStatus() const { return data; }
        operator dllimport::AdbServerStatus* () { return &data; }
    private:
        dllimport::AdbServerStatus data;
    };
}
#endif