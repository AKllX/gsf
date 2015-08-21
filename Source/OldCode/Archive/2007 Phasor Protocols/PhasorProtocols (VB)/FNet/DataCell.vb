'*******************************************************************************************************
'  DataCell.vb - FNet Data Cell
'  Copyright � 2008 - TVA, all rights reserved - Gbtc
'
'  Build Environment: VB.NET, Visual Studio 2008
'  Primary Developer: J. Ritchie Carroll, Operations Data Architecture [TVA]
'      Office: COO - TRNS/PWR ELEC SYS O, CHATTANOOGA, TN - MR 2W-C
'       Phone: 423/751-2827
'       Email: jrcarrol@tva.gov
'
'  Code Modification History:
'  -----------------------------------------------------------------------------------------------------
'  02/08/2007 - J. Ritchie Carroll & Jian (Ryan) Zuo
'       Initial version of source generated
'
'*******************************************************************************************************

Imports System.Runtime.Serialization
Imports System.Text
Imports PhasorProtocols.Common
Imports PhasorProtocols.FNet.Common
Imports TVA.Text.Common

Namespace FNet

    ' This data cell represents what most might call a "field" in table of rows - it is a single unit of data for a specific PMU
    <CLSCompliant(False), Serializable()> _
    Public Class DataCell

        Inherits DataCellBase

        Private m_analogValue As Single

        Protected Sub New()
        End Sub

        Protected Sub New(ByVal info As SerializationInfo, ByVal context As StreamingContext)

            MyBase.New(info, context)

        End Sub

        Public Sub New(ByVal parent As IDataFrame, ByVal configurationCell As IConfigurationCell)

            MyBase.New(parent, False, configurationCell, MaximumPhasorValues, MaximumAnalogValues, MaximumDigitalValues)

            ' Initialize single phasor value and frequency value with an empty value
            PhasorValues.Add(New PhasorValue(Me, configurationCell.PhasorDefinitions(0), Single.NaN, Single.NaN))

            ' Initialize frequency and df/dt
            FrequencyValue = New FrequencyValue(Me, configurationCell.FrequencyDefinition, Single.NaN, Single.NaN)

        End Sub

        Public Sub New(ByVal dataCell As IDataCell)

            MyBase.New(dataCell)

        End Sub

        Public Sub New(ByVal parent As IDataFrame, ByVal state As DataFrameParsingState, ByVal index As Int32, ByVal binaryImage As Byte(), ByVal startIndex As Int32)

            MyBase.New(parent, False, MaximumPhasorValues, MaximumAnalogValues, MaximumDigitalValues, _
                New DataCellParsingState(state.ConfigurationFrame.Cells(index), Nothing, Nothing, Nothing, Nothing), _
                binaryImage, startIndex)

        End Sub

        Friend Shared Function CreateNewDataCell(ByVal parent As IChannelFrame, ByVal state As IChannelFrameParsingState(Of IDataCell), ByVal index As Int32, ByVal binaryImage As Byte(), ByVal startIndex As Int32) As IDataCell

            Return New DataCell(parent, state, index, binaryImage, startIndex)

        End Function

        Public Overrides ReadOnly Property DerivedType() As System.Type
            Get
                Return Me.GetType()
            End Get
        End Property

        Public Shadows ReadOnly Property Parent() As DataFrame
            Get
                Return MyBase.Parent
            End Get
        End Property

        Public Shadows Property ConfigurationCell() As ConfigurationCell
            Get
                Return MyBase.ConfigurationCell
            End Get
            Set(ByVal value As ConfigurationCell)
                MyBase.ConfigurationCell = value
            End Set
        End Property

        Public Overrides Property SynchronizationIsValid() As Boolean
            Get
                Return ConfigurationCell.NumberOfSatellites > 0
            End Get
            Set(ByVal value As Boolean)
                ' We just ignore this value as FNet defines synchronization validity as a derived value based on the number of available satellites
            End Set
        End Property

        Public Overrides Property DataIsValid() As Boolean
            Get
                Return True
            End Get
            Set(ByVal value As Boolean)
                ' We just ignore this value as FNet defines no flags for data validity
            End Set
        End Property

        Public Overrides Property DataSortingType() As DataSortingType
            Get
                Return IIf(SynchronizationIsValid, PhasorProtocols.DataSortingType.ByTimestamp, PhasorProtocols.DataSortingType.ByArrival)
            End Get
            Set(ByVal value As DataSortingType)
                ' We just ignore this value as we have defined data sorting type as a derived value based on synchronization validity
            End Set
        End Property

        Public Overrides Property PmuError() As Boolean
            Get
                Return False
            End Get
            Set(ByVal value As Boolean)
                ' We just ignore this value as FNet defines no flags for data errors
            End Set
        End Property

        Public ReadOnly Property FNetDate() As String
            Get
                Return Parent.Timestamp.ToString("MMddyy")
            End Get
        End Property

        Public ReadOnly Property FNetTime() As String
            Get
                Return Parent.Timestamp.ToString("HHmmss")
            End Get
        End Property

        Public Property AnalogValue() As Single
            Get
                Return m_analogValue
            End Get
            Set(ByVal value As Single)
                m_analogValue = value
            End Set
        End Property

        Public ReadOnly Property FNetDataString() As String
            Get
                With New StringBuilder
                    .Append(StartByte)
                    .Append(IDCode)
                    .Append(" "c)
                    .Append(FNetDate)
                    .Append(" "c)
                    .Append(FNetTime)
                    .Append(" "c)
                    .Append(Parent.SampleIndex)
                    .Append(" "c)
                    .Append(m_analogValue)
                    .Append(" "c)
                    .Append(FrequencyValue.Frequency)
                    .Append(" "c)
                    .Append(PhasorValues(0).Magnitude)
                    .Append(" "c)
                    .Append(PhasorValues(0).Angle)
                    .Append(EndByte)

                    Return .ToString()
                End With
            End Get
        End Property

        Protected Overrides ReadOnly Property BodyLength() As UInt16
            Get
                Return FNetDataString.Length()
            End Get
        End Property

        Protected Overrides ReadOnly Property BodyImage() As Byte()
            Get
                Return Encoding.ASCII.GetBytes(FNetDataString)
            End Get
        End Property

        ''' <summary>
        ''' Overrides the ParseBodyImage in ChannelCell,phase the image body
        ''' </summary>
        ''' <remarks>The longitude, latitude and number of satellite at the top of minute in FNET data</remarks>
        Protected Overrides Sub ParseBodyImage(ByVal state As IChannelParsingState, ByVal binaryImage As Byte(), ByVal startIndex As Int32)

            If binaryImage(startIndex) <> StartByte Then Throw New InvalidOperationException("Bad data stream, expected start byte 01 as first byte in FNet frame, got " & binaryImage(startIndex).ToString("x"c).PadLeft(2, "0"c).ToUpper())

            Dim configurationCell As ConfigurationCell = DirectCast(DirectCast(state, IDataCellParsingState).ConfigurationCell, ConfigurationCell)

            Dim data As String()
            Dim stopByteIndex As Integer

            For x As Integer = startIndex To binaryImage.Length - 1
                If binaryImage(x) = EndByte Then
                    stopByteIndex = x
                    Exit For
                End If
            Next

            ' Parse FNet data frame into individual fields separated by spaces
            data = RemoveDuplicateWhiteSpace(Encoding.ASCII.GetString(binaryImage, startIndex + 1, stopByteIndex - startIndex - 1)).Trim().Split(" "c)

            ' Make sure all the needed data elements exist (could be a bad frame)
            If data.Length >= 8 Then
                ' Assign sample index
                Parent.SampleIndex = Convert.ToInt32(data(Element.SampleIndex))

                ' Get timestamp of data record
                Parent.Ticks = configurationCell.TicksOffset + ParseTimestamp(data(Element.Date), data(Element.Time), Parent.SampleIndex, configurationCell.FrameRate)

                ' Parse out first analog value (can be long/lat at top of minute)
                m_analogValue = Convert.ToSingle(data(Element.Analog))

                If Convert.ToInt32(data(Element.Time).Substring(4, 2)) = 0 Then
                    Select Case Parent.SampleIndex
                        Case 1
                            configurationCell.Latitude = m_analogValue
                        Case 2
                            configurationCell.Longitude = m_analogValue
                        Case 3
                            configurationCell.NumberOfSatellites = m_analogValue
                    End Select
                End If

                ' Create frequency value
                FrequencyValue = New FrequencyValue(Me, configurationCell.FrequencyDefinition, Convert.ToSingle(data(Element.Frequency)), 0)

                ' Create single phasor value
                PhasorValues.Add(PhasorValue.CreateFromPolarValues(Me, configurationCell.PhasorDefinitions(0), Convert.ToSingle(data(Element.Angle)), Convert.ToSingle(data(Element.Voltage))))
            Else
                Throw New InvalidOperationException("Invalid number of data elements encountered in FNET data stream line: """ & Encoding.ASCII.GetString(binaryImage, startIndex + 1, stopByteIndex - startIndex - 1) & """.  Got " & data.Length & " elements, expected 8.")
            End If

        End Sub

        Public Overrides ReadOnly Property Attributes() As Dictionary(Of String, String)
            Get
                Dim baseAttributes As Dictionary(Of String, String) = MyBase.Attributes

                baseAttributes.Add("FNET Date", FNetDate)
                baseAttributes.Add("FNET Time", FNetTime)
                baseAttributes.Add("Analog Value", m_analogValue)

                Return baseAttributes
            End Get
        End Property

        ''' <summary>
        ''' Convert FNET date (mm/dd/yy), time (hh:mm:ss) and subsecond to time ticks
        ''' </summary>
        ''' <param name="fnetDate"></param>
        ''' <param name="fnetTime"></param>
        ''' <param name="sampleIndex"></param>
        ''' <param name="frameRate"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function ParseTimestamp(ByVal fnetDate As String, ByVal fnetTime As String, ByVal sampleIndex As Integer, ByVal frameRate As Integer) As Long

            fnetDate = fnetDate.PadLeft(6, "0"c)
            fnetTime = fnetTime.PadLeft(6, "0"c)

            If sampleIndex = 10 Then
                Return New Date( _
                    2000 + Convert.ToInt32(fnetDate.Substring(4, 2)), _
                    Convert.ToInt32(fnetDate.Substring(0, 2).Trim()), _
                    Convert.ToInt32(fnetDate.Substring(2, 2)), _
                    Convert.ToInt32(fnetTime.Substring(0, 2)), _
                    Convert.ToInt32(fnetTime.Substring(2, 2)), _
                    Convert.ToInt32(fnetTime.Substring(4, 2)), _
                    0).AddSeconds(1.0).Ticks
            Else
                Return New Date( _
                    2000 + Convert.ToInt32(fnetDate.Substring(4, 2)), _
                    Convert.ToInt32(fnetDate.Substring(0, 2).Trim()), _
                    Convert.ToInt32(fnetDate.Substring(2, 2)), _
                    Convert.ToInt32(fnetTime.Substring(0, 2)), _
                    Convert.ToInt32(fnetTime.Substring(2, 2)), _
                    Convert.ToInt32(fnetTime.Substring(4, 2)), _
                    Convert.ToInt32(sampleIndex / frameRate * 1000)).Ticks
            End If

        End Function

    End Class

End Namespace