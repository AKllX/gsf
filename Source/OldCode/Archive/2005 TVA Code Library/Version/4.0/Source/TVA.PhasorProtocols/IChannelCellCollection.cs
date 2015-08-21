//*******************************************************************************************************
//  IChannelCellCollection.cs
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
//  02/18/2005 - James R. Carroll
//       Generated original version of source code.
//  08/07/2009 - Josh Patterson
//      Edited Comments
//
//*******************************************************************************************************

namespace TVA.PhasorProtocols
{
    /// <summary>
    /// Represents a protocol independent interface representation of a collection of <see cref="IChannelCell"/> objects.
    /// </summary>
    /// <typeparam name="T">Generic type used.</typeparam>
    public interface IChannelCellCollection<T> : IChannelCollection<T> where T : IChannelCell
    {
        /// <summary>
        /// Gets flag that determines if the lengths of <see cref="IChannelCell"/> elements in this <see cref="IChannelCellCollection{T}"/> are constant.
        /// </summary>
        bool ConstantCellLength { get; }
    }
}
