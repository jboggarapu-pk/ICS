Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

''' <summary>
''' Log exception data to ICS log
''' </summary>
''' <remarks>
''' <list type="table">
''' <listheader>
''' <term>Author</term>
''' <description>Description</description>
''' </listheader>
''' <item>
''' <term>NA</term>
''' <description>
''' <para>NA</para>
''' <para>Original Code.</para>
''' </description>
''' </item>
''' <item>
''' <term>Srinivas S</term>
''' <description>
''' <para>09/18/2019</para>
''' <para>Implemented code standardization.</para>
''' </description>
''' </item>
''' </list>
''' </remarks>
Public Class EventLogger

    ''' <summary>
    ''' Enter record to ICS DB log or Windows Event Log
    ''' </summary>
    ''' <param name="exp">Exception object.</param>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>NA</term>
    ''' <description>
    ''' <para>NA</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Srinivas S</term>
    ''' <description>
    ''' <para>09/18/2019</para>
    ''' <para>Implemented code standardization.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Shared Sub Enter(ByVal exp As Exception)
        Const ExceptionPolicyText As String = "Exception Policy"
        Dim dalcLogger As EventLoggerDalc = New EventLoggerDalc
        ' Log exception to ICS DB
        Dim blnLogged As Boolean = dalcLogger.LogToDB(exp)
        If (Not blnLogged) Then
            ' Log exception as configured (e.g., Windows Event Log)
            ExceptionPolicy.HandleException(exp, ExceptionPolicyText)
        End If

    End Sub

End Class
