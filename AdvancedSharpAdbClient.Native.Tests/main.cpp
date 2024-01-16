#include "pch.h"
#include "AdbClient.h"
#include "AdbServer.h"

using namespace winrt;
using namespace Windows::Foundation;
using namespace AdvancedSharpAdbClient;
using namespace AdvancedSharpAdbClient::Models;

int main()
{
    init_apartment();
    //Collections::IIterable
    AdbServer::StartServer(L"C:/Users/qq251/OneDrive/应用/Win32/platform-tools/adb.exe", true);
    auto status = AdbServer::GetStatus();
    printf("%ls\n", status.ToString().c_str());
    {
        AdbClient client;
        auto version = client.GetAdbVersion();
        printf("%d\n", version);
        auto devices = client.GetDevices();
        for (auto& device : devices)
		{
			printf("%ls\n", device.ToString().c_str());
		}
    }
    AdbServer::StopServer();
}
