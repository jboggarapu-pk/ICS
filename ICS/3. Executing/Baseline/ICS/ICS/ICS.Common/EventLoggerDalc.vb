Imports System.Data
Imports System.Data.OracleClient
Imports System.Configuration

''' <summary>
''' Log exception data to CTS compliant ICS DB
''' </summary>
''' <remarks>
''' </remarks>
Public NotInheritable Class EventLoggerDalc
    Inherits CooperTire.ICS.CTS.DALC.CtsDalc

#Region "Exception logging to DB"

    ''' <summary>
    ''' Insert record into ICS Log table
    ''' </summary>
    ''' <param name="exp"></param>
    ''' <returns>Success flag</returns>
    ''' <remarks></remarks>
    Function LogToDB(ByVal exp As Exception) As System.Boolean

        Dim blnLogged As Boolean = False
        Dim oraCmd As New OracleCommand

        Try
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "APP_MESSAGE_OPERATIONS.App_Message_Insert"

            Dim opMachineid As New OracleParameter("AS_MACHINEID", System.Net.Dns.GetHostName())
            opMachineid.Direction = ParameterDirection.Input
            opMachineid.DbType = OracleType.VarChar
            oraCmd.Parameters.Add(opMachineid)

            Dim opOperatorID As New OracleParameter("AD_OPERATORID", System.Net.Dns.GetHostName())
            opOperatorID.Direction = ParameterDirection.Input
            opOperatorID.DbType = OracleType.VarChar
            oraCmd.Parameters.Add(opOperatorID)

            Dim opDATERECORDED As New OracleParameter("AD_DATERECORDED", DateTime.Now)
            opDATERECORDED.Direction = ParameterDirection.Input
            opDATERECORDED.DbType = DbType.Date
            oraCmd.Parameters.Add(opDATERECORDED)

            Dim process As System.Diagnostics.Process = System.Diagnostics.Process.GetCurrentProcess

            Dim opPROCESSNAME As New OracleParameter("AS_PROCESSNAME", process.ProcessName)
            opPROCESSNAME.Direction = ParameterDirection.Input
            opOperatorID.DbType = OracleType.VarChar
            oraCmd.Parameters.Add(opPROCESSNAME)

            Dim param1 As New OracleParameter("Ax_RECORDDATA", exp.ToString())
            param1.Direction = ParameterDirection.Input
            param1.DbType = OracleType.VarChar
            oraCmd.Parameters.Add(param1)

            Dim opMESSAGECODE As New OracleParameter("As_MESSAGECODE", "1111")
            opMESSAGECODE.Direction = ParameterDirection.Input
            opMESSAGECODE.OracleType = OracleType.VarChar
            opMESSAGECODE.Size = 10
            oraCmd.Parameters.Add(opMESSAGECODE)

            Dim opMESSAGE As New OracleParameter("AS_MESSAGE", exp.Message)
            opMESSAGE.Direction = ParameterDirection.Input
            opMESSAGE.OracleType = OracleType.VarChar
            opMESSAGE.Size = 4000
            oraCmd.Parameters.Add(opMESSAGE)

            Connect()
            oraCmd.Connection = Connection

            Dim rowsaffected As Integer = oraCmd.ExecuteNonQuery()
            If rowsaffected = 1 Then
                blnLogged = True
            End If
        Catch ex As Exception
            blnLogged = False
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
        End Try

        Return blnLogged
    End Function

#End Region

End Class
