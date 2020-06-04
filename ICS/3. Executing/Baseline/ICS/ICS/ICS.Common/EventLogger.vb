Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling

''' <summary>
''' Log exception data to ICS log
''' </summary>
''' <remarks></remarks>
Public Class EventLogger

    ''' <summary>
    ''' Enter record to ICS DB log or Windows Event Log
    ''' </summary>
    ''' <param name="exp"></param>
    ''' <remarks></remarks>
    Public Shared Sub Enter(ByVal exp As Exception)

        Dim dalcLogger As EventLoggerDalc = New EventLoggerDalc
        ' Log exception to ICS DB
        Dim blnLogged As Boolean = dalcLogger.LogToDB(exp)
        If (Not blnLogged) Then
            ' Log exception as configured (e.g., Windows Event Log)
            ExceptionPolicy.HandleException(exp, "Exception Policy")
        End If

    End Sub

End Class
