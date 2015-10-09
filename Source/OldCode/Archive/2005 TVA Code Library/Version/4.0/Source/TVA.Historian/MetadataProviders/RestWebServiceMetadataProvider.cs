﻿//*******************************************************************************************************
//  RestWebServiceMetadataProvider.cs
//  Copyright © 2009 - TVA, all rights reserved - Gbtc
//
//  Build Environment: C#, Visual Studio 2008
//  Primary Developer: Pinal C. Patel
//      Office: INFO SVCS APP DEV, CHATTANOOGA - MR BK-C
//       Phone: 423/751-3024
//       Email: pcpatel@tva.gov
//
//  Code Modification History:
//  -----------------------------------------------------------------------------------------------------
//  08/11/2009 - Pinal C. Patel
//       Generated original version of source code.
//  08/18/2009 - Pinal C. Patel
//       Added cleanup code for response stream of the REST web service.
//
//*******************************************************************************************************

using System;
using System.IO;
using System.Net;
using TVA.Configuration;

namespace TVA.Historian.MetadataProviders
{
    #region [ Enumerations ]

    /// <summary>
    /// Indicates the data format for a REST (Representational State Transfer) web service.
    /// </summary>
    public enum RestDataFormat
    {
        /// <summary>
        /// Data is in ASMX (.NET Web Service) XML (eXtensible Markup Language) format.
        /// </summary>
        AsmxXml,
        /// <summary>
        /// Data is in REST (Representational State Transfer) XML (eXtensible Markup Language) format.
        /// </summary>
        RestXml,
        /// <summary>
        /// Data is in REST (Representational State Transfer) JSON (JavaScript Object Notation) format.
        /// </summary>
        RestJson
    }

    #endregion

    /// <summary>
    /// Represents a provider of data to a <see cref="TVA.Historian.Files.MetadataFile"/> from a REST (Representational State Transfer) web service.
    /// </summary>
    public class RestWebServiceMetadataProvider : MetadataProviderBase
    {
        #region [ Members ]

        // Fields
        private string m_serviceUri;
        private RestDataFormat m_serviceDataFormat;

        #endregion

        #region [ Constructors ]

        /// <summary>
        /// Initializes a new instance of the <see cref="RestWebServiceMetadataProvider"/> class.
        /// </summary>
        public RestWebServiceMetadataProvider()
            : base()
        {
            m_serviceUri = string.Empty;
            m_serviceDataFormat = RestDataFormat.RestXml;
        }

        #endregion

        #region [ Properties ]

        /// <summary>
        /// Gets or sets the URI where the REST web service is hosted.
        /// </summary>
        public string ServiceUri
        {
            get
            {
                return m_serviceUri;
            }
            set
            {
                m_serviceUri = value;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="RestDataFormat"/> in which the REST web service exposes the data.
        /// </summary>
        public RestDataFormat ServiceDataFormat
        {
            get
            {
                return m_serviceDataFormat;
            }
            set
            {
                m_serviceDataFormat = value;
            }
        }

        #endregion

        #region [ Methods ]

        /// <summary>
        /// Saves <see cref="RestWebServiceMetadataProvider"/> settings to the config file if the <see cref="MetadataProviderBase.PersistSettings"/> property is set to true.
        /// </summary>
        public override void SaveSettings()
        {
            base.SaveSettings();
            if (PersistSettings)
            {
                // Ensure that settings category is specified.
                if (string.IsNullOrEmpty(SettingsCategory))
                    throw new InvalidOperationException("SettingsCategory property has not been set.");

                // Save settings under the specified category.
                ConfigurationFile config = ConfigurationFile.Current;
                CategorizedSettingsElement element = null;
                CategorizedSettingsElementCollection settings = config.Settings[SettingsCategory];
                element = settings["ServiceUri", true];
                element.Update(m_serviceUri, element.Description, element.Encrypted);
                element = settings["ServiceDataFormat", true];
                element.Update(m_serviceDataFormat, element.Description, element.Encrypted);
                config.Save();
            }
        }

        /// <summary>
        /// Loads saved <see cref="RestWebServiceMetadataProvider"/> settings from the config file if the <see cref="MetadataProviderBase.PersistSettings"/> property is set to true.
        /// </summary>
        public override void LoadSettings()
        {
            base.LoadSettings();
            if (PersistSettings)
            {
                // Ensure that settings category is specified.
                if (string.IsNullOrEmpty(SettingsCategory))
                    throw new InvalidOperationException("SettingsCategory property has not been set.");

                // Load settings from the specified category.
                ConfigurationFile config = ConfigurationFile.Current;
                CategorizedSettingsElementCollection settings = config.Settings[SettingsCategory];
                settings.Add("ServiceUri", m_serviceUri, "URI where the REST web service is hosted.");
                settings.Add("ServiceDataFormat", m_serviceDataFormat, "Format (AsmxXml; RestXml; RestJson) in which the REST web service exposes the data.");
                ServiceUri = settings["ServiceUri"].ValueAs(m_serviceUri);
                ServiceDataFormat = settings["ServiceDataFormat"].ValueAs(m_serviceDataFormat);
            }
        }

        /// <summary>
        /// Refreshes the <see cref="MetadataProviderBase.Metadata"/> from a REST web service.
        /// </summary>
        /// <exception cref="ArgumentNullException"><see cref="ServiceUri"/> is set to a null or empty string.</exception>
        protected override void RefreshMetadata()
        {
            if (string.IsNullOrEmpty(m_serviceUri))
                throw new ArgumentNullException("ServiceUri");

            // Update existing metadata with retrieved metadata.
            WebResponse response = null;
            Stream responseStream = null;
            try
            {
                response = WebRequest.Create(m_serviceUri).GetResponse();
                responseStream = response.GetResponseStream();

                MetadataUpdater metadataUpdater = new MetadataUpdater(Metadata);
                metadataUpdater.UpdateMetadata(responseStream, m_serviceDataFormat);
            }
            finally
            {
                if (response != null)
                    response.Close();

                if (responseStream != null)
                    responseStream.Dispose();
            }
        }

        #endregion
    }
}