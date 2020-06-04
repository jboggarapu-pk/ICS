Imports System.Data
Imports System.Data.OracleClient
Imports System.Configuration

''' <summary>
''' Log exception data to CTS compliant ICS DB
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
Public NotInheritable Class EventLoggerDalc
    Inherits CooperTire.ICS.CTS.DALC.CtsDalc

#Region "Exception logging to DB"

    ''' <summary>
    ''' Insert record into ICS Log table
    ''' </summary>
    ''' <param name="exp">Exception object</param>
    ''' <returns>Success flag True or False.</returns>
    ''' <exception cref="Exception">
    ''' Returns False value
    ''' </exception>
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
    Function LogToDB(ByVal exp As Exception) As System.Boolean
        Const SpAppMessageInsert As String = "APP_MESSAGE_OPERATIONS.App_Message_Insert"
        Const AsMachineId As String = "AS_MACHINEID"
        Const AdOperatorId As String = "AD_OPERATORID"
        Const AdDateRecorded As String = "AD_DATERECORDED"
        Const AsProcessName As String = "AS_PROCESSNAME"
        Const AxRecordData As String = "Ax_RECORDDATA"
        Const AsMessageCode As String = "As_MESSAGECODE"
        Const MessageCodeValue As String = "1111"
        Const AsMessage As String = "AS_MESSAGE"

        Dim blnLogged As Boolean = False
        Dim oraCmd As New OracleCommand

        Try
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = SpAppMessageInsert

            Dim opMachineid As New OracleParameter(AsMachineId, System.Net.Dns.GetHostName())
            opMachineid.Direction = ParameterDirection.Input
            opMachineid.OracleType = OracleType.VarChar
            oraCmd.Parameters.Add(opMachineid)

            Dim opOperatorID As New OracleParameter(AdOperatorId, System.Net.Dns.GetHostName())
            opOperatorID.Direction = ParameterDirection.Input
            opOperatorID.OracleType = OracleType.VarChar
            oraCmd.Parameters.Add(opOperatorID)

            Dim opDATERECORDED As New OracleParameter(AdDateRecorded, DateTime.Now)
            opDATERECORDED.Direction = ParameterDirection.Input
            opDATERECORDED.DbType = DbType.Date
            oraCmd.Parameters.Add(opDATERECORDED)

            Dim process As System.Diagnostics.Process = System.Diagnostics.Process.GetCurrentProcess

            Dim opPROCESSNAME As New OracleParameter(AsProcessName, process.ProcessName)
            opPROCESSNAME.Direction = ParameterDirection.Input
            opOperatorID.OracleType = OracleType.VarChar
            oraCmd.Parameters.Add(opPROCESSNAME)

            Dim param1 As New OracleParameter(AxRecordData, exp.ToString())
            param1.Direction = ParameterDirection.Input
            param1.OracleType = OracleType.VarChar
            oraCmd.Parameters.Add(param1)

            Dim opMESSAGECODE As New OracleParameter(AsMessageCode, MessageCodeValue)
            opMESSAGECODE.Direction = ParameterDirection.Input
            opMESSAGECODE.OracleType = OracleType.VarChar
            opMESSAGECODE.Size = 10
            oraCmd.Parameters.Add(opMESSAGECODE)

            Dim opMESSAGE As New OracleParameter(AsMessage, exp.Message)
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
