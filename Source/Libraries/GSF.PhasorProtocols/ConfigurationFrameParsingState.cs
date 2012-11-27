//******************************************************************************************************
//  ConfigurationFrameParsingState.cs - Gbtc
//
//  Copyright © 2010, Grid Protection Alliance.  All Rights Reserved.
//
//  Licensed to the Grid Protection Alliance (GPA) under one or more contributor license agreements. See
//  the NOTICE file distributed with this work for additional information regarding copyright ownership.
//  The GPA licenses this file to you under the Eclipse Public License -v 1.0 (the "License"); you may
//  not use this file except in compliance with the License. You may obtain a copy of the License at:
//
//      http://www.opensource.org/licenses/eclipse-1.0.php
//
//  Unless agreed to in writing, the subject software distributed under the License is distributed on an
//  "AS-IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. Refer to the
//  License for the specific language governing permissions and limitations.
//
//  Code Modification History:
//  ----------------------------------------------------------------------------------------------------
//  01/14/2005 - J. Ritchie Carroll
//       Generated original version of source code.
//  09/15/2009 - Stephen C. Wills
//       Added new header and license agreement.
//  10/5/2012 - Gavin E. Holden
//       Added new header and license agreement.
//
//******************************************************************************************************

namespace GSF.PhasorProtocols
{
    /// <summary>
    /// Represents the protocol independent common implementation of the parsing state used by any <see cref="IConfigurationFrame"/>.
    /// </summary>
    public class ConfigurationFrameParsingState : ChannelFrameParsingStateBase<IConfigurationCell>, IConfigurationFrameParsingState
    {
        #region [ Constructors ]

        /// <summary>
        /// Creates a new <see cref="ConfigurationFrameParsingState"/> from specified parameters.
        /// </summary>
        /// <param name="parsedBinaryLength">Binary length of the <see cref="IConfigurationFrame"/> being parsed.</param>
        /// <param name="createNewCellFunction">Reference to delegate to create new <see cref="IConfigurationCell"/> instances.</param>
        public ConfigurationFrameParsingState(int parsedBinaryLength, CreateNewCellFunction<IConfigurationCell> createNewCellFunction)
            : base(parsedBinaryLength, createNewCellFunction)
        {
        }

        /// <summary>
        /// Creates a new <see cref="ConfigurationFrameParsingState"/> from specified parameters.
        /// </summary>
        /// <param name="parsedBinaryLength">Binary length of the <see cref="IConfigurationFrame"/> being parsed.</param>
        /// <param name="createNewCellFunction">Reference to delegate to create new <see cref="IConfigurationCell"/> instances.</param>
        /// <param name="cellCount">Number of cells that exist in the frame to be parsed.</param>
        public ConfigurationFrameParsingState(int parsedBinaryLength, CreateNewCellFunction<IConfigurationCell> createNewCellFunction, int cellCount)
            : base(parsedBinaryLength, createNewCellFunction)
        {
            CellCount = cellCount;
        }

        #endregion
    }
}