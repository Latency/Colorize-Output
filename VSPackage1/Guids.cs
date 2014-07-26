// Guids.cs
// MUST match guids.h
using System;

namespace CodePlexProjectHostingforOpenSourceSoftware.VSPackage1
{
    static class GuidList
    {
        public const string guidVSPackage1PkgString = "ed830165-6b60-4706-9feb-feb48ee279d5";
        public const string guidVSPackage1CmdSetString = "9d35db7f-4339-4689-a8fb-92e11e9c0866";

        public static readonly Guid guidVSPackage1CmdSet = new Guid(guidVSPackage1CmdSetString);
    };
}