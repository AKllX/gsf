'*******************************************************************************************************
'  TVA.Collections.Common.vb - Common Collection Functions
'  Copyright � 2006 - TVA, all rights reserved - Gbtc
'
'  Build Environment: VB.NET, Visual Studio 2005
'  Primary Developer: J. Ritchie Carroll, Operations Data Architecture [TVA]
'      Office: COO - TRNS/PWR ELEC SYS O, CHATTANOOGA, TN - MR 2W-C
'       Phone: 423/751-2827
'       Email: jrcarrol@tva.gov
'
'  Code Modification History:
'  -----------------------------------------------------------------------------------------------------
'  02/23/2003 - J. Ritchie Carroll
'       Generated original version of source code.
'  01/23/2005 - J. Ritchie Carroll
'       Migrated 2.0 version of source code from 1.1 source (TVA.Shared.Common).
'  08/17/2007 - Darrell Zuercher
'       Edited code comments.
'
'*******************************************************************************************************

Imports System.Text
Imports TVA.Math.Common

Namespace Collections

    ''' <summary>Defines common global functions related to manipulation of collections.</summary>
    Public NotInheritable Class Common

        Private Sub New()

            ' This class contains only global functions and is not meant to be instantiated.

        End Sub

        ''' <summary>Returns the smallest item from a list of parameters.</summary>
        Public Shared Function Minimum(ByVal ParamArray itemList As Object()) As Object

            Return Minimum(DirectCast(itemList, IEnumerable))

        End Function

        ''' <summary>Returns the smallest item from a list of parameters.</summary>
        Public Shared Function Minimum(Of T)(ByVal ParamArray itemList As T()) As T

            Return Minimum(Of T)(DirectCast(itemList, IEnumerable(Of T)))

        End Function

        ''' <summary>Returns the smallest item from the specified enumeration.</summary>
        Public Shared Function Minimum(Of T)(ByVal items As IEnumerable(Of T)) As T

            Dim minItem As T

            With items.GetEnumerator()
                If .MoveNext() Then
                    minItem = .Current
                    While .MoveNext()
                        If Compare(Of T)(.Current, minItem) < 0 Then minItem = .Current
                    End While
                End If
            End With

            Return minItem

        End Function

        ''' <summary>Returns the smallest item from the specified enumeration.</summary>
        Public Shared Function Minimum(ByVal items As IEnumerable) As Object

            Dim minItem As Object

            With items.GetEnumerator()
                If .MoveNext() Then
                    minItem = .Current
                    While .MoveNext()
                        If Compare(.Current, minItem) < 0 Then minItem = .Current
                    End While
                End If
            End With

            Return minItem

        End Function

        ''' <summary>Returns the largest item from a list of parameters.</summary>
        Public Shared Function Maximum(ByVal ParamArray itemList As Object()) As Object

            Return Maximum(DirectCast(itemList, IEnumerable))

        End Function

        ''' <summary>Returns the largest item from a list of parameters.</summary>
        Public Shared Function Maximum(Of T)(ByVal ParamArray itemList As T()) As T

            Return Maximum(Of T)(DirectCast(itemList, IEnumerable(Of T)))

        End Function

        ''' <summary>Returns the largest item from the specified enumeration.</summary>
        Public Shared Function Maximum(Of T)(ByVal items As IEnumerable(Of T)) As T

            Dim maxItem As T

            With items.GetEnumerator()
                If .MoveNext() Then
                    maxItem = .Current
                    While .MoveNext()
                        If Compare(Of T)(.Current, maxItem) > 0 Then maxItem = .Current
                    End While
                End If
            End With

            Return maxItem

        End Function

        ''' <summary>Returns the largest item from the specified enumeration.</summary>
        Public Shared Function Maximum(ByVal items As IEnumerable) As Object

            Dim maxItem As Object

            With items.GetEnumerator()
                If .MoveNext() Then
                    maxItem = .Current
                    While .MoveNext()
                        If Compare(.Current, maxItem) > 0 Then maxItem = .Current
                    End While
                End If
            End With

            Return maxItem

        End Function

        ''' <summary>Compares two elements of the specified type.</summary>
        Public Shared Function Compare(Of T)(ByVal x As T, ByVal y As T) As Integer

            Return System.Collections.Generic.Comparer(Of T).Default.Compare(x, y)

        End Function

        ''' <summary>Compares two elements of any type.</summary>
        Public Shared Function Compare(ByVal x As Object, ByVal y As Object) As Integer

            If IsReference(x) And IsReference(y) Then
                ' If both items are reference objects, then it tests object equality by reference.
                ' If not equal by overridable Object.Equals function, use default Comparer.
                If x Is y Then
                    Return 0
                ElseIf x.GetType().Equals(y.GetType()) Then
                    ' Compares two items that are the same type. Sees if the type supports IComparable interface.
                    If TypeOf x Is IComparable Then
                        Return DirectCast(x, IComparable).CompareTo(y)
                    ElseIf x.Equals(y) Then
                        Return 0
                    Else
                        Return Comparer.Default.Compare(x, y)
                    End If
                Else
                    Return Comparer.Default.Compare(x, y)
                End If
            Else
                ' Compares non-reference (i.e., value) types, using VB rules.
                ' ms-help://MS.VSCC.v80/MS.MSDN.v80/MS.VisualStudio.v80.en/dv_vbalr/html/d6cb12a8-e52e-46a7-8aaf-f804d634a825.htm
                Return IIf(x < y, -1, IIf(x > y, 1, 0))
            End If

        End Function

        ''' <summary>Compares two arrays.</summary>
        Public Shared Function CompareArrays(ByVal arrayA As Array, ByVal arrayB As Array) As Integer

            Return CompareArrays(arrayA, arrayB, Nothing)

        End Function

        ''' <summary>Compares two arrays.</summary>
        Public Shared Function CompareArrays(ByVal arrayA As Array, ByVal arrayB As Array, ByVal comparer As IComparer) As Integer

            If arrayA Is Nothing And arrayB Is Nothing Then
                Return 0
            ElseIf arrayA Is Nothing Then
                Return -1
            ElseIf arrayB Is Nothing Then
                Return 1
            Else
                If arrayA.Rank = 1 And arrayB.Rank = 1 Then
                    If arrayA.GetUpperBound(0) = arrayB.GetUpperBound(0) Then
                        Dim comparison As Integer

                        For x As Integer = 0 To arrayA.Length - 1
                            If comparer Is Nothing Then
                                comparison = Compare(arrayA.GetValue(x), arrayB.GetValue(x))
                            Else
                                comparison = comparer.Compare(arrayA.GetValue(x), arrayB.GetValue(x))
                            End If

                            If comparison <> 0 Then Exit For
                        Next

                        Return comparison
                    Else
                        ' For arrays that do not have the same number of elements, the array with most elements 
                        ' is assumed to be larger.
                        Return Compare(arrayA.GetUpperBound(0), arrayB.GetUpperBound(0))
                    End If
                Else
                    Throw New ArgumentException("Cannot compare multidimensional arrays")
                End If
            End If

        End Function

        ''' <summary>Changes the type of all elements in the source enumeration, and adds the conversion 
        ''' result to destination list.</summary>
        ''' <remarks>Items in source enumeration that are converted are added to destination list. The \
        ''' destination list is not cleared in advance.</remarks>
        Public Shared Sub ConvertList(ByVal source As IEnumerable, ByVal destination As IList, ByVal toType As System.Type)

            If source Is Nothing Then Throw New ArgumentNullException("Source list is null")
            If destination Is Nothing Then Throw New ArgumentNullException("Destination list is null")
            If destination.IsReadOnly Then Throw New ArgumentException("Cannot add items to a read only list")
            If destination.IsFixedSize Then Throw New ArgumentException("Cannot add items to a fixed size list")

            For Each Item As Object In source
                destination.Add(Convert.ChangeType(Item, toType))
            Next

        End Sub

        ''' <summary>Converts a list (i.e., any collection implementing IList) to an array.</summary>
        Public Shared Function ListToArray(ByVal sourceList As IList, ByVal toType As System.Type) As Array

            Dim destination As Array = Array.CreateInstance(toType, sourceList.Count)

            ConvertList(sourceList, destination, toType)

            Return destination

        End Function

        ''' <summary>Converts an array to a string, using the default delimeter ("|") that can later be 
        ''' converted back to array using StringToArray.</summary>
        ''' <remarks>
        ''' This function is a semantic reference to the ListToString function (the Array class implements 
        ''' IEnumerable) and is only provided for the sake of completeness.
        ''' </remarks>
        Public Shared Function ArrayToString(ByVal source As Array) As String

            Return ListToString(source)

        End Function

        ''' <summary>Converts an array to a string that can later be converted back to array using StringToArray.</summary>
        ''' <remarks>
        ''' This function is a semantic reference to the ListToString function (the Array class implements 
        ''' IEnumerable) and is only provided for the sake of completeness.
        ''' </remarks>
        Public Shared Function ArrayToString(ByVal source As Array, ByVal delimeter As Char) As String

            Return ListToString(source, delimeter)

        End Function

        ''' <summary>Converts an enumeration to a string, using the default delimeter ("|") that can later be 
        ''' converted back to array using StringToList.</summary>
        Public Shared Function ListToString(ByVal source As IEnumerable) As String

            Return ListToString(source, "|"c)

        End Function

        ''' <summary>Converts an enumeration to a string that can later be converted back to array using 
        ''' StringToList.</summary>
        Public Shared Function ListToString(ByVal source As IEnumerable, ByVal delimeter As Char) As String

            If source Is Nothing Then Throw New ArgumentNullException("Source list is null")

            With New StringBuilder
                For Each item As Object In source
                    If .Length > 0 Then .Append(delimeter)
                    .Append(item.ToString())
                Next

                Return .ToString()
            End With

        End Function

        ''' <summary>Converts a string, created with ArrayToString, using the default delimeter ("|") back into 
        ''' an array.</summary>
        Public Shared Function StringToArray(ByVal source As String, ByVal toType As System.Type) As Array

            Return StringToArray(source, toType, "|"c)

        End Function

        ''' <summary>Converts a string, created with ArrayToString, back into an array.</summary>
        Public Shared Function StringToArray(ByVal source As String, ByVal toType As System.Type, ByVal delimeter As Char) As Array

            Dim items As New ArrayList

            StringToList(source, items, delimeter)

            Return ListToArray(items, toType)

        End Function

        ''' <summary>Appends items parsed from delimited string, created with ArrayToString or ListToString, 
        ''' using the default delimeter ("|") into the given list.</summary>
        ''' <remarks>Items that are converted are added to destination list. The destination list is not 
        ''' cleared in advance.</remarks>
        Public Shared Sub StringToList(ByVal source As String, ByVal destination As IList)

            StringToList(source, destination, "|"c)

        End Sub

        ''' <summary>Appends items parsed from delimited string, created with ArrayToString or ListToString, 
        ''' into the given list.</summary>
        ''' <remarks>Items that are converted are added to destination list. The destination list is not 
        ''' cleared in advance.</remarks>
        Public Shared Sub StringToList(ByVal source As String, ByVal destination As IList, ByVal delimeter As Char)

            If source Is Nothing Then Exit Sub
            If destination Is Nothing Then Throw New ArgumentNullException("Destination list is null")
            If destination.IsFixedSize Then Throw New ArgumentException("Cannot add items to a fixed size list")
            If destination.IsReadOnly Then Throw New ArgumentException("Cannot add items to a read only list")

            For Each item As String In source.Split(delimeter)
                If Not String.IsNullOrEmpty(item) Then
                    item = item.Trim
                    If item.Length > 0 Then destination.Add(item)
                End If
            Next

        End Sub

        ''' <summary>Rearranges all the elements in the array into a random order.</summary>
        ''' <remarks>
        ''' <para>
        ''' This function is a semantic reference to the ScrambleList function (the Array class implements 
        ''' IList) and is only provided for the sake of completeness.
        ''' </para>
        ''' <para>This function uses a cryptographically strong random number generator to perform the scramble.</para>
        ''' </remarks>
        Public Shared Sub ScrambleArray(ByVal source As Array)

            ScrambleList(source)

        End Sub

        ''' <summary>Rearranges all the elements in the list (i.e., any collection implementing IList) into 
        ''' a random order.</summary>
        ''' <remarks>This function uses a cryptographically strong random number generator to perform the 
        ''' scramble.</remarks>
        Public Shared Sub ScrambleList(ByVal source As IList)

            If source Is Nothing Then Throw New ArgumentNullException("Source list is null")
            If source.IsReadOnly Then Throw New ArgumentException("Cannot modify items in a read only list")

            Dim x, y As Integer
            Dim currentItem As Object

            ' Mixes up the data in random order.
            For x = 0 To source.Count - 1
                ' Calls random function in Math namespace.
                y = RandomInt32Between(0, source.Count - 1)

                If x <> y Then
                    ' Swaps items
                    currentItem = source(x)
                    source(x) = source(y)
                    source(y) = currentItem
                End If
            Next

        End Sub

    End Class

End Namespace
