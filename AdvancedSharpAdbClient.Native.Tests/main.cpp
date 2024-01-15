#include "pch.h"
#include "AdbServer.h"

using namespace winrt;
using namespace Windows::Foundation;
using namespace AdvancedSharpAdbClient;

int main()
{
    init_apartment();
    AdbServer::StartServer(L"C:/Users/qq251/OneDrive/应用/Win32/platform-tools/adb.exe", true);
    auto status = AdbServer::GetStatus();
    printf("%ls", status.ToString().c_str());
    AdbServer::StopServer();
}
