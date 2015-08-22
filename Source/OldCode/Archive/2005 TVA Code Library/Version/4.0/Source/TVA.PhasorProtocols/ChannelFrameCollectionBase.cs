//*******************************************************************************************************
//  ChannelFrameCollectionBase.cs
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
//  01/14/2005 - James R. Carroll
//       Generated original version of source code.
//
//*******************************************************************************************************

using System;
using System.Linq;
using System.Runtime.Serialization;

namespace TVA.PhasorProtocols
{
    /// <summary>
    /// Represents a protocol independent collection of <see cref="IChannelFrame"/> objects.
    /// </summary>
    /// <typeparam name="T">Specific <see cref="IChannelFrame"/> type that the <see cref="ChannelFrameCollectionBase{T}"/> contains.</typeparam>
    [Serializable()]
    public abstract class ChannelFrameCollectionBase<T> : ChannelCollectionBase<T> where T : IChannelFrame
    {
        #region [ Constructors ]

        /// <summary>
        /// Creates a new <see cref="ChannelFrameCollectionBase{T}"/> using specified <paramref name="lastValidIndex"/>.
        /// </summary>
        /// <param name="lastValidIndex">Last valid index for the collection (i.e., maximum count - 1).</param>
        /// <remarks>
        /// <paramref name="lastValidIndex"/> is used instead of maximum count so that maximum type values may
        /// be specified as needed. For example, if the protocol specifies a collection with a signed 16-bit
        /// maximum length you can specify <see cref="Int16.MaxValue"/> (i.e., 32,767) as the last valid index
        /// for the collection since total number of items supported would be 32,768.
        /// </remarks>
        protected ChannelFrameCollectionBase(int lastValidIndex)
            : base(lastValidIndex)
        {
        }

        /// <summary>
        /// Creates a new <see cref="ChannelFrameCollectionBase{T}"/> from serialization parameters.
        /// </summary>
        /// <param name="info">The <see cref="SerializationInfo"/> with populated with data.</param>
        /// <param name="context">The source <see cref="StreamingContext"/> for this deserialization.</param>
        protected ChannelFrameCollectionBase(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion

        #region [ Properties ]

        /// <summary>
        /// Gets the length of the <see cref="ChannelCollectionBase{T}.BinaryImage"/>.
        /// </summary>
        /// <remarks>
        /// The length of the <see cref="ChannelFrameCollectionBase{T}"/> binary image is the combined length of all the items in the collection.
        /// </remarks>
        public override int BinaryLength
        {
            get
            {
                // It is expected that frames can be different lengths, so we manually sum lengths - this represents
                // a change in behavior from the base class...
                return this.Sum(frame => frame.BinaryLength);
            }
        }

        #endregion
    }
}