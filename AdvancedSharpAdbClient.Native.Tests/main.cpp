#include "pch.h"
#include "functional"
#include "AdbClient.h"
#include "AdbServer.h"

using namespace winrt;
using namespace Windows::Foundation;
using namespace AdvancedSharpAdbClient;
using namespace AdvancedSharpAdbClient::Models;

int main()
{
    init_apartment();
    AdbServer::StartServer(L"C:/Users/qq251/OneDrive/应用/Win32/platform-tools/adb.exe", true);
    printf("%ls\n", AdbServer::GetStatus().ToString().c_str());
    printf("Adb Server Port: %d\n", AdbClient::AdbServerPort());
    printf("Encoding: %d\n", AdbClient::Encoding());
    {
        AdbClient client;
        printf("EndPoint: %ls\n", client.EndPoint().c_str());
        printf("Adb Version: %d\n", client.GetAdbVersion());
        auto devices = client.GetDevices();
        for (auto& device : devices)
        {
            printf("Device: %ls\n", device.ToString().c_str());
            auto features = client.GetFeatureSet(device);
            printf("Feature\n");
            for (auto& feature : features)
            {
                printf("%ls\n", feature.c_str());
            }
        }
    }
    system("pause");
    AdbServer::StopServer();
}
