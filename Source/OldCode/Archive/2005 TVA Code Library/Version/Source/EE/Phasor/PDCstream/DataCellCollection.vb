'*******************************************************************************************************
'  DataCellCollection.vb - PDCstream specific data cell collection
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
'  11/12/2004 - James R Carroll
'       Initial version of source generated
'
'*******************************************************************************************************

Namespace EE.Phasor.PDCstream

    Public Class DataCellCollection

        Inherits Phasor.DataCellCollection

        Public Sub New()

            ' PDCstream data cells are variable length
            MyBase.New(Int16.MaxValue, False)

        End Sub

        Public Shadows Sub Add(ByVal value As DataCell)

            MyBase.Add(value)

        End Sub

        Default Public Shadows ReadOnly Property Item(ByVal index As Integer) As DataCell
            Get
                Return MyBase.Item(index)
            End Get
        End Property

    End Class

End Namespace
