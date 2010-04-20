//*******************************************************************************************************
//  CategorizedSettingsElement.cs - Gbtc
//
//  Tennessee Valley Authority, 2009
//  No copyright is claimed pursuant to 17 USC § 105.  All Other Rights Reserved.
//
//  This software is made freely available under the TVA Open Source Agreement (see below).
//
//  Code Modification History:
//  -----------------------------------------------------------------------------------------------------
//  04/11/2006 - Pinal C. Patel
//       Generated original version of source code.
//  05/25/2006 - J. Ritchie Carroll
//       Added Try/Catch safety wrapper around GetTypedValue implementation.
//  06/01/2006 - J. Ritchie Carroll
//       Added GetTypedValue overload to handle boolean types as a special case.
//  08/17/2007 - Darrell Zuercher
//       Edited code comments.
//  09/17/2008 - Pinal C. Patel
//       Converted code to C#.
//  09/22/2008 - J. Ritchie Carroll
//       Made boolean types a special case (i.e., using ParseBoolean extension).
//  09/29/2008 - Pinal C. Patel
//       Reviewed code comments.
//  09/14/2009 - Stephen C. Wills
//       Added new header and license agreement.
//  04/15/2010 - Pinal C. Patel
//       Modified property setters to update the internal property bag only if values have changed.
//  04/20/2010 - Pinal C. Patel
//       Added new Category property for the purpose of managing user scope setting.
//       Removed publicly accessible constructors for managability.
//       Added Scope property as a way of identifying user scope settings.
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
using System.Configuration;
using TVA.Security.Cryptography;

namespace TVA.Configuration
{
    #region [ Enumerations ]

    /// <summary>
    /// Specifies the scope of a setting represented by <see cref="CategorizedSettingsElement"/>.
    /// </summary>
    public enum SettingScope
    {
        /// <summary>
        /// Settings is intended for user specific use.
        /// </summary>
        User,
        /// <summary>
        /// Settings is intended for application wide use.
        /// </summary>
        Application
    }

    #endregion

    /// <summary>
    /// Represents a settings entry in the config file.
    /// </summary>
    public class CategorizedSettingsElement : ConfigurationElement
    {
        #region [ Members ]

        // Constants

        /// <summary>
        /// Specifies the default value for the <see cref="Value"/> property.
        /// </summary>
        public const string DefaultValue = "";

        /// <summary>
        /// Specifies the default value for the <see cref="Description"/> property.
        /// </summary>
        public const string DefaultDescription = "";

        /// <summary>
        /// Specifies the default value for the <see cref="Encrypted"/> property.
        /// </summary>
        public const bool DefaultEncrypted = false;

        /// <summary>
        /// Specifies the default value for the <see cref="Scope"/> property.
        /// </summary>
        public const SettingScope DefaultScope = SettingScope.Application;

        private const string DefaultCryptoKey = "0679d9ae-aca5-4702-a3f5-604415096987";

        // Fields
        private string m_cryptoKey;
        private CategorizedSettingsElementCollection m_category;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Required by the configuration API and is for internal use only.
        /// </summary>
        internal CategorizedSettingsElement(CategorizedSettingsElementCollection category)
            : this(category, "")
        {
        }

        /// <summary>
        /// Required by the configuration API and is for internal use only.
        /// </summary>
        internal CategorizedSettingsElement(CategorizedSettingsElementCollection category, string name)
            : base()
        {
            m_category = category;
            this.Name = name;
            m_cryptoKey = DefaultCryptoKey;
        }

        #endregion

        #region [ Properties ]

        /// <summary>
        /// Gets or sets the <see cref="CategorizedSettingsElementCollection"/> to which this <see cref="CategorizedSettingsElement"/> belongs.
        /// </summary>
        public CategorizedSettingsElementCollection Category
        {
            get
            {
                return m_category;
            }
            internal set
            {
                m_category = value;
            }
        }

        /// <summary>
        /// Gets or sets the identifier of the setting.
        /// </summary>
        /// <returns>The identifier of the setting.</returns>
        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        public string Name
        {
            get
            {
                return (string)base["name"];
            }
            set
            {
                base["name"] = value;
            }
        }

        /// <summary>
        /// Gets or sets the value of the setting.
        /// </summary>
        /// <returns>The value of the setting.</returns>
        [ConfigurationProperty("value", IsRequired = true, DefaultValue = DefaultValue)]
        public string Value
        {
            get
            {
                string value = (string)base["value"];
                if (Scope == SettingScope.User)
                    // Setting is user specific so retrive value from user settings store.
                    value = Category.Section.File.UserSettings.Read(Category.Name, Name, value);

                return DecryptValue(value);
            }
            set
            {
                // Continue only if values are different.
                if (value.ToNonNullString().Equals(Value))
                    return;

                value = EncryptValue(value);
                if (Scope == SettingScope.Application || Category[Name] == null)
                    // Setting is application wide or is being added for the first time.
                    base["value"] = value;
                else
                    // Setting is user specific so update setting in user settings store.
                    Category.Section.File.UserSettings.Write(Category.Name, Name, value);
            }
        }

        /// <summary>
        /// Gets or sets the description of the setting.
        /// </summary>
        /// <returns>The description of the setting.</returns>
        [ConfigurationProperty("description", IsRequired = true, DefaultValue = DefaultDescription)]
        public string Description
        {
            get
            {
                return (string)base["description"];
            }
            set
            {
                // Continue only if values are different.
                if (value.ToNonNullString().Equals(Description))
                    return;

                base["description"] = value;
            }
        }

        /// <summary>
        /// Gets or sets a boolean value that indicates whether the setting value is to be encrypted.
        /// </summary>
        /// <returns>true, if the setting value is to be encrypted; otherwise false.</returns>
        [ConfigurationProperty("encrypted", IsRequired = true, DefaultValue = DefaultEncrypted)]
        public bool Encrypted
        {
            get
            {
                return (bool)base["encrypted"];
            }
            set
            {
                // Continue only if values are different.
                if (value.Equals(Encrypted))
                    return;

                string elementValue = this.Value;   // Gets the decrypted value if encrypted.
                base["encrypted"] = value;
                this.Value = elementValue;          // This will cause encryption to be performed if required.
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="SettingScope"/>.
        /// </summary>
        [ConfigurationProperty("scope", IsRequired = false, DefaultValue = DefaultScope)]
        public SettingScope Scope
        {
            get
            {
                return (SettingScope)base["scope"];
            }
            set
            {
                // Continue only if values are different.
                if (value.Equals(Scope))
                    return;

                base["scope"] = value;
            }
        }

        #endregion

        #region [ Methods ]

        /// <summary>
        /// Sets the key to be used for encrypting and decrypting the <see cref="Value"/>.
        /// </summary>
        /// <param name="cryptoKey">New crypto key.</param>
        public void SetCryptoKey(string cryptoKey)
        {
            if (!string.IsNullOrEmpty(cryptoKey))
            {
                // Re-encrypt the existing value with the new key. This is done because the value gets encrypted,
                // if specified, with the default crypto key when the value is set during instantiation.
                string decryptedValue = Value;
                m_cryptoKey = cryptoKey;
                Value = decryptedValue;
            }
        }

        /// <summary>
        /// Updates setting information.
        /// </summary>
        /// <param name="value">New setting value.</param>
        public void Update(object value)
        {
            Update(value, Description, Encrypted, Scope);
        }

        /// <summary>
        /// Updates setting information.
        /// </summary>
        /// <param name="value">New setting value.</param>
        public void Update(string value)
        {
            Update(value, Description, Encrypted, Scope);
        }

        /// <summary>
        /// Updates setting information.
        /// </summary>
        /// <param name="value">New setting value.</param>
        /// <param name="description">New setting description.</param>
        public void Update(object value, string description)
        {
            Update(value, description, Encrypted, Scope);
        }

        /// <summary>
        /// Updates setting information.
        /// </summary>
        /// <param name="value">New setting value.</param>
        /// <param name="description">New setting description.</param>
        public void Update(string value, string description)
        {
            Update(value, description, Encrypted, Scope);
        }

        /// <summary>
        /// Updates setting information.
        /// </summary>
        /// <param name="value">New setting value.</param>
        /// <param name="description">New setting description.</param>
        /// <param name="encrypted">A boolean value that indicated whether the new setting value is to be encrypted.</param>
        public void Update(object value, string description, bool encrypted)
        {
            Update(value, description, encrypted, Scope);
        }

        /// <summary>
        /// Updates setting information.
        /// </summary>
        /// <param name="value">New setting value.</param>
        /// <param name="description">New setting description.</param>
        /// <param name="encrypted">A boolean value that indicated whether the new setting value is to be encrypted.</param>
        public void Update(string value, string description, bool encrypted)
        {
            Update(value, description, encrypted, Scope);
        }

        /// <summary>
        /// Updates setting information.
        /// </summary>
        /// <param name="value">New setting value.</param>
        /// <param name="description">New setting description.</param>
        /// <param name="encrypted">A boolean value that indicated whether the new setting value is to be encrypted.</param>
        /// <param name="scope">One of the <see cref="SettingScope"/> values.</param>
        public void Update(object value, string description, bool encrypted, SettingScope scope)
        {
            this.Scope = scope;
            this.Value = value.ToNonNullString();
            this.Description = description;
            this.Encrypted = encrypted;
        }

        /// <summary>
        /// Updates setting information.
        /// </summary>
        /// <param name="value">New setting value.</param>
        /// <param name="description">New setting description.</param>
        /// <param name="encrypted">A boolean value that indicated whether the new setting value is to be encrypted.</param>
        /// <param name="scope">One of the <see cref="SettingScope"/> values.</param>
        public void Update(string value, string description, bool encrypted, SettingScope scope)
        {
            this.Scope = scope;
            this.Value = value;
            this.Description = description;
            this.Encrypted = encrypted;
        }

        /// <summary>
        /// Gets the setting value as the specified type.
        /// </summary>
        /// <typeparam name="T">Type to which the setting value is to be converted.</typeparam>
        /// <returns>The type-coerced value of the setting.</returns>
        /// <remarks>
        /// If this function fails to properly coerce value to specified type, the default value is returned.
        /// </remarks>
        public T ValueAs<T>()
        {
            return this.ValueAs<T>(default(T));
        }

        /// <summary>
        /// Gets the setting value as the specified type.
        /// </summary>
        /// <typeparam name="T">Type to which the setting value is to be converted.</typeparam>
        /// <param name="defaultValue">The default value to return if the setting value is empty.</param>
        /// <returns>The type-coerced value of the setting.</returns>
        /// <remarks>
        /// If this function fails to properly coerce value to specified type, the default value is returned.
        /// </remarks>
        public T ValueAs<T>(T defaultValue)
        {
            try
            {
                if (string.IsNullOrEmpty(Value))
                    // Value is an empty string - use default value.
                    return defaultValue;
                else
                    // Value is not empty string - convert to target type.
                    return Value.ConvertToType<T>();
            }
            catch
            {
                // Conversion to target type failed so use the default value.
                return defaultValue;
            }
        }

        /// <summary>
        /// Gets the setting value as a string.
        /// </summary>
        /// <returns>Value as string.</returns>
        public string ValueAsString()
        {
            return ValueAsString("");
        }

        /// <summary>
        /// Gets the setting value as a string.
        /// </summary>
        /// <param name="defaultValue">The default value to return if the setting value is empty.</param>
        /// <returns>Value as string.</returns>
        public string ValueAsString(string defaultValue)
        {
            string setting = Value;

            if (string.IsNullOrEmpty(setting))
                return defaultValue;
            else
                return setting;
        }

        /// <summary>
        /// Gets the setting value as a boolean.
        /// </summary>
        /// <returns>Value as boolean.</returns>
        public bool ValueAsBoolean()
        {
            return ValueAsBoolean(default(bool));
        }

        /// <summary>
        /// Gets the setting value as a boolean.
        /// </summary>
        /// <param name="defaultValue">The default value to return if the setting value is empty.</param>
        /// <returns>Value as boolean.</returns>
        public bool ValueAsBoolean(bool defaultValue)
        {
            return ValueAs(defaultValue);
        }

        /// <summary>
        /// Gets the setting value as a byte.
        /// </summary>
        /// <returns>Value as byte.</returns>
        public byte ValueAsByte()
        {
            return ValueAsByte(default(byte));
        }

        /// <summary>
        /// Gets the setting value as a byte.
        /// </summary>
        /// <param name="defaultValue">The default value to return if the setting value is empty.</param>
        /// <returns>Value as byte.</returns>
        public byte ValueAsByte(byte defaultValue)
        {
            return ValueAs(defaultValue);
        }

        /// <summary>
        /// Gets the setting value as a signed byte.
        /// </summary>
        /// <returns>Value as signed byte.</returns>
        [CLSCompliant(false)]
        public sbyte ValueAsSByte()
        {
            return ValueAsSByte(default(sbyte));
        }

        /// <summary>
        /// Gets the setting value as a signed byte.
        /// </summary>
        /// <param name="defaultValue">The default value to return if the setting value is empty.</param>
        /// <returns>Value as signed byte.</returns>
        [CLSCompliant(false)]
        public sbyte ValueAsSByte(sbyte defaultValue)
        {
            return ValueAs(defaultValue);
        }

        /// <summary>
        /// Gets the setting value as a char.
        /// </summary>
        /// <returns>Value as char.</returns>
        public char ValueAsChar()
        {
            return ValueAsChar(default(char));
        }

        /// <summary>
        /// Gets the setting value as a char.
        /// </summary>
        /// <param name="defaultValue">The default value to return if the setting value is empty.</param>
        /// <returns>Value as char.</returns>
        public char ValueAsChar(char defaultValue)
        {
            return ValueAs(defaultValue);
        }

        /// <summary>
        /// Gets the setting value as a short.
        /// </summary>
        /// <returns>Value as short.</returns>
        public short ValueAsInt16()
        {
            return ValueAsInt16(default(short));
        }

        /// <summary>
        /// Gets the setting value as a short.
        /// </summary>
        /// <param name="defaultValue">The default value to return if the setting value is empty.</param>
        /// <returns>Value as short.</returns>
        public short ValueAsInt16(short defaultValue)
        {
            return ValueAs(defaultValue);
        }

        /// <summary>
        /// Gets the setting value as an int.
        /// </summary>
        /// <returns>Value as int.</returns>
        public int ValueAsInt32()
        {
            return ValueAsInt32(default(int));
        }

        /// <summary>
        /// Gets the setting value as an int.
        /// </summary>
        /// <param name="defaultValue">The default value to return if the setting value is empty.</param>
        /// <returns>Value as int.</returns>
        public int ValueAsInt32(int defaultValue)
        {
            return ValueAs(defaultValue);
        }

        /// <summary>
        /// Gets the setting value as a long.
        /// </summary>
        /// <returns>Value as long.</returns>
        public long ValueAsInt64()
        {
            return ValueAsInt64(default(long));
        }

        /// <summary>
        /// Gets the setting value as a long.
        /// </summary>
        /// <param name="defaultValue">The default value to return if the setting value is empty.</param>
        /// <returns>Value as long.</returns>
        public long ValueAsInt64(long defaultValue)
        {
            return ValueAs(defaultValue);
        }

        /// <summary>
        /// Gets the setting value as an unsigned short.
        /// </summary>
        /// <returns>Value as unsigned short.</returns>
        [CLSCompliant(false)]
        public ushort ValueAsUInt16()
        {
            return ValueAsUInt16(default(ushort));
        }

        /// <summary>
        /// Gets the setting value as an unsigned short.
        /// </summary>
        /// <param name="defaultValue">The default value to return if the setting value is empty.</param>
        /// <returns>Value as unsigned short.</returns>
        [CLSCompliant(false)]
        public ushort ValueAsUInt16(ushort defaultValue)
        {
            return ValueAs(defaultValue);
        }

        /// <summary>
        /// Gets the setting value as an unsigned int.
        /// </summary>
        /// <returns>Value as unsigned int.</returns>
        [CLSCompliant(false)]
        public uint ValueAsUInt32()
        {
            return ValueAsUInt32(default(uint));
        }

        /// <summary>
        /// Gets the setting value as an unsigned int.
        /// </summary>
        /// <param name="defaultValue">The default value to return if the setting value is empty.</param>
        /// <returns>Value as unsigned int.</returns>
        [CLSCompliant(false)]
        public uint ValueAsUInt32(uint defaultValue)
        {
            return ValueAs(defaultValue);
        }

        /// <summary>
        /// Gets the setting value as an unsigned long.
        /// </summary>
        /// <returns>Value as unsigned long.</returns>
        [CLSCompliant(false)]
        public ulong ValueAsUInt64()
        {
            return ValueAsUInt64(default(ulong));
        }

        /// <summary>
        /// Gets the setting value as an unsigned long.
        /// </summary>
        /// <param name="defaultValue">The default value to return if the setting value is empty.</param>
        /// <returns>Value as unsigned long.</returns>
        [CLSCompliant(false)]
        public ulong ValueAsUInt64(ulong defaultValue)
        {
            return ValueAs(defaultValue);
        }

        /// <summary>
        /// Gets the setting value as a float.
        /// </summary>
        /// <returns>Value as float.</returns>
        public float ValueAsSingle()
        {
            return ValueAsSingle(default(float));
        }

        /// <summary>
        /// Gets the setting value as a float.
        /// </summary>
        /// <param name="defaultValue">The default value to return if the setting value is empty.</param>
        /// <returns>Value as float.</returns>
        public float ValueAsSingle(float defaultValue)
        {
            return ValueAs(defaultValue);
        }

        /// <summary>
        /// Gets the setting value as a double.
        /// </summary>
        /// <returns>Value as double.</returns>
        public double ValueAsDouble()
        {
            return ValueAsDouble(default(double));
        }

        /// <summary>
        /// Gets the setting value as a double.
        /// </summary>
        /// <param name="defaultValue">The default value to return if the setting value is empty.</param>
        /// <returns>Value as double.</returns>
        public double ValueAsDouble(double defaultValue)
        {
            return ValueAs(defaultValue);
        }

        /// <summary>
        /// Gets the setting value as a decimal.
        /// </summary>
        /// <returns>Value as decimal.</returns>
        public decimal ValueAsDecimal()
        {
            return ValueAsDecimal(default(decimal));
        }

        /// <summary>
        /// Gets the setting value as a decimal.
        /// </summary>
        /// <param name="defaultValue">The default value to return if the setting value is empty.</param>
        /// <returns>Value as decimal.</returns>
        public decimal ValueAsDecimal(decimal defaultValue)
        {
            return ValueAs(defaultValue);
        }

        /// <summary>
        /// Gets the setting value as DateTime.
        /// </summary>
        /// <returns>Value as DateTime.</returns>
        public DateTime ValueAsDateTime()
        {
            return ValueAsDateTime(default(DateTime));
        }

        /// <summary>
        /// Gets the setting value as DateTime.
        /// </summary>
        /// <param name="defaultValue">The default value to return if the setting value is empty.</param>
        /// <returns>Value as DateTime.</returns>
        public DateTime ValueAsDateTime(DateTime defaultValue)
        {
            return ValueAs(defaultValue);
        }

        private string EncryptValue(string value)
        {
            if (Encrypted && !string.IsNullOrEmpty(value))
            {
                try
                {
                    // Encrypts the element's value.
                    value = value.Encrypt(m_cryptoKey, CipherStrength.Aes256);
                }
                catch (Exception ex)
                {
                    throw new ConfigurationErrorsException(string.Format("Failed to encrypt '{0}'", value), ex);
                }
            }
            return value;
        }

        private string DecryptValue(string value)
        {
            if (Encrypted && !string.IsNullOrEmpty(value))
            {
                try
                {
                    // Decrypts the element's value.
                    return value.Decrypt(m_cryptoKey, CipherStrength.Aes256);
                }
                catch (Exception ex)
                {
                    throw new ConfigurationErrorsException(string.Format("Failed to decrypt '{0}'", value), ex);
                }
            }

            return value;
        }

        #endregion
    }
}
