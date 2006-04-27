#include "stdafx.h"

using namespace System::Reflection;
using namespace System::Runtime::CompilerServices;

//
// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
//
[assembly:AssemblyTitleAttribute("TVA .NET Code Library: Shared Compression")];
[assembly:AssemblyDescriptionAttribute("Shared .NET Compression Functions & Zip File Manipulation Code Library")];
[assembly:AssemblyConfigurationAttribute("")];
[assembly:AssemblyCompanyAttribute("TVA")];
[assembly:AssemblyProductAttribute("Shared .NET Code Library for TVA")];
[assembly:AssemblyCopyrightAttribute("Copyright � 2003, TVA - All rights reserved.  This .NET library uses \"zlib\" compression code written by Jean-loup Gailly and Mark Adler, Copyright � 1995-2002, and general zip file format manipulation code written by Gilles Vollant, Copyright � 1998-2003")];
[assembly:AssemblyTrademarkAttribute("Authors: James Ritchie Carroll (.NET wrapper), Jean-loup Gailly and Mark Adler (zlib), Gilles Vollant (zip/unzip)")];
[assembly:AssemblyCultureAttribute("")];		

//
// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the value or you can default the Revision and Build Numbers 
// by using the '*' as shown below:

[assembly:AssemblyVersion("2.0.25.42881")];

//
// In order to sign your assembly you must specify a key to use. Refer to the 
// Microsoft .NET Framework documentation for more information on assembly signing.
//
// Use the attributes below to control which key is used for signing. 
//
// Notes: 
//   (*) If no key is specified, the assembly is not signed.
//   (*) KeyName refers to a key that has been installed in the Crypto Service
//       Provider (CSP) on your machine. KeyFile refers to a file which contains
//       a key.
//   (*) If the KeyFile and the KeyName values are both specified, the 
//       following processing occurs:
//       (1) If the KeyName can be found in the CSP, that key is used.
//       (2) If the KeyName does not exist and the KeyFile does exist, the key 
//           in the KeyFile is installed into the CSP and used.
//   (*) In order to create a KeyFile, you can use the sn.exe (Strong Name) utility.
//        When specifying the KeyFile, the location of the KeyFile should be
//        relative to the project directory.
//   (*) Delay Signing is an advanced option - see the Microsoft .NET Framework
//       documentation for more information on this.
//
[assembly:AssemblyDelaySignAttribute(false)];
[assembly:AssemblyKeyFileAttribute("")];
[assembly:AssemblyKeyNameAttribute("")];