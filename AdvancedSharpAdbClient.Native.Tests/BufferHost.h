#pragma once
#ifndef _BUFFERHOST_H_
#define _BUFFERHOST_H_
namespace AdvancedSharpAdbClient::Models
{
    struct _declspec(dllexport) BufferHost
    {
        unsigned char* Buffer;
        int Count;

        operator unsigned char* () const { return Buffer; }
    };
}
#endif