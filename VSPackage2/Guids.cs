// Guids.cs
// MUST match guids.h
using System;

namespace OpenSourceCommunity.VSPackage2
{
    static class GuidList
    {
        public const string guidVSPackage2PkgString = "cb7c12b1-a065-4224-9c78-eda8c113b5a2";
        public const string guidVSPackage2CmdSetString = "fada70f8-2b6d-4fc9-a8ee-d4ad712c309e";

        public static readonly Guid guidVSPackage2CmdSet = new Guid(guidVSPackage2CmdSetString);
    };
}