//*******************************************************************************************************
//  ConfigurationCellCollection.cs
//  Copyright © 2009 - TVA, all rights reserved - Gbtc
//
//  Build Environment: C#, Visual Studio 2008
//  Primary Developer: James R. Carroll
//      Office: PSO TRAN & REL, CHATTANOOGA - MR BK-C
//       Phone: 423/751-4165
//       Email: jrcarrol@tva.gov
//
//  Code Modification History:
//  -----------------------------------------------------------------------------------------------------
//  11/12/2004 - James R. Carroll
//       Generated original version of source code.
//
//*******************************************************************************************************

using System;
using System.Runtime.Serialization;

namespace TVA.PhasorProtocols.IeeeC37_118
{
    /// <summary>
    /// Represents a IEEE C37.118 implementation of a collection of <see cref="IConfigurationCell"/> objects.
    /// </summary>
    [Serializable()]
    public class ConfigurationCellCollection : PhasorProtocols.ConfigurationCellCollection
    {
        #region [ Constructors ]

        /// <summary>
        /// Creates a new <see cref="ConfigurationCellCollection"/>.
        /// </summary>
        public ConfigurationCellCollection()
            : base(ushort.MaxValue, false)
        {
        }

        /// <summary>
        /// Creates a new <see cref="ConfigurationCellCollection"/> from serialization parameters.
        /// </summary>
        /// <param name="info">The <see cref="SerializationInfo"/> with populated with data.</param>
        /// <param name="context">The source <see cref="StreamingContext"/> for this deserialization.</param>
        protected ConfigurationCellCollection(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion

        #region [ Properties ]

        /// <summary>
        /// Gets or sets <see cref="ConfigurationCell"/> at specified <paramref name="index"/>.
        /// </summary>
        /// <param name="index">Index of value to get or set.</param>
        public new ConfigurationCell this[int index]
        {
            get
            {
                return base[index] as ConfigurationCell;
            }
            set
            {
                base[index] = value;
            }
        }

        #endregion
    }
}