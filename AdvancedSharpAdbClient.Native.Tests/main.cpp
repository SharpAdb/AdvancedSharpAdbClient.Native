#include "pch.h"
#include "AdbClient.h"
#include "AdbServer.h"

using namespace winrt;
using namespace Windows::Foundation;
using namespace AdvancedSharpAdbClient;

int main()
{
    init_apartment();
    AdbServer::StartServer(L"C:/Users/qq251/OneDrive/应用/Win32/platform-tools/adb.exe", true);
    auto status = AdbServer::GetStatus();
    printf("%ls\n", status.ToString().c_str());
    {
        AdbClient client;
        auto version = client.GetAdbVersion();
        printf("%d\n", version);
    }
    AdbServer::StopServer();
}
