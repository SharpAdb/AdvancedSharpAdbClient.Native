#pragma once
#ifndef _ARRAYHOST_H_
#define _ARRAYHOST_H_
namespace AdvancedSharpAdbClient::Models
{
    template <class T>
    struct _declspec(dllexport) ArrayHost
    {
        T* Array;
        int Count;
    };
}
#endif