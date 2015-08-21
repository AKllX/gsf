'*******************************************************************************************************
'  IChannelCollection.vb - Channel collection interface
'  Copyright � 2005 - TVA, all rights reserved - Gbtc
'
'  Build Environment: VB.NET, Visual Studio 2003
'  Primary Developer: James R Carroll, System Analyst [TVA]
'      Office: COO - TRNS/PWR ELEC SYS O, CHATTANOOGA, TN - MR 2W-C
'       Phone: 423/751-2827
'       Email: jrcarrol@tva.gov
'
'  Code Modification History:
'  -----------------------------------------------------------------------------------------------------
'  02/18/2005 - James R Carroll
'       Initial version of source generated
'
'*******************************************************************************************************

Namespace EE.Phasor

    ' This interface represents a protocol independent representation of a collection of any data type.
    Public Interface IChannelCollection

        Inherits IChannel, IEnumerable

        Sub Add(ByVal value As IChannel)

        ReadOnly Property Item(ByVal index As Integer) As IChannel

    End Interface

End Namespace
