//*******************************************************************************************************
//  FrequencyValue.cs
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
//  04/27/2009 - James R. Carroll
//       Generated original version of source code.
//
//*******************************************************************************************************

using System;
using System.Runtime.Serialization;

namespace TVA.PhasorProtocols.SelFastMessage
{
    /// <summary>
    /// Represents the SEL Fast Message implementation of a <see cref="IFrequencyValue"/>.
    /// </summary>
    [Serializable()]
    public class FrequencyValue : FrequencyValueBase
    {
        #region [ Constructors ]

        /// <summary>
        /// Creates a new <see cref="FrequencyValue"/>.
        /// </summary>
        /// <param name="parent">The <see cref="IDataCell"/> parent of this <see cref="FrequencyValue"/>.</param>
        /// <param name="frequencyDefinition">The <see cref="IFrequencyDefinition"/> associated with this <see cref="FrequencyValue"/>.</param>
        public FrequencyValue(IDataCell parent, IFrequencyDefinition frequencyDefinition)
            : base(parent, frequencyDefinition)
        {
        }

        /// <summary>
        /// Creates a new <see cref="FrequencyValue"/> from specified parameters.
        /// </summary>
        /// <param name="parent">The <see cref="DataCell"/> parent of this <see cref="FrequencyValue"/>.</param>
        /// <param name="frequencyDefinition">The <see cref="FrequencyDefinition"/> associated with this <see cref="FrequencyValue"/>.</param>
        /// <param name="frequency">The floating point value that represents this <see cref="FrequencyValue"/>.</param>
        /// <param name="dfdt">The floating point value that represents the change in this <see cref="FrequencyValue"/> over time.</param>
        public FrequencyValue(DataCell parent, FrequencyDefinition frequencyDefinition, double frequency, double dfdt)
            : base(parent, frequencyDefinition, frequency, dfdt)
        {
        }

        /// <summary>
        /// Creates a new <see cref="FrequencyValue"/> from serialization parameters.
        /// </summary>
        /// <param name="info">The <see cref="SerializationInfo"/> with populated with data.</param>
        /// <param name="context">The source <see cref="StreamingContext"/> for this deserialization.</param>
        protected FrequencyValue(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion

        #region [ Properties ]

        /// <summary>
        /// Gets or sets the <see cref="DataCell"/> parent of this <see cref="FrequencyValue"/>.
        /// </summary>
        public virtual new DataCell Parent
        {
            get
            {
                return base.Parent as DataCell;
            }
            set
            {
                base.Parent = value;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="FrequencyDefinition"/> associated with this <see cref="FrequencyValue"/>.
        /// </summary>
        public virtual new FrequencyDefinition Definition
        {
            get
            {
                return base.Definition as FrequencyDefinition;
            }
            set
            {
                base.Definition = value;
            }
        }

        /// <summary>
        /// Gets the length of the <see cref="BodyImage"/>.
        /// </summary>
        /// <remarks>
        /// The SEL Fast Message protocol provides frequency and df/dt as optional data measurements, so we override
        /// the default behavior to account for this change in operation.
        /// </remarks>
        protected override int BodyLength
        {
            get
            {
                return 4;
            }
        }

        /// <summary>
        /// Gets the binary body image of the <see cref="FrequencyValue"/> object.
        /// </summary>
        protected override byte[] BodyImage
        {
            get
            {
                return EndianOrder.BigEndian.GetBytes((float)Frequency);
            }
        }

        #endregion

        #region [ Methods ]

        /// <summary>
        /// Parses the binary body image.
        /// </summary>
        /// <param name="binaryImage">Binary image to parse.</param>
        /// <param name="startIndex">Start index into <paramref name="binaryImage"/> to begin parsing.</param>
        /// <param name="length">Length of valid data within <paramref name="binaryImage"/>.</param>
        /// <returns>The length of the data that was parsed.</returns>
        protected override int ParseBodyImage(byte[] binaryImage, int startIndex, int length)
        {
            Frequency = EndianOrder.BigEndian.ToSingle(binaryImage, startIndex);
            return 4;
        }

        #endregion

        #region [ Static ]

        // Static Methods

        // Delegate handler to create a new SEL Fast Message frequency value
        internal static IFrequencyValue CreateNewValue(IDataCell parent, IFrequencyDefinition definition, byte[] binaryImage, int startIndex, out int parsedLength)
        {
            IFrequencyValue frequency = new FrequencyValue(parent, definition);

            parsedLength = frequency.Initialize(binaryImage, startIndex, 0);

            return frequency;
        }

        #endregion
    }
}