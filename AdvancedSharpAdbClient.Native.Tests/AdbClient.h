#pragma once
#ifndef _ADBCLIENT_H_
#define _ADBCLIENT_H_
namespace AdvancedSharpAdbClient::dllimport
{
    extern "C"
    {
        __declspec(dllimport) int AdbClientGetAdbVersion();
        __declspec(dllimport) int AdbClientKillAdb();
        __declspec(dllimport) void AdbClientDispose();
    }
}
namespace AdvancedSharpAdbClient
{
    struct AdbClient
    {
        ~AdbClient() { dllimport::AdbClientDispose(); }

        int GetAdbVersion() const { return dllimport::AdbClientGetAdbVersion(); }
        int KillAdb() const { return dllimport::AdbClientKillAdb(); }
    };
}
#endif