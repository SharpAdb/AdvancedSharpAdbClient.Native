#pragma once
#ifndef _ARRAYHOST_H_
#define _ARRAYHOST_H_
namespace AdvancedSharpAdbClient::Models
{
    template <typename T>
    struct _declspec(dllexport) ArrayHost
    {
        T* Array;
        int Count;

        operator T* () const { return Array; }
    };
}
#endif