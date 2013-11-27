﻿//******************************************************************************************************
//  ConfigurationErrorFrame.cs - Gbtc
//
//  Copyright © 2012, Grid Protection Alliance.  All Rights Reserved.
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
//  04/23/2010 - James R. Carroll
//       Generated original version of source code.
//  10/5/2012 - Gavin E. Holden
//       Added new header and license agreement.
//  12/04/2012 - J. Ritchie Carroll
//       Migrated to PhasorProtocolAdapters project.
//  12/13/2012 - Starlynn Danyelle Gilliam
//       Modified Header.
//
//******************************************************************************************************

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using GSF;
using GSF.Parsing;
using GSF.PhasorProtocols;
using GSF.TimeSeries;

namespace PhasorProtocolAdapters
{
    /// <summary>
    /// Represents an implementation of <see cref="IConfigurationFrame"/> that by existence only denotes an error state.
    /// </summary>
    /// <remarks>
    /// This class is used by <see cref="CommonPhasorServices"/> to return an error state when configuration could not be retrieved.
    /// </remarks>
    [Serializable]
    public class ConfigurationErrorFrame : IConfigurationFrame
    {
        #region [ Constructors ]

        /// <summary>
        /// Creates a new instance of the <see cref="ConfigurationErrorFrame"/> class.
        /// </summary>
        public ConfigurationErrorFrame()
        {
        }

        /// <summary>
        /// Creates a new <see cref="ConfigurationErrorFrame"/> from serialization parameters.
        /// </summary>
        /// <param name="info">The <see cref="SerializationInfo"/> with populated with data.</param>
        /// <param name="context">The source <see cref="StreamingContext"/> for this deserialization.</param>
        protected ConfigurationErrorFrame(SerializationInfo info, StreamingContext context)
        {
        }

        #endregion

        #region [ Properties ]

        /// <summary>
        /// Gets exact timestamp, in <see cref="Ticks"/>, of the data represented in this <see cref="ConfigurationErrorFrame"/>.
        /// </summary>
        /// <remarks>
        /// The value of this property represents the number of 100-nanosecond intervals that have elapsed since 12:00:00 midnight, January 1, 0001.
        /// </remarks>
        public Ticks Timestamp
        {
            get
            {
                return 0;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Gets timestamp, in ticks, of when this <see cref="ConfigurationErrorFrame"/> was received (i.e., created).
        /// </summary>
        public Ticks ReceivedTimestamp
        {
            get
            {
                return DateTime.UtcNow.Ticks;
            }
        }

        #endregion

        #region [ Methods ]

        /// <summary>
        /// Populates a <see cref="SerializationInfo"/> with the data needed to serialize the target object.
        /// </summary>
        /// <param name="info">The <see cref="SerializationInfo"/> to populate with data.</param>
        /// <param name="context">The destination <see cref="StreamingContext"/> for this serialization.</param>
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
        }

        #endregion

        #region [ Explicit Interface Implementation ]

        ConfigurationCellCollection IConfigurationFrame.Cells
        {
            get
            {
                return null;
            }
        }

        IConfigurationFrameParsingState IConfigurationFrame.State
        {
            get
            {
                return null;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        ushort IConfigurationFrame.FrameRate
        {
            get
            {
                return 0;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        decimal IConfigurationFrame.TicksPerFrame
        {
            get
            {
                return 0;
            }
        }

        void IConfigurationFrame.SetNominalFrequency(LineFrequency value)
        {
            throw new NotImplementedException();
        }

        FundamentalFrameType IChannelFrame.FrameType
        {
            get
            {
                return FundamentalFrameType.ConfigurationFrame;
            }
        }

        object IChannelFrame.Cells
        {
            get
            {
                return null;
            }
        }

        ushort IChannelFrame.IDCode
        {
            get
            {
                return 0;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        UnixTimeTag IChannelFrame.TimeTag
        {
            get
            {
                return null;
            }
        }

        Dictionary<string, string> IChannel.Attributes
        {
            get
            {
                return null;
            }
        }

        IChannelParsingState IChannel.State
        {
            get
            {
                return null;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        object IChannel.Tag
        {
            get
            {
                return null;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        int ISupportBinaryImage.BinaryLength
        {
            get
            {
                return 0;
            }
        }

        int ISupportBinaryImage.ParseBinaryImage(byte[] buffer, int startIndex, int length)
        {
            return 0;
        }

        int ISupportBinaryImage.GenerateBinaryImage(byte[] buffer, int startIndex)
        {
            return 0;
        }

        IDictionary<Guid, ITimeSeriesEntity> IFrame.Entities
        {
            get
            {
                return null;
            }
        }

        bool IEquatable<IFrame>.Equals(IFrame other)
        {
            return false;
        }

        int IComparable<IFrame>.CompareTo(IFrame other)
        {
            return 1;
        }

        int IComparable.CompareTo(object obj)
        {
            return 1;
        }

        #endregion
    }
}
