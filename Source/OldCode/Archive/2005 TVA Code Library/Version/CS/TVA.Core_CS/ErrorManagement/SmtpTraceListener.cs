using System.Diagnostics;
using System.Linq;
using System.Data;
using System.Collections;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System;
using System.Text;
using TVA.Net.Smtp;

//*******************************************************************************************************
//  TVA.ErrorManagement.SmtpTraceListener.vb - Defines an e-mail based trace listener
//  Copyright © 2006 - TVA, all rights reserved - Gbtc
//
//  Build Environment: VB.NET, Visual Studio 2005
//  Primary Developer: Pinal C. Patel, Operations Data Architecture [TVA]
//      Office: COO - TRNS/PWR ELEC SYS O, CHATTANOOGA, TN - MR 2W-C
//       Phone: 423/751-2250
//       Email: pcpatel@tva.gov
//
//  Code Modification History:
//  -----------------------------------------------------------------------------------------------------
//  03/18/2005 - Pinal C. Patel
//       Generated original version of source code.
//  01/04/2006 - Pinal C. Patel
//       Migrated 2.0 version of source code from 1.1 source (TVA.ErrorManagement.SmtpTraceListener).
//  09/13/2007 - Darrell Zuercher
//       Edited code comments.
//
//*******************************************************************************************************


namespace TVA
{
    namespace ErrorManagement
    {

        /// <summary>Defines an e-mail based trace listener.</summary>
        public class SmtpTraceListener : TraceListener
        {



            private string m_sender;
            private string m_recipient;
            private string m_smtpServer;

            public SmtpTraceListener()
            {


            }

            public SmtpTraceListener(string initializationData)
            {


                string[] smtpData = initializationData.Split(",");

                if (smtpData.Length < 3)
                {
                    // Insufficient initialization data was provided.
                    throw (new ArgumentException("Insufficient initialization data provided for Smtp.TraceListner. Initialization data must be provided in the following format: \'sender@email.com, recipient@email.com, smtp.email.com\'."));
                }

                // Initializes private variables.
                m_sender = smtpData[0];
                m_recipient = smtpData[1];
                m_smtpServer = smtpData[2];

            }

            public override void Write(string message)
            {

                StringBuilder messageBuilder = new StringBuilder();
                messageBuilder.Append(message);
                // Appends standard information to the bottom of the message.
                messageBuilder.Append(Environment.NewLine + Environment.NewLine);
                messageBuilder.Append("This trace message was sent from the machine ");
                messageBuilder.Append(System.Net.Dns.GetHostName());
                messageBuilder.Append(" (");
                messageBuilder.Append(System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList(0).ToString());
                messageBuilder.Append(") at ");
                messageBuilder.Append(DateTime.Now);

                SimpleMailMessage mailMessage = new SimpleMailMessage();
                mailMessage.Sender = m_sender;
                mailMessage.Recipients = m_recipient;
                mailMessage.Subject = "Trace message for " + System.AppDomain.CurrentDomain.FriendlyName;
                mailMessage.Body = messageBuilder.ToString();
                mailMessage.Send();

            }

            public override void WriteLine(string message)
            {

                // Emails the trace message.
                Write(message);

            }

        }

    }
}
