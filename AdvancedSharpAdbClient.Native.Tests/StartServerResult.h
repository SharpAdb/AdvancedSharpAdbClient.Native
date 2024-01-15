#pragma once
#ifndef _STARTSERVERRESULT_H_
#define _STARTSERVERRESULT_H_
namespace AdvancedSharpAdbClient::Models
{
    /// <summary>
    /// Gives information about a AdbServer.StartServer(string, bool) operation.
    /// </summary>
    enum StartServerResult
    {
        /// <summary>
        /// The adb server was already running. The server was not restarted.
        /// </summary>
        AlreadyRunning = 0b011,

        /// <summary>
        /// The adb server was running, but was running an outdated version of adb.
        /// The server was stopped and a newer version of the server was started.
        /// </summary>
        RestartedOutdatedDaemon = 0b101,

        /// <summary>
        /// The adb server was not running, and a new instance of the adb server was started.
        /// </summary>
        Started = 0b001,

        /// <summary>
        /// An <see cref="IAdbServer.StartServer(string, bool)"/> operation is already in progress.
        /// </summary>
        Starting = 0b000
    };
}
#endif