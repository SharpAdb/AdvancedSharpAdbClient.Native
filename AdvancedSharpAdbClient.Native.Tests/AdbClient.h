#pragma once
#ifndef _ADBCLIENT_H_
#define _ADBCLIENT_H_
#include "DeviceData.h"
namespace AdvancedSharpAdbClient::dllimport
{
    extern "C"
    {
        __declspec(dllimport) int AdbClientGetAdbVersion();
        __declspec(dllimport) int AdbClientKillAdb();
        __declspec(dllimport) Models::dllimport::DeviceDataArray AdbClientGetDevices();
        __declspec(dllimport) void AdbClientDispose();
    }
}
namespace AdvancedSharpAdbClient
{
    static unsigned int AdbClientRefCounter = 0;
    struct AdbClient
    {
        AdbClient() { AdbClientRefCounter++; }
        ~AdbClient() { if (--AdbClientRefCounter == 0) { dllimport::AdbClientDispose(); } }

        int GetAdbVersion() const { return dllimport::AdbClientGetAdbVersion(); }
        int KillAdb() const { return dllimport::AdbClientKillAdb(); }
        std::vector<Models::DeviceData> GetDevices() const { return Models::GetVectorFromArray(dllimport::AdbClientGetDevices()); }
    };
}
#endif