#pragma once
#ifndef _DEVICEDATA_H_
#define _DEVICEDATA_H_
#include "DeviceState.h"
namespace AdvancedSharpAdbClient::Models::dllimport
{
    struct _declspec(dllexport) DeviceData
    {
        /// <summary>
        /// Gets or sets the device serial number.
        /// </summary>
        wchar_t* Serial;

        /// <summary>
        /// Gets or sets the device state.
        /// </summary>
        DeviceState State;

        /// <summary>
        /// Gets or sets the device model name.
        /// </summary>
        wchar_t* Model;

        /// <summary>
        /// Gets or sets the device product name.
        /// </summary>
        wchar_t* Product;

        /// <summary>
        /// Gets or sets the device name.
        /// </summary>
        wchar_t* Name;

        /// <summary>
        /// Gets or sets the features available on the device.
        /// </summary>
        wchar_t* Features;

        /// <summary>
        /// Gets or sets the USB port to which this device is connected. Usually available on Linux only.
        /// </summary>
        wchar_t* Usb;

        /// <summary>
        /// Gets or sets the transport ID for this device.
        /// </summary>
        wchar_t* TransportId;

        /// <summary>
        /// Gets or sets the device info message. Currently only seen for NoPermissions state.
        /// </summary>
        wchar_t* Message;
    };

    struct _declspec(dllexport) DeviceDataArray
    {
        DeviceData* Devices;
        int Count;
    };

    extern "C"
    {
        __declspec(dllimport) DeviceData DeviceDataCreateFromAdbData(wchar_t*);
        __declspec(dllimport) DeviceState DeviceDataGetStateFromString(wchar_t*);
        __declspec(dllimport) wchar_t* DeviceDataToString(DeviceData);
    }
}
namespace AdvancedSharpAdbClient::Models
{
    struct DeviceData
    {
        DeviceData() = default;
        DeviceData(dllimport::DeviceData device) { data = device; }

        std::wstring Serial() const { return data.Serial; }
        void Serial(std::wstring value) { data.Serial = const_cast<wchar_t*>(value.c_str()); }

        DeviceState State() const { return data.State; }
        void State(DeviceState value) { data.State = value; }

        std::wstring Model() const { return data.Model; }
        void Model(std::wstring value) { data.Model = const_cast<wchar_t*>(value.c_str()); }

        std::wstring Product() const { return data.Product; }
        void Product(std::wstring value) { data.Product = const_cast<wchar_t*>(value.c_str()); }

        std::wstring Name() const { return data.Name; }
        void Name(std::wstring value) { data.Name = const_cast<wchar_t*>(value.c_str()); }

        std::wstring Features() const { return data.Features; }
        void Features(std::wstring value) { data.Features = const_cast<wchar_t*>(value.c_str()); }

        std::wstring Usb() const { return data.Usb; }
        void Usb(std::wstring value) { data.Usb = const_cast<wchar_t*>(value.c_str()); }

        std::wstring TransportId() const { return data.TransportId; }
        void TransportId(std::wstring value) { data.TransportId = const_cast<wchar_t*>(value.c_str()); }

        std::wstring Message() const { return data.Message; }
        void Message(std::wstring value) { data.Message = const_cast<wchar_t*>(value.c_str()); }

        std::wstring ToString() const { return dllimport::DeviceDataToString(data); }

        static DeviceData CreateFromAdbData(std::wstring data) { return dllimport::DeviceDataCreateFromAdbData(const_cast<wchar_t*>(data.c_str())); }
        static DeviceState GetStateFromString(std::wstring data) { return dllimport::DeviceDataGetStateFromString(const_cast<wchar_t*>(data.c_str())); }

        operator dllimport::DeviceData() const { return data; }
        operator dllimport::DeviceData* () { return &data; }
    private:
        dllimport::DeviceData data;
    };

    inline std::vector<DeviceData> GetVectorFromArray(dllimport::DeviceDataArray array)
    {
        std::vector<DeviceData> list;
        for (int i = 0; i < array.Count; i++)
        {
            list.push_back(array.Devices[i]);
        }
        return list;
    }
}
#endif