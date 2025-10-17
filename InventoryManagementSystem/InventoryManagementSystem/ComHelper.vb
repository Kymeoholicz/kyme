Imports System.Runtime.InteropServices

Module ComHelper
    ''' <summary>
    ''' Safely releases a COM object and sets it to Nothing.
    ''' </summary>
    ''' <param name="comObj">The COM object to release.</param>
    Public Sub SafeReleaseComObject(ByRef comObj As Object)
        If comObj IsNot Nothing AndAlso Marshal.IsComObject(comObj) Then
            Marshal.FinalReleaseComObject(comObj)
            comObj = Nothing
        End If
    End Sub
End Module
