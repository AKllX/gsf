//*******************************************************************************************************
//  IChannelDefinition.cs
//  Copyright © 2009 - TVA, all rights reserved - Gbtc
//
//  Build Environment: C#, Visual Studio 2008
//  Primary Developer: James R Carroll
//      Office: PSO TRAN & REL, CHATTANOOGA - MR BK-C
//       Phone: 423/751-4165
//       Email: jrcarrol@tva.gov
//
//  Code Modification History:
//  -----------------------------------------------------------------------------------------------------
//  02/18/2005 - James R Carroll
//       Generated original version of source code.
//
//*******************************************************************************************************

using System;
using System.Runtime.Serialization;

namespace PCS.PhasorProtocols
{
    /// <summary>
    /// Represents a protocol independent interface representation of a definition of any kind of <see cref="IChannel"/> data.
    /// </summary>
    public interface IChannelDefinition : IChannel, ISerializable, IEquatable<IChannelDefinition>, IComparable<IChannelDefinition>, IComparable
    {
        /// <summary>
        /// Gets the <see cref="IConfigurationCell"/> parent of this <see cref="IChannelDefinition"/>.
        /// </summary>
        IConfigurationCell Parent { get; }
        
        /// <summary>
        /// Gets the <see cref="PhasorProtocols.DataFormat"/> of this <see cref="IChannelDefinition"/>.
        /// </summary>
        DataFormat DataFormat { get; }

        /// <summary>
        /// Gets or sets the index of this <see cref="IChannelDefinition"/>.
        /// </summary>
        int Index { get; set; }

        /// <summary>
        /// Gets or sets the offset of this <see cref="IChannelDefinition"/>.
        /// </summary>
        float Offset { get; set; }

        /// <summary>
        /// Gets or sets the scaling factor of this <see cref="IChannelDefinition"/>.
        /// </summary>
        int ScalingFactor { get; set; }

        /// <summary>
        /// Gets the maximum value for the scaling factor of this <see cref="IChannelDefinition"/>.
        /// </summary>
        int MaximumScalingFactor { get; }

        /// <summary>
        /// Gets the conversion factor of this <see cref="IChannelDefinition"/>.
        /// </summary>
        float ConversionFactor { get; set; }

        /// <summary>
        /// Gets the scale/bit value of this <see cref="IChannelDefinition"/>.
        /// </summary>
        float ScalePerBit { get; }

        /// <summary>
        /// Gets or sets the label of this <see cref="IChannelDefinition"/>.
        /// </summary>
        string Label { get; set; }

        /// <summary>
        /// Gets the binary label image of this <see cref="IChannelDefinition"/>.
        /// </summary>
        byte[] LabelImage { get; }

        /// <summary>
        /// Gets the maximum label length of this <see cref="IChannelDefinition"/>.
        /// </summary>
        int MaximumLabelLength { get; }
    }
}