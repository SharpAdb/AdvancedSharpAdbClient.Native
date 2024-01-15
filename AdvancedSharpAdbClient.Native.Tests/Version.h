#pragma once
#ifndef _VERSION_H_
#define _VERSION_H_
namespace AdvancedSharpAdbClient::Models::dllimport
{
    struct _declspec(dllexport) Version
    {
        int Major;

        int Minor;

        int Build;

        int Revision;
    };

    extern "C"
    {
        __declspec(dllimport) wchar_t* VersionToString(Version);
        __declspec(dllimport) wchar_t* VersionToFormattedString(Version, int);
    }
}
namespace AdvancedSharpAdbClient::Models
{
    struct Version
    {
        Version() = default;
        Version(dllimport::Version status)
        {
            data = status;
        }

        bool Major() const { return data.Major; }
        void Major(int value) { data.Major = value; }

        bool Minor() const { return data.Minor; }
        void Minor(int value) { data.Minor = value; }

        bool Build() const { return data.Build; }
        void Build(int value) { data.Build = value; }

        bool Revision() const { return data.Revision; }
        void Revision(int value) { data.Revision = value; }

        std::wstring ToString() const { return dllimport::VersionToString(data); }
        std::wstring ToString(int significance) const { return dllimport::VersionToFormattedString(data, significance); }

        operator dllimport::Version() const { return data; }
        operator dllimport::Version* () { return &data; }
    private:
        dllimport::Version data;
    };
}
#endif