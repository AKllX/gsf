﻿//*******************************************************************************************************
//  AssemblyInfo.cs - Gbtc
//
//  Tennessee Valley Authority, 2009
//  No copyright is claimed pursuant to 17 USC § 105.  All Other Rights Reserved.
//
//  This software is made freely available under the TVA Open Source Agreement (see below).
//
//  Code Modification History:
//  -----------------------------------------------------------------------------------------------------
//  04/29/2005 - Pinal C. Patel
//       Generated original version of source code.
//  12/29/2005 - Pinal C. Patel
//       Migrated 2.0 version of source code from 1.1 source (TVA.Shared.Assembly).
//  12/12/2007 - Darrell Zuercher
//       Edited Code Comments.
//  09/08/2008 - J. Ritchie Carroll
//       Converted to C# as AssemblyInformation.
//  09/14/2009 - Stephen C. Wills
//       Added new header and license agreement.
//  10/21/2009 - Pinal C. Patel
//       Added error checking to assembly attribute properties.
//
//*******************************************************************************************************

#region [ TVA Open Source Agreement ]
/*

 THIS OPEN SOURCE AGREEMENT ("AGREEMENT") DEFINES THE RIGHTS OF USE,REPRODUCTION, DISTRIBUTION,
 MODIFICATION AND REDISTRIBUTION OF CERTAIN COMPUTER SOFTWARE ORIGINALLY RELEASED BY THE
 TENNESSEE VALLEY AUTHORITY, A CORPORATE AGENCY AND INSTRUMENTALITY OF THE UNITED STATES GOVERNMENT
 ("GOVERNMENT AGENCY"). GOVERNMENT AGENCY IS AN INTENDED THIRD-PARTY BENEFICIARY OF ALL SUBSEQUENT
 DISTRIBUTIONS OR REDISTRIBUTIONS OF THE SUBJECT SOFTWARE. ANYONE WHO USES, REPRODUCES, DISTRIBUTES,
 MODIFIES OR REDISTRIBUTES THE SUBJECT SOFTWARE, AS DEFINED HEREIN, OR ANY PART THEREOF, IS, BY THAT
 ACTION, ACCEPTING IN FULL THE RESPONSIBILITIES AND OBLIGATIONS CONTAINED IN THIS AGREEMENT.

 Original Software Designation: openPDC
 Original Software Title: The TVA Open Source Phasor Data Concentrator
 User Registration Requested. Please Visit https://naspi.tva.com/Registration/
 Point of Contact for Original Software: J. Ritchie Carroll <mailto:jrcarrol@tva.gov>

 1. DEFINITIONS

 A. "Contributor" means Government Agency, as the developer of the Original Software, and any entity
 that makes a Modification.

 B. "Covered Patents" mean patent claims licensable by a Contributor that are necessarily infringed by
 the use or sale of its Modification alone or when combined with the Subject Software.

 C. "Display" means the showing of a copy of the Subject Software, either directly or by means of an
 image, or any other device.

 D. "Distribution" means conveyance or transfer of the Subject Software, regardless of means, to
 another.

 E. "Larger Work" means computer software that combines Subject Software, or portions thereof, with
 software separate from the Subject Software that is not governed by the terms of this Agreement.

 F. "Modification" means any alteration of, including addition to or deletion from, the substance or
 structure of either the Original Software or Subject Software, and includes derivative works, as that
 term is defined in the Copyright Statute, 17 USC § 101. However, the act of including Subject Software
 as part of a Larger Work does not in and of itself constitute a Modification.

 G. "Original Software" means the computer software first released under this Agreement by Government
 Agency entitled openPDC, including source code, object code and accompanying documentation, if any.

 H. "Recipient" means anyone who acquires the Subject Software under this Agreement, including all
 Contributors.

 I. "Redistribution" means Distribution of the Subject Software after a Modification has been made.

 J. "Reproduction" means the making of a counterpart, image or copy of the Subject Software.

 K. "Sale" means the exchange of the Subject Software for money or equivalent value.

 L. "Subject Software" means the Original Software, Modifications, or any respective parts thereof.

 M. "Use" means the application or employment of the Subject Software for any purpose.

 2. GRANT OF RIGHTS

 A. Under Non-Patent Rights: Subject to the terms and conditions of this Agreement, each Contributor,
 with respect to its own contribution to the Subject Software, hereby grants to each Recipient a
 non-exclusive, world-wide, royalty-free license to engage in the following activities pertaining to
 the Subject Software:

 1. Use

 2. Distribution

 3. Reproduction

 4. Modification

 5. Redistribution

 6. Display

 B. Under Patent Rights: Subject to the terms and conditions of this Agreement, each Contributor, with
 respect to its own contribution to the Subject Software, hereby grants to each Recipient under Covered
 Patents a non-exclusive, world-wide, royalty-free license to engage in the following activities
 pertaining to the Subject Software:

 1. Use

 2. Distribution

 3. Reproduction

 4. Sale

 5. Offer for Sale

 C. The rights granted under Paragraph B. also apply to the combination of a Contributor's Modification
 and the Subject Software if, at the time the Modification is added by the Contributor, the addition of
 such Modification causes the combination to be covered by the Covered Patents. It does not apply to
 any other combinations that include a Modification. 

 D. The rights granted in Paragraphs A. and B. allow the Recipient to sublicense those same rights.
 Such sublicense must be under the same terms and conditions of this Agreement.

 3. OBLIGATIONS OF RECIPIENT

 A. Distribution or Redistribution of the Subject Software must be made under this Agreement except for
 additions covered under paragraph 3H. 

 1. Whenever a Recipient distributes or redistributes the Subject Software, a copy of this Agreement
 must be included with each copy of the Subject Software; and

 2. If Recipient distributes or redistributes the Subject Software in any form other than source code,
 Recipient must also make the source code freely available, and must provide with each copy of the
 Subject Software information on how to obtain the source code in a reasonable manner on or through a
 medium customarily used for software exchange.

 B. Each Recipient must ensure that the following copyright notice appears prominently in the Subject
 Software:

          No copyright is claimed pursuant to 17 USC § 105.  All Other Rights Reserved.

 C. Each Contributor must characterize its alteration of the Subject Software as a Modification and
 must identify itself as the originator of its Modification in a manner that reasonably allows
 subsequent Recipients to identify the originator of the Modification. In fulfillment of these
 requirements, Contributor must include a file (e.g., a change log file) that describes the alterations
 made and the date of the alterations, identifies Contributor as originator of the alterations, and
 consents to characterization of the alterations as a Modification, for example, by including a
 statement that the Modification is derived, directly or indirectly, from Original Software provided by
 Government Agency. Once consent is granted, it may not thereafter be revoked.

 D. A Contributor may add its own copyright notice to the Subject Software. Once a copyright notice has
 been added to the Subject Software, a Recipient may not remove it without the express permission of
 the Contributor who added the notice.

 E. A Recipient may not make any representation in the Subject Software or in any promotional,
 advertising or other material that may be construed as an endorsement by Government Agency or by any
 prior Recipient of any product or service provided by Recipient, or that may seek to obtain commercial
 advantage by the fact of Government Agency's or a prior Recipient's participation in this Agreement.

 F. In an effort to track usage and maintain accurate records of the Subject Software, each Recipient,
 upon receipt of the Subject Software, is requested to register with Government Agency by visiting the
 following website: https://naspi.tva.com/Registration/. Recipient's name and personal information
 shall be used for statistical purposes only. Once a Recipient makes a Modification available, it is
 requested that the Recipient inform Government Agency at the web site provided above how to access the
 Modification.

 G. Each Contributor represents that that its Modification does not violate any existing agreements,
 regulations, statutes or rules, and further that Contributor has sufficient rights to grant the rights
 conveyed by this Agreement.

 H. A Recipient may choose to offer, and to charge a fee for, warranty, support, indemnity and/or
 liability obligations to one or more other Recipients of the Subject Software. A Recipient may do so,
 however, only on its own behalf and not on behalf of Government Agency or any other Recipient. Such a
 Recipient must make it absolutely clear that any such warranty, support, indemnity and/or liability
 obligation is offered by that Recipient alone. Further, such Recipient agrees to indemnify Government
 Agency and every other Recipient for any liability incurred by them as a result of warranty, support,
 indemnity and/or liability offered by such Recipient.

 I. A Recipient may create a Larger Work by combining Subject Software with separate software not
 governed by the terms of this agreement and distribute the Larger Work as a single product. In such
 case, the Recipient must make sure Subject Software, or portions thereof, included in the Larger Work
 is subject to this Agreement.

 J. Notwithstanding any provisions contained herein, Recipient is hereby put on notice that export of
 any goods or technical data from the United States may require some form of export license from the
 U.S. Government. Failure to obtain necessary export licenses may result in criminal liability under
 U.S. laws. Government Agency neither represents that a license shall not be required nor that, if
 required, it shall be issued. Nothing granted herein provides any such export license.

 4. DISCLAIMER OF WARRANTIES AND LIABILITIES; WAIVER AND INDEMNIFICATION

 A. No Warranty: THE SUBJECT SOFTWARE IS PROVIDED "AS IS" WITHOUT ANY WARRANTY OF ANY KIND, EITHER
 EXPRESSED, IMPLIED, OR STATUTORY, INCLUDING, BUT NOT LIMITED TO, ANY WARRANTY THAT THE SUBJECT
 SOFTWARE WILL CONFORM TO SPECIFICATIONS, ANY IMPLIED WARRANTIES OF MERCHANTABILITY, FITNESS FOR A
 PARTICULAR PURPOSE, OR FREEDOM FROM INFRINGEMENT, ANY WARRANTY THAT THE SUBJECT SOFTWARE WILL BE ERROR
 FREE, OR ANY WARRANTY THAT DOCUMENTATION, IF PROVIDED, WILL CONFORM TO THE SUBJECT SOFTWARE. THIS
 AGREEMENT DOES NOT, IN ANY MANNER, CONSTITUTE AN ENDORSEMENT BY GOVERNMENT AGENCY OR ANY PRIOR
 RECIPIENT OF ANY RESULTS, RESULTING DESIGNS, HARDWARE, SOFTWARE PRODUCTS OR ANY OTHER APPLICATIONS
 RESULTING FROM USE OF THE SUBJECT SOFTWARE. FURTHER, GOVERNMENT AGENCY DISCLAIMS ALL WARRANTIES AND
 LIABILITIES REGARDING THIRD-PARTY SOFTWARE, IF PRESENT IN THE ORIGINAL SOFTWARE, AND DISTRIBUTES IT
 "AS IS."

 B. Waiver and Indemnity: RECIPIENT AGREES TO WAIVE ANY AND ALL CLAIMS AGAINST GOVERNMENT AGENCY, ITS
 AGENTS, EMPLOYEES, CONTRACTORS AND SUBCONTRACTORS, AS WELL AS ANY PRIOR RECIPIENT. IF RECIPIENT'S USE
 OF THE SUBJECT SOFTWARE RESULTS IN ANY LIABILITIES, DEMANDS, DAMAGES, EXPENSES OR LOSSES ARISING FROM
 SUCH USE, INCLUDING ANY DAMAGES FROM PRODUCTS BASED ON, OR RESULTING FROM, RECIPIENT'S USE OF THE
 SUBJECT SOFTWARE, RECIPIENT SHALL INDEMNIFY AND HOLD HARMLESS  GOVERNMENT AGENCY, ITS AGENTS,
 EMPLOYEES, CONTRACTORS AND SUBCONTRACTORS, AS WELL AS ANY PRIOR RECIPIENT, TO THE EXTENT PERMITTED BY
 LAW.  THE FOREGOING RELEASE AND INDEMNIFICATION SHALL APPLY EVEN IF THE LIABILITIES, DEMANDS, DAMAGES,
 EXPENSES OR LOSSES ARE CAUSED, OCCASIONED, OR CONTRIBUTED TO BY THE NEGLIGENCE, SOLE OR CONCURRENT, OF
 GOVERNMENT AGENCY OR ANY PRIOR RECIPIENT.  RECIPIENT'S SOLE REMEDY FOR ANY SUCH MATTER SHALL BE THE
 IMMEDIATE, UNILATERAL TERMINATION OF THIS AGREEMENT.

 5. GENERAL TERMS

 A. Termination: This Agreement and the rights granted hereunder will terminate automatically if a
 Recipient fails to comply with these terms and conditions, and fails to cure such noncompliance within
 thirty (30) days of becoming aware of such noncompliance. Upon termination, a Recipient agrees to
 immediately cease use and distribution of the Subject Software. All sublicenses to the Subject
 Software properly granted by the breaching Recipient shall survive any such termination of this
 Agreement.

 B. Severability: If any provision of this Agreement is invalid or unenforceable under applicable law,
 it shall not affect the validity or enforceability of the remainder of the terms of this Agreement.

 C. Applicable Law: This Agreement shall be subject to United States federal law only for all purposes,
 including, but not limited to, determining the validity of this Agreement, the meaning of its
 provisions and the rights, obligations and remedies of the parties.

 D. Entire Understanding: This Agreement constitutes the entire understanding and agreement of the
 parties relating to release of the Subject Software and may not be superseded, modified or amended
 except by further written agreement duly executed by the parties.

 E. Binding Authority: By accepting and using the Subject Software under this Agreement, a Recipient
 affirms its authority to bind the Recipient to all terms and conditions of this Agreement and that
 Recipient hereby agrees to all terms and conditions herein.

 F. Point of Contact: Any Recipient contact with Government Agency is to be directed to the designated
 representative as follows: J. Ritchie Carroll <mailto:jrcarrol@tva.gov>.

*/
#endregion

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using TVA.IO;

namespace TVA.Reflection
{
    /// <summary>Assembly Information Class.</summary>
    public class AssemblyInfo
    {
        #region [ Members ]

        // Fields
        private Assembly m_assemblyInstance;

        #endregion

        #region [ Constructors ]

        /// <summary>Initializes a new instance of the <see cref="AssemblyInfo"/> class.</summary>
        /// <param name="assemblyInstance">An <see cref="Assembly"/> object.</param>
        public AssemblyInfo(Assembly assemblyInstance)
        {
            m_assemblyInstance = assemblyInstance;
        }

        #endregion

        #region [ Properties ]

        /// <summary>Gets the title information of the assembly.</summary>
        /// <returns>The title information of the assembly.</returns>
        public string Title
        {
            get
            {
                AssemblyTitleAttribute attribute = GetCustomAttribute(typeof(AssemblyTitleAttribute)) as AssemblyTitleAttribute;
                if (attribute == null)
                    return string.Empty;
                else
                    return attribute.Title;
            }
        }

        /// <summary>Gets the description information of the assembly.</summary>
        /// <returns>The description information of the assembly.</returns>
        public string Description
        {
            get
            {
                AssemblyDescriptionAttribute attribute = GetCustomAttribute(typeof(AssemblyDescriptionAttribute)) as AssemblyDescriptionAttribute;
                if (attribute == null)
                    return string.Empty;
                else
                    return attribute.Description;
            }
        }

        /// <summary>Gets the company name information of the assembly.</summary>
        /// <returns>The company name information of the assembly.</returns>
        public string Company
        {
            get
            {
                AssemblyCompanyAttribute attribute = GetCustomAttribute(typeof(AssemblyCompanyAttribute)) as AssemblyCompanyAttribute;
                if (attribute == null)
                    return string.Empty;
                else
                    return attribute.Company;
            }
        }

        /// <summary>Gets the product name information of the assembly.</summary>
        /// <returns>The product name information of the assembly.</returns>
        public string Product
        {
            get
            {
                AssemblyProductAttribute attribute = GetCustomAttribute(typeof(AssemblyProductAttribute)) as AssemblyProductAttribute;
                if (attribute == null)
                    return string.Empty;
                else
                    return attribute.Product;
            }
        }

        /// <summary>Gets the copyright information of the assembly.</summary>
        /// <returns>The copyright information of the assembly.</returns>
        public string Copyright
        {
            get
            {
                AssemblyCopyrightAttribute attribute = GetCustomAttribute(typeof(AssemblyCopyrightAttribute)) as AssemblyCopyrightAttribute;
                if (attribute == null)
                    return string.Empty;
                else
                    return attribute.Copyright;
            }
        }

        /// <summary>Gets the trademark information of the assembly.</summary>
        /// <returns>The trademark information of the assembly.</returns>
        public string Trademark
        {
            get
            {
                AssemblyTrademarkAttribute attribute = GetCustomAttribute(typeof(AssemblyTrademarkAttribute)) as AssemblyTrademarkAttribute;
                if (attribute == null)
                    return string.Empty;
                else
                    return attribute.Trademark;
            }
        }

        /// <summary>Gets the configuration information of the assembly.</summary>
        /// <returns>The configuration information of the assembly.</returns>
        public string Configuration
        {
            get
            {
                AssemblyConfigurationAttribute attribute = GetCustomAttribute(typeof(AssemblyConfigurationAttribute)) as AssemblyConfigurationAttribute;
                if (attribute == null)
                    return string.Empty;
                else
                    return attribute.Configuration;
            }
        }

        /// <summary>Gets a boolean value indicating if the assembly has been built as delay-signed.</summary>
        /// <returns>True, if the assembly has been built as delay-signed; otherwise, False.</returns>
        public bool DelaySign
        {
            get
            {
                AssemblyDelaySignAttribute attribute = GetCustomAttribute(typeof(AssemblyDelaySignAttribute)) as AssemblyDelaySignAttribute;
                if (attribute == null)
                    return false;
                else
                    return attribute.DelaySign;
            }
        }

        /// <summary>Gets the version information of the assembly.</summary>
        /// <returns>The version information of the assembly</returns>
        public string InformationalVersion
        {
            get
            {
                AssemblyInformationalVersionAttribute attribute = GetCustomAttribute(typeof(AssemblyInformationalVersionAttribute)) as AssemblyInformationalVersionAttribute;
                if (attribute == null)
                    return string.Empty;
                else
                    return attribute.InformationalVersion;
            }
        }

        /// <summary>Gets the name of the file containing the key pair used to generate a strong name for the attributed
        /// assembly.</summary>
        /// <returns>A string containing the name of the file that contains the key pair.</returns>
        public string KeyFile
        {
            get
            {
                AssemblyKeyFileAttribute attribute = GetCustomAttribute(typeof(AssemblyKeyFileAttribute)) as AssemblyKeyFileAttribute;
                if (attribute == null)
                    return string.Empty;
                else
                    return attribute.KeyFile;
            }
        }

        /// <summary>Gets the culture name of the assembly.</summary>
        /// <returns>The culture name of the assembly.</returns>
        public string CultureName
        {
            get
            {
                NeutralResourcesLanguageAttribute attribute = GetCustomAttribute(typeof(NeutralResourcesLanguageAttribute)) as NeutralResourcesLanguageAttribute;
                if (attribute == null)
                    return string.Empty;
                else
                    return attribute.CultureName;
            }
        }

        /// <summary>Gets the assembly version used to instruct the System.Resources.ResourceManager to ask for a particular
        /// version of a satellite assembly to simplify updates of the main assembly of an application.</summary>
        public string SatelliteContractVersion
        {
            get
            {
                SatelliteContractVersionAttribute attribute = GetCustomAttribute(typeof(SatelliteContractVersionAttribute)) as SatelliteContractVersionAttribute;
                if (attribute == null)
                    return string.Empty;
                else
                    return attribute.Version;
            }
        }

        /// <summary>Gets the string representing the assembly version used to indicate to a COM client that all classes
        /// in the current version of the assembly are compatible with classes in an earlier version of the assembly.</summary>
        /// <returns>The string representing the assembly version in MajorVersion.MinorVersion.RevisionNumber.BuildNumber
        /// format.</returns>
        public string ComCompatibleVersion
        {
            get
            {
                ComCompatibleVersionAttribute attribute = GetCustomAttribute(typeof(ComCompatibleVersionAttribute)) as ComCompatibleVersionAttribute;
                if (attribute == null)
                    return string.Empty;
                else
                    return attribute.MajorVersion.ToString() + "." + attribute.MinorVersion.ToString() + "." + "." + attribute.BuildNumber.ToString() + attribute.RevisionNumber.ToString();
            }
        }

        /// <summary>Gets a boolean value indicating if the assembly is exposed to COM.</summary>
        /// <returns>True, if the assembly is exposed to COM; otherwise, False.</returns>
        public bool ComVisible
        {
            get
            {
                ComVisibleAttribute attribute = GetCustomAttribute(typeof(ComVisibleAttribute)) as ComVisibleAttribute;
                if (attribute == null)
                    return false;
                else
                    return attribute.Value;
            }
        }

        /// <summary>Gets the assembly GUID that is used as an ID if the assembly is exposed to COM.</summary>
        /// <returns>The assembly GUID that is used as an ID if the assembly is exposed to COM.</returns>
        public string Guid
        {
            get
            {
                GuidAttribute attribute = GetCustomAttribute(typeof(GuidAttribute)) as GuidAttribute;
                if (attribute == null)
                    return string.Empty;
                else
                    return attribute.Value;
            }
        }

        /// <summary>Gets the string representing the assembly version number in MajorVersion.MinorVersion format.</summary>
        /// <returns>The string representing the assembly version number in MajorVersion.MinorVersion format.</returns>
        public string TypeLibVersion
        {
            get
            {
                TypeLibVersionAttribute attribute = GetCustomAttribute(typeof(TypeLibVersionAttribute)) as TypeLibVersionAttribute;
                if (attribute == null)
                    return string.Empty;
                else
                    return attribute.MajorVersion.ToString() + "." + attribute.MinorVersion.ToString();
            }
        }

        /// <summary>Gets a boolean value indicating whether the indicated program element is CLS-compliant.</summary>
        /// <returns>True, if the program element is CLS-compliant; otherwise, False.</returns>
        public bool CLSCompliant
        {
            get
            {
                CLSCompliantAttribute attribute = GetCustomAttribute(typeof(CLSCompliantAttribute)) as CLSCompliantAttribute;
                if (attribute == null)
                    return false;
                else
                    return attribute.IsCompliant;
            }
        }

        /// <summary>Gets a value that indicates whether the runtime will track information during code generation for the
        /// debugger.</summary>
        /// <returns>True, if the runtime will track information during code generation for the debugger; otherwise, False.</returns>
        public bool Debuggable
        {
            get
            {
                DebuggableAttribute attribute = GetCustomAttribute(typeof(DebuggableAttribute)) as DebuggableAttribute;
                if (attribute == null)
                    return false;
                else
                    return attribute.IsJITTrackingEnabled;
            }
        }

        /// <summary>Gets the path or UNC location of the loaded file that contains the manifest.</summary>
        /// <returns>The location of the loaded file that contains the manifest.</returns>
        public string Location
        {
            get
            {
                return m_assemblyInstance.Location.ToLower();
            }
        }

        /// <summary>Gets the location of the assembly as specified originally; for example, in a
        /// AssemblyName object.</summary>
        /// <returns>The location of the assembly as specified originally.</returns>
        public string CodeBase
        {
            get
            {
                return m_assemblyInstance.CodeBase.Replace("file:///", "").ToLower();
            }
        }

        /// <summary>Gets the display name of the assembly.</summary>
        /// <returns>The display name of the assembly.</returns>
        public string FullName
        {
            get
            {
                return m_assemblyInstance.FullName;
            }
        }

        /// <summary>Gets the simple, unencrypted name of the assembly.</summary>
        /// <returns>A string that is the simple, unencrypted name of the assembly.</returns>
        public string Name
        {
            get
            {
                return m_assemblyInstance.GetName().Name;
            }
        }

        /// <summary>Gets the major, minor, revision, and build numbers of the assembly.</summary>
        /// <returns>A System.Version object representing the major, minor, revision, and build numbers of the assembly.</returns>
        public Version Version
        {
            get
            {
                return m_assemblyInstance.GetName().Version;
            }
        }

        /// <summary>Gets the string representing the version of the common language runtime (CLR) saved in the file
        /// containing the manifest.</summary>
        /// <returns>The string representing the CLR version folder name. This is not a full path.</returns>
        public string ImageRuntimeVersion
        {
            get
            {
                return m_assemblyInstance.ImageRuntimeVersion;
            }
        }

        /// <summary>Gets a boolean value indicating whether the assembly was loaded from the global assembly cache.</summary>
        /// <returns>True, if the assembly was loaded from the global assembly cache; otherwise, False.</returns>
        public bool GACLoaded
        {
            get
            {
                return m_assemblyInstance.GlobalAssemblyCache;
            }
        }

        /// <summary>Gets the date and time when the assembly was last built.</summary>
        /// <returns>The date and time when the assembly was last built.</returns>
        public DateTime BuildDate
        {
            get
            {
                return File.GetLastWriteTime(m_assemblyInstance.Location);
            }
        }

        /// <summary>Gets the root namespace of the assembly.</summary>
        /// <returns>The root namespace of the assembly.</returns>
        public string RootNamespace
        {
            get
            {
                return m_assemblyInstance.GetExportedTypes()[0].Namespace;
            }
        }

        #endregion

        #region [ Methods ]

        /// <summary>Gets a collection of assembly attributes exposed by the assembly.</summary>
        /// <returns>A System.Specialized.KeyValueCollection of assembly attributes.</returns>
        public NameValueCollection GetAttributes()
        {
            NameValueCollection assemblyAttributes = new NameValueCollection();

            //Add some values that are not in AssemblyInfo.
            assemblyAttributes.Add("Full Name", FullName);
            assemblyAttributes.Add("Name", Name);
            assemblyAttributes.Add("Version", Version.ToString());
            assemblyAttributes.Add("Image Runtime Version", ImageRuntimeVersion);
            assemblyAttributes.Add("Build Date", BuildDate.ToString());
            assemblyAttributes.Add("Location", Location);
            assemblyAttributes.Add("Code Base", CodeBase);
            assemblyAttributes.Add("GAC Loaded", GACLoaded.ToString());

            //Add all attributes available from AssemblyInfo.
            foreach (object assemblyAttribute in m_assemblyInstance.GetCustomAttributes(false))
            {
                if (assemblyAttribute is AssemblyTitleAttribute)
                {
                    assemblyAttributes.Add("Title", Title);
                }
                else if (assemblyAttribute is AssemblyDescriptionAttribute)
                {
                    assemblyAttributes.Add("Description", Description);
                }
                else if (assemblyAttribute is AssemblyCompanyAttribute)
                {
                    assemblyAttributes.Add("Company", Company);
                }
                else if (assemblyAttribute is AssemblyProductAttribute)
                {
                    assemblyAttributes.Add("Product", Product);
                }
                else if (assemblyAttribute is AssemblyCopyrightAttribute)
                {
                    assemblyAttributes.Add("Copyright", Copyright);
                }
                else if (assemblyAttribute is AssemblyTrademarkAttribute)
                {
                    assemblyAttributes.Add("Trademark", Trademark);
                }
                else if (assemblyAttribute is AssemblyConfigurationAttribute)
                {
                    assemblyAttributes.Add("Configuration", Configuration);
                }
                else if (assemblyAttribute is AssemblyDelaySignAttribute)
                {
                    assemblyAttributes.Add("Delay Sign", DelaySign.ToString());
                }
                else if (assemblyAttribute is AssemblyInformationalVersionAttribute)
                {
                    assemblyAttributes.Add("Informational Version", InformationalVersion);
                }
                else if (assemblyAttribute is AssemblyKeyFileAttribute)
                {
                    assemblyAttributes.Add("Key File", KeyFile);
                }
                else if (assemblyAttribute is NeutralResourcesLanguageAttribute)
                {
                    assemblyAttributes.Add("Culture Name", CultureName);
                }
                else if (assemblyAttribute is SatelliteContractVersionAttribute)
                {
                    assemblyAttributes.Add("Satellite Contract Version", SatelliteContractVersion);
                }
                else if (assemblyAttribute is ComCompatibleVersionAttribute)
                {
                    assemblyAttributes.Add("Com Compatible Version", ComCompatibleVersion);
                }
                else if (assemblyAttribute is ComVisibleAttribute)
                {
                    assemblyAttributes.Add("Com Visible", ComVisible.ToString());
                }
                else if (assemblyAttribute is GuidAttribute)
                {
                    assemblyAttributes.Add("Guid", Guid);
                }
                else if (assemblyAttribute is TypeLibVersionAttribute)
                {
                    assemblyAttributes.Add("Type Lib Version", TypeLibVersion);
                }
                else if (assemblyAttribute is CLSCompliantAttribute)
                {
                    assemblyAttributes.Add("CLS Compliant", CLSCompliant.ToString());
                }
                else if (assemblyAttribute is DebuggableAttribute)
                {
                    assemblyAttributes.Add("Debuggable", Debuggable.ToString());
                }
            }

            return assemblyAttributes;
        }

        /// <summary>Gets the specified assembly attribute if it is exposed by the assembly.</summary>
        /// <param name="attributeType">Type of the attribute to get.</param>
        /// <returns>The requested assembly attribute if it exists; otherwise null.</returns>
        public object GetCustomAttribute(Type attributeType)
        {
            //Returns the requested assembly attribute.
            object[] assemblyAttributes = m_assemblyInstance.GetCustomAttributes(attributeType, false);
            if (assemblyAttributes.Length <= 0)
                return null;
            else
                return assemblyAttributes[0];
        }

        /// <summary>Gets the specified embedded resource from the assembly.</summary>
        /// <param name="resourceName">The full name (including the namespace) of the embedded resource to get.</param>
        /// <returns>The embedded resource.</returns>
        public Stream GetEmbeddedResource(string resourceName)
        {
            //Extracts and returns the requested embedded resource.
            return m_assemblyInstance.GetEmbeddedResource(resourceName);
        }

        #endregion

        #region [ Static ]

        // Static Fields
        private static AssemblyInfo s_callingAssembly;
        private static AssemblyInfo s_entryAssembly;
        private static AssemblyInfo s_executingAssembly;
        private static Dictionary<string, Assembly> s_assemblyCache;
        private static bool s_addedResolver;

        // Static Properties

        /// <summary>Gets the <see cref="AssemblyInfo"/> object of the assembly that invoked the currently executing method.</summary>
        public static AssemblyInfo CallingAssembly
        {
            get
            {
                if (s_callingAssembly == null)
                {
                    // We have to find the calling assembly of the caller.
                    StackTrace trace = new StackTrace();
                    Assembly caller = Assembly.GetCallingAssembly();
                    Assembly current = Assembly.GetExecutingAssembly();
                    foreach (StackFrame frame in trace.GetFrames())
                    {
                        Assembly assembly = Assembly.GetAssembly(frame.GetMethod().DeclaringType);
                        if (assembly != caller && assembly != current)
                        {
                            // Assembly is neither the current assembly or the calling assembly.
                            s_callingAssembly = new AssemblyInfo(assembly);
                            break;
                        }
                    }
                }

                return s_callingAssembly;
            }
        }

        /// <summary>Gets the <see cref="AssemblyInfo"/> object of the process executable in the default application domain.</summary>
        public static AssemblyInfo EntryAssembly
        {
            get
            {
                if (s_entryAssembly == null)
                    s_entryAssembly = new AssemblyInfo(Assembly.GetEntryAssembly());

                return s_entryAssembly;
            }
        }

        /// <summary>Gets the <see cref="AssemblyInfo"/> object of the assembly that contains the code that is currently executing.</summary>
        public static AssemblyInfo ExecutingAssembly
        {
            get
            {
                if (s_executingAssembly == null)
                    // Caller's assembly will be the executing assembly for the caller.
                    s_executingAssembly = new AssemblyInfo(Assembly.GetCallingAssembly());

                return s_executingAssembly;
            }
        }

        // Static Methods

        /// <summary>Loads the specified assembly that is embedded as a resource in the assembly.</summary>
        /// <param name="assemblyName">Name of the assembly to load.</param>
        /// <remarks>This cannot be used to load TVA.Core itself.</remarks>
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.ControlAppDomain)]
        public static void LoadAssemblyFromResource(string assemblyName)
        {
            // Hooks into assembly resolve event for current domain so it can load assembly from embedded resource.
            if (!s_addedResolver)
            {
                AppDomain.CurrentDomain.AssemblyResolve += ResolveAssemblyFromResource;
                s_addedResolver = true;
            }

            // Loads the assembly (This will invoke event that will resolve assembly from resource.).
            AppDomain.CurrentDomain.Load(assemblyName);
        }

        private static Assembly ResolveAssemblyFromResource(object sender, ResolveEventArgs e)
        {
            Assembly resourceAssembly;
            string shortName = e.Name.Split(',')[0];

            if (s_assemblyCache == null) s_assemblyCache = new Dictionary<string, Assembly>();
            resourceAssembly = s_assemblyCache[shortName];

            if (resourceAssembly == null)
            {
                // Loops through all of the resources in the executing assembly.
                foreach (string name in Assembly.GetEntryAssembly().GetManifestResourceNames())
                {
                    // Sees if the embedded resource name matches the assembly it is trying to load.
                    if (string.Compare(FilePath.GetFileNameWithoutExtension(name), EntryAssembly.RootNamespace + "." + shortName, true) == 0)
                    {
                        // If so, loads embedded resource assembly into a binary buffer.
                        System.IO.Stream resourceStream = Assembly.GetEntryAssembly().GetManifestResourceStream(name);
                        byte[] buffer = new byte[resourceStream.Length];
                        resourceStream.Read(buffer, 0, (int)resourceStream.Length);
                        resourceStream.Close();

                        // Loads assembly from binary buffer.
                        resourceAssembly = Assembly.Load(buffer);
                        s_assemblyCache.Add(shortName, resourceAssembly);
                        break;
                    }
                }
            }

            return resourceAssembly;
        }

        #endregion
    }
}