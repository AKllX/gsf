﻿//******************************************************************************************************
//  Program.cs - Gbtc
//
//  Copyright © 2014, Grid Protection Alliance.  All Rights Reserved.
//
//  Licensed to the Grid Protection Alliance (GPA) under one or more contributor license agreements. See
//  the NOTICE file distributed with this work for additional information regarding copyright ownership.
//  The GPA licenses this file to you under the MIT License (MIT), the "License"; you may
//  not use this file except in compliance with the License. You may obtain a copy of the License at:
//
//      http://www.opensource.org/licenses/MIT
//
//  Unless agreed to in writing, the subject software distributed under the License is distributed on an
//  "AS-IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. Refer to the
//  License for the specific language governing permissions and limitations.
//
//  Code Modification History:
//  ----------------------------------------------------------------------------------------------------
//  02/10/2014 - Stephen C. Wills
//       Generated original version of source code.
//  04/29/2015 - J. Ritchie Carroll
//       Added report e-mailing options.
//
//******************************************************************************************************

using System;
using System.IO;
using System.Windows.Forms;
using GSF.Console;
using GSF.IO;
using GSF.Net.Smtp;

namespace StatHistorianReportGenerator
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Arguments args;
            string arg;

            if (Environment.GetCommandLineArgs().Length > 1)
            {
                args = new Arguments(Environment.CommandLine, true);

                if (TryGetValue(args, "reportType", out arg) && !string.IsNullOrWhiteSpace(arg))
                {
                    arg = arg.Trim();

                    if (arg.Equals("completeness", StringComparison.OrdinalIgnoreCase))
                        GenerateCompletenessReport();
                    else if (arg.Equals("correctness", StringComparison.OrdinalIgnoreCase))
                        GenerateCorrectnessReport();
                }
                else
                {
                    // Generate completeness report by default
                    GenerateCompletenessReport();
                }
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Main());
            }
        }

        private static void GenerateCompletenessReport()
        {
            CompletenessReportGenerator completenessReportGenerator = new CompletenessReportGenerator();
            Arguments args = new Arguments(Environment.CommandLine, true);
            string arg;

            string reportLocation = "";
            string reportFileName = "";
            DateTime reportDate = DateTime.UtcNow;
            double threshold;

            if (TryGetValue(args, "archiveLocation", out arg))
                completenessReportGenerator.ArchiveLocation = arg;

            if (TryGetValue(args, "reportLocation", out arg))
                reportLocation = arg;

            if (TryGetValue(args, "reportFileName", out arg))
                reportFileName = arg;

            if (TryGetValue(args, "title", out arg))
                completenessReportGenerator.TitleText = arg;

            if (TryGetValue(args, "company", out arg))
                completenessReportGenerator.CompanyText = arg;

            if (TryGetValue(args, "reportDate", out arg) && DateTime.TryParse(arg, out reportDate))
            {
                if (reportDate.Kind == DateTimeKind.Unspecified)
                    reportDate = new DateTime(reportDate.Ticks, DateTimeKind.Utc);

                completenessReportGenerator.ReportDate = reportDate;
            }

            if (TryGetValue(args, "level4Threshold", out arg) && double.TryParse(arg, out threshold))
                completenessReportGenerator.Level4Threshold = threshold;

            if (TryGetValue(args, "level3Threshold", out arg) && double.TryParse(arg, out threshold))
                completenessReportGenerator.Level3Threshold = threshold;

            if (TryGetValue(args, "level4Alias", out arg))
                completenessReportGenerator.Level4Alias = arg;

            if (TryGetValue(args, "level3Alias", out arg))
                completenessReportGenerator.Level3Alias = arg;

            if (string.IsNullOrEmpty(reportFileName))
                reportFileName = string.Format("{0} {1:yyyy-MM-dd}.pdf", completenessReportGenerator.TitleText, completenessReportGenerator.ReportDate);

            reportLocation = FilePath.GetAbsolutePath(reportLocation);

            if (!Directory.Exists(reportLocation))
                Directory.CreateDirectory(reportLocation);

            string reportFilePath = Path.Combine(reportLocation, reportFileName);

            // Generate PDF report
            completenessReportGenerator.GenerateReport().Save(reportFilePath);

            // E-mail PDF report if parameters were provided
            EmailReport(args, string.Format("{0} {1} for {2:MMMM dd, yyyy}", completenessReportGenerator.CompanyText, completenessReportGenerator.TitleText, reportDate), reportFilePath);
        }

        private static void GenerateCorrectnessReport()
        {
            CorrectnessReportGenerator correctnessReportGenerator = new CorrectnessReportGenerator();
            Arguments args = new Arguments(Environment.CommandLine, true);
            string arg;

            string reportLocation = "";
            string reportFileName = "";
            DateTime reportDate = DateTime.UtcNow;

            if (TryGetValue(args, "archiveLocation", out arg))
                correctnessReportGenerator.ArchiveLocation = arg;

            if (TryGetValue(args, "reportLocation", out arg))
                reportLocation = arg;

            if (TryGetValue(args, "reportFileName", out arg))
                reportFileName = arg;

            if (TryGetValue(args, "title", out arg))
                correctnessReportGenerator.TitleText = arg;

            if (TryGetValue(args, "company", out arg))
                correctnessReportGenerator.CompanyText = arg;

            if (TryGetValue(args, "reportDate", out arg) && DateTime.TryParse(arg, out reportDate))
            {
                if (reportDate.Kind == DateTimeKind.Unspecified)
                    reportDate = new DateTime(reportDate.Ticks, DateTimeKind.Utc);

                correctnessReportGenerator.ReportDate = reportDate;
            }

            if (string.IsNullOrEmpty(reportFileName))
                reportFileName = string.Format("{0} {1:yyyy-MM-dd}.pdf", correctnessReportGenerator.TitleText, correctnessReportGenerator.ReportDate);

            reportLocation = FilePath.GetAbsolutePath(reportLocation);

            if (!Directory.Exists(reportLocation))
                Directory.CreateDirectory(reportLocation);

            string reportFilePath = Path.Combine(reportLocation, reportFileName);

            // Generate PDF report
            correctnessReportGenerator.GenerateReport().Save(reportFilePath);

            // E-mail PDF report if parameters were provided
            EmailReport(args, string.Format("{0} {1} for {2:MMMM dd, yyyy}", correctnessReportGenerator.CompanyText, correctnessReportGenerator.TitleText, reportDate), reportFilePath);
        }

        private static void EmailReport(Arguments args, string subject, string reportFilePath)
        {
            string smtpServer, fromAddress, toAddresses;

            if (TryGetValue(args, "smtpServer", out smtpServer) &&
                TryGetValue(args, "fromAddress", out fromAddress) &&
                TryGetValue(args, "toAddresses", out toAddresses) &&
                smtpServer.Length > 0 &&
                fromAddress.Length > 0 &&
                toAddresses.Length > 0)
            {
                using (Mail message = new Mail(fromAddress, toAddresses, smtpServer))
                {
                    message.Subject = subject;
                    message.Attachments = reportFilePath;
                    message.IsBodyHtml = true;
                    message.Body = string.Format(
                        "<div>\r\n" +
                        "The attached report requires a portable document format (PDF) reader, such as the " +
                        "<a href=\"http://get.adobe.com/reader/\">Adobe Acrobat PDF Reader</a>.\r\n" +
                        "</div>\r\n" +
                        "<br><br>\r\n" +
                        "<div>\r\n" +
                        "<i>E-mail generated at {0:yyyy-MM-dd HH:mm:ss.fff} UTC.</i>\r\n" +
                        "</div>",
                        DateTime.UtcNow);

                    message.Send();
                }
            }
        }

        private static bool TryGetValue(Arguments args, string arg, out string value)
        {
            value = args[arg];

            if ((object)value != null)
            {
                value = value.Trim();
                return true;
            }

            return false;
        }
    }
}
