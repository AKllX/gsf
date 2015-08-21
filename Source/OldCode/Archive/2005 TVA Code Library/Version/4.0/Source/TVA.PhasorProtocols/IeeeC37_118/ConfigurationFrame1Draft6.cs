//*******************************************************************************************************
//  ConfigurationFrame1Draft6.cs
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
using System.ComponentModel;
using System.Runtime.Serialization;
using TVA.Parsing;

namespace TVA.PhasorProtocols.IeeeC37_118
{
    /// <summary>
    /// Represents the IEEE C37.118 draft 6 implementation of a <see cref="IConfigurationFrame"/>, type 1, that can be sent or received.
    /// </summary>
    [Serializable()]
    public class ConfigurationFrame1Draft6 : ConfigurationFrame1
    {
        #region [ Constructors ]

        /// <summary>
        /// Creates a new <see cref="ConfigurationFrame1Draft6"/>.
        /// </summary>
        /// <remarks>
        /// This constructor is used by <see cref="FrameImageParserBase{TTypeIdentifier,TOutputType}"/> to parse an IEEE C37.118 draft 6 configuration frame.
        /// </remarks>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public ConfigurationFrame1Draft6()
        {
        }

        /// <summary>
        /// Creates a new <see cref="ConfigurationFrame1Draft6"/> from specified parameters.
        /// </summary>
        /// <param name="timebase">Timebase to use for fraction second resolution.</param>
        /// <param name="idCode">The ID code of this <see cref="ConfigurationFrame1Draft6"/>.</param>
        /// <param name="timestamp">The exact timestamp, in <see cref="Ticks"/>, of the data represented by this <see cref="ConfigurationFrame1Draft6"/>.</param>
        /// <param name="frameRate">The defined frame rate of this <see cref="ConfigurationFrame1Draft6"/>.</param>
        /// <remarks>
        /// This constructor is used by a consumer to generate an IEEE C37.118 draft 6 configuration frame.
        /// </remarks>
        public ConfigurationFrame1Draft6(uint timebase, ushort idCode, Ticks timestamp, ushort frameRate)
            : base(timebase, idCode, timestamp, frameRate)
        {
        }

        /// <summary>
        /// Creates a new <see cref="ConfigurationFrame1Draft6"/> from serialization parameters.
        /// </summary>
        /// <param name="info">The <see cref="SerializationInfo"/> with populated with data.</param>
        /// <param name="context">The source <see cref="StreamingContext"/> for this deserialization.</param>
        protected ConfigurationFrame1Draft6(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion

        #region [ Properties ]

        /// <summary>
        /// Gets the <see cref="IeeeC37_118.DraftRevision"/> of this <see cref="ConfigurationFrame1Draft6"/>.
        /// </summary>
        public override DraftRevision DraftRevision
        {
            get
            {
                return IeeeC37_118.DraftRevision.Draft6;
            }
        }

        #endregion
    }
}