'*******************************************************************************************************
'  PhasorValueCollection.vb - Phasor value collection class
'  Copyright � 2005 - TVA, all rights reserved - Gbtc
'
'  Build Environment: VB.NET, Visual Studio 2005
'  Primary Developer: J. Ritchie Carroll, Operations Data Architecture [TVA]
'      Office: COO - TRNS/PWR ELEC SYS O, CHATTANOOGA, TN - MR 2W-C
'       Phone: 423/751-2827
'       Email: jrcarrol@tva.gov
'
'  Note: Phasors are stored in rectangular format internally
'
'  Code Modification History:
'  -----------------------------------------------------------------------------------------------------
'  11/12/2004 - J. Ritchie Carroll
'       Initial version of source generated
'
'*******************************************************************************************************

Imports System.Runtime.Serialization

''' <summary>This class represents the protocol independent collection of phasor values.</summary>
<CLSCompliant(False), Serializable()> _
Public Class PhasorValueCollection

    Inherits ChannelValueCollectionBase(Of IPhasorDefinition, IPhasorValue)

    Protected Sub New()
    End Sub

    Protected Sub New(ByVal info As SerializationInfo, ByVal context As StreamingContext)

        MyBase.New(info, context)

    End Sub

    Public Sub New(ByVal maximumCount As Int32)

        MyBase.New(maximumCount)

    End Sub

    Public Overrides ReadOnly Property DerivedType() As Type
        Get
            Return Me.GetType()
        End Get
    End Property

End Class