#pragma once
#ifndef _ADBCLIENT_H_
#define _ADBCLIENT_H_
#include "ArrayHost.h"
#include "BufferHost.h"
#include "DeviceData.h"
namespace AdvancedSharpAdbClient::dllimport
{
    extern "C"
    {
        __declspec(dllimport) int AdbClientGetEncoding();
        __declspec(dllimport) void AdbClientSetEncoding(int);
        __declspec(dllimport) int AdbClientGetAdbServerPort();
        __declspec(dllimport) wchar_t* AdbClientGetDefaultEndPoint();
        __declspec(dllimport) wchar_t* AdbClientGetEndPoint();
        __declspec(dllimport) Models::BufferHost AdbClientFormAdbRequest(wchar_t*);
        __declspec(dllimport) Models::BufferHost AdbClientCreateAdbForwardRequest(wchar_t*, int);
        __declspec(dllimport) int AdbClientGetAdbVersion();
        __declspec(dllimport) int AdbClientKillAdb();
        __declspec(dllimport) Models::ArrayHost<Models::dllimport::DeviceData> AdbClientGetDevices();
        __declspec(dllimport) int AdbClientCreateForward(Models::dllimport::DeviceData, wchar_t*, wchar_t*, bool);
        __declspec(dllimport) int AdbClientCreateReverseForward(Models::dllimport::DeviceData, wchar_t*, wchar_t*, bool);
        __declspec(dllimport) int AdbClientRemoveReverseForward(Models::dllimport::DeviceData, wchar_t*);
        __declspec(dllimport) int AdbClientRemoveAllReverseForwards(Models::dllimport::DeviceData);
        __declspec(dllimport) int AdbClientRemoveForward(Models::dllimport::DeviceData, int);
        __declspec(dllimport) int AdbClientRemoveAllForwards(Models::dllimport::DeviceData);
        __declspec(dllimport) void AdbClientExecuteServerCommand(wchar_t*, wchar_t*);
        __declspec(dllimport) void AdbClientExecuteRemoteCommand(wchar_t*, Models::dllimport::DeviceData);
        __declspec(dllimport) void AdbClientExecuteServerCommandWithPredicate(wchar_t*, wchar_t*, bool(*)(wchar_t*), int);
        __declspec(dllimport) void AdbClientExecuteRemoteCommandWithPredicate(wchar_t*, Models::dllimport::DeviceData, bool(*)(wchar_t*), int);
        __declspec(dllimport) wchar_t** AdbClientExecuteServerCommandWithOutput(wchar_t*, wchar_t*, int);
        __declspec(dllimport) wchar_t** AdbClientExecuteRemoteCommandWithOutput(wchar_t*, Models::dllimport::DeviceData, int);
        __declspec(dllimport) wchar_t** AdbClientGetFeatureSet(Models::dllimport::DeviceData);
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

        static int Encoding() { return dllimport::AdbClientGetEncoding(); }
        static void Encoding(int value) { dllimport::AdbClientSetEncoding(value); }

        static int AdbServerPort() { return dllimport::AdbClientGetAdbServerPort(); }

        static std::wstring DefaultEndPoint() { return dllimport::AdbClientGetDefaultEndPoint(); }

        std::wstring EndPoint() const { return dllimport::AdbClientGetEndPoint(); }

        static Models::BufferHost FormAdbRequest(std::wstring req) { return dllimport::AdbClientFormAdbRequest(const_cast<wchar_t*>(req.c_str())); }
        static Models::BufferHost CreateAdbForwardRequest(std::wstring address, int port) { return dllimport::AdbClientCreateAdbForwardRequest(const_cast<wchar_t*>(address.c_str()), port); }

        int GetAdbVersion() const { return dllimport::AdbClientGetAdbVersion(); }
        int KillAdb() const { return dllimport::AdbClientKillAdb(); }
        std::vector<Models::DeviceData> GetDevices() const { return Models::GetVectorFromArray(dllimport::AdbClientGetDevices()); }
        int CreateForward(Models::DeviceData device, std::wstring local, std::wstring remote, bool allowRebind) const { return dllimport::AdbClientCreateForward(device, const_cast<wchar_t*>(local.c_str()), const_cast<wchar_t*>(remote.c_str()), allowRebind); }
        int CreateReverseForward(Models::DeviceData device, std::wstring remote, std::wstring local, bool allowRebind) const { return dllimport::AdbClientCreateReverseForward(device, const_cast<wchar_t*>(remote.c_str()), const_cast<wchar_t*>(local.c_str()), allowRebind); }
        int RemoveReverseForward(Models::DeviceData device, std::wstring remote) const { return dllimport::AdbClientRemoveReverseForward(device, const_cast<wchar_t*>(remote.c_str())); }
        int RemoveAllReverseForwards(Models::DeviceData device) const { return dllimport::AdbClientRemoveAllReverseForwards(device); }
        int RemoveForward(Models::DeviceData device, int localPort) const { return dllimport::AdbClientRemoveForward(device, localPort); }
        int RemoveAllForwards(Models::DeviceData device) const { return dllimport::AdbClientRemoveAllForwards(device); }
        void ExecuteServerCommand(std::wstring target, std::wstring command) const { dllimport::AdbClientExecuteServerCommand(const_cast<wchar_t*>(target.c_str()), const_cast<wchar_t*>(command.c_str())); }
        void ExecuteRemoteCommand(std::wstring command, Models::DeviceData device) const { dllimport::AdbClientExecuteRemoteCommand(const_cast<wchar_t*>(command.c_str()), device); }
        void ExecuteServerCommand(std::wstring target, std::wstring command, std::function<bool(std::wstring)> predicate, int encoding) const { dllimport::AdbClientExecuteServerCommandWithPredicate(const_cast<wchar_t*>(target.c_str()), const_cast<wchar_t*>(command.c_str()), GetPredicate([&](wchar_t* line) { return predicate(line); }), encoding); }
        void ExecuteRemoteCommand(std::wstring command, Models::DeviceData device, std::function<bool(std::wstring)> predicate, int encoding) const { dllimport::AdbClientExecuteRemoteCommandWithPredicate(const_cast<wchar_t*>(command.c_str()), device, GetPredicate([&](wchar_t* line) { return predicate(line); }), encoding); }
        std::vector<std::wstring> ExecuteServerCommand(std::wstring target, std::wstring command, int encoding) const { return GetStringVector(dllimport::AdbClientExecuteServerCommandWithOutput(const_cast<wchar_t*>(target.c_str()), const_cast<wchar_t*>(command.c_str()), encoding)); }
        std::vector<std::wstring> ExecuteRemoteCommand(std::wstring command, Models::DeviceData device, int encoding) const { return GetStringVector(dllimport::AdbClientExecuteRemoteCommandWithOutput(const_cast<wchar_t*>(command.c_str()), device, encoding)); }
        std::vector<std::wstring> GetFeatureSet(Models::DeviceData device) const { return GetStringVector(dllimport::AdbClientGetFeatureSet(device)); }

    private:
        using Predicate = bool(*)(wchar_t*);
        template<typename F>
        static Predicate GetPredicate(F predicate)
        {
            static auto function = predicate;
            return [](wchar_t* line) { return function(line); };
        }

        static std::vector<std::wstring> GetStringVector(wchar_t** array)
        {
            std::vector<std::wstring> result;
            for (int i = 0; array[i] != nullptr; i++)
            {
                result.push_back(array[i]);
            }
            return result;
        }
    };
}
#endif