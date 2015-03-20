﻿//******************************************************************************************************
//  IConfigurationLoader.cs - Gbtc
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
//  04/04/2014 - Stephen C. Wills
//       Generated original version of source code.
//
//******************************************************************************************************

using System;
using System.Data;

namespace GSF.TimeSeries.Configuration
{
    /// <summary>
    /// Represents the interface by which the time-series engine's
    /// system configuration is loaded from a configuration source.
    /// </summary>
    public interface IConfigurationLoader
    {
        #region [ Members ]

        // Events

        /// <summary>
        /// Occurs when the configuration loader has a message to provide about its current status.
        /// </summary>
        event EventHandler<EventArgs<string>> StatusMessage;

        /// <summary>
        /// Occurs when the configuration loader encounters a non-catastrophic exception.
        /// </summary>
        event EventHandler<EventArgs<Exception>> ProcessException;

        #endregion

        #region [ Properties ]

        /// <summary>
        /// Gets the flag that indicates whether augmentation is supported by this configuration loader.
        /// </summary>
        bool CanAugment
        {
            get;
        }

        #endregion

        #region [ Methods ]

        /// <summary>
        /// Loads the entire configuration data set from scratch.
        /// </summary>
        /// <returns>The configuration data set.</returns>
        DataSet Load();

        /// <summary>
        /// Augments the given configuration data set with the changes
        /// tracked since the version of the given configuration data set.
        /// </summary>
        /// <param name="configuration">The configuration data set to be augmented.</param>
        void Augment(DataSet configuration);

        #endregion
    }
}
