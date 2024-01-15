#pragma once
#ifndef _ADBSERVER_H_
#define _ADBSERVER_H_
#include "AdbServerStatus.h"
#include "StartServerResult.h"
namespace AdvancedSharpAdbClient::dllimport
{
    extern "C"
    {
        __declspec(dllimport) Models::StartServerResult AdbServerStartServer(wchar_t*, bool);
        __declspec(dllimport) Models::StartServerResult AdbServerRestartServer();
        __declspec(dllimport) Models::StartServerResult AdbServerRestartServerWithPath(wchar_t*);
        __declspec(dllimport) void AdbServerStopServer();
        __declspec(dllimport) Models::dllimport::AdbServerStatus AdbServerGetStatus();
    }
}
namespace AdvancedSharpAdbClient
{
    struct AdbServer
    {
        static Models::StartServerResult StartServer(std::wstring adbPath, bool restartServerIfNewer = false)
        {
            return dllimport::AdbServerStartServer(const_cast<wchar_t*>(adbPath.c_str()), restartServerIfNewer);
        }

        static Models::StartServerResult RestartServer()
        {
            return dllimport::AdbServerRestartServer();
        }

        static Models::StartServerResult RestartServer(std::wstring adbPath)
        {
            return dllimport::AdbServerRestartServerWithPath(const_cast<wchar_t*>(adbPath.c_str()));
        }

        static void StopServer()
        {
            return dllimport::AdbServerStopServer();
        }

        static Models::AdbServerStatus GetStatus()
        {
            return dllimport::AdbServerGetStatus();
        }
    };
}
#endif