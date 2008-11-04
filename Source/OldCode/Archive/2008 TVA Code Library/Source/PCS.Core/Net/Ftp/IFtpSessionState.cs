﻿//*******************************************************************************************************
//  ISessionState.cs
//  Copyright © 2008 - TVA, all rights reserved - Gbtc
//
//  Build Environment: C#, Visual Studio 2008
//  Primary Developer: James R Carroll
//      Office: PSO TRAN & REL, CHATTANOOGA - MR 2W-C
//       Phone: 423/751-2827
//       Email: jrcarrol@tva.gov
//
//  Code Modification History:
//  -----------------------------------------------------------------------------------------------------
//  09/23/2008 - James R Carroll
//       Generated original version of source code.
//
//*******************************************************************************************************


namespace PCS.Net.Ftp
{
    internal interface IFtpSessionState
    {
        string Server { get; set; }

        int Port { get; set; }

        FtpDirectory CurrentDirectory { get; set; }

        FtpDirectory RootDirectory { get; }

        FtpControlChannel ControlChannel { get; }

        bool IsBusy { get; }

        void AbortTransfer();

        void Connect(string UserName, string Password);

        void Close();
    }
}
