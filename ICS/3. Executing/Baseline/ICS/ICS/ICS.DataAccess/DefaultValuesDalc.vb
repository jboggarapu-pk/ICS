Imports System.Data
Imports System.Data.OracleClient
Imports System.Configuration
Imports CooperTire.ICS.Common
Imports CooperTire.ICS.DomainEntities

''' <summary>
''' Certificate default values data access methods
''' </summary>
''' <remarks></remarks>
Public Class DefaultValuesDalc
    Inherits CooperTire.ICS.CTS.DALC.CtsDalc

    ''' <summary>
    ''' Save certificate default values
    ''' </summary>
    ''' <param name="p_certDeftValues"></param>
    ''' <param name="p_certificateNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CertificateDefaultValueSave(ByVal p_certDeftValues As List(Of CertificationDefaultField), ByVal p_certificateNo As String) As Boolean

        Dim blnSaved As Boolean = False
        Dim oraCmd As New OracleCommand

        Try
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "certification_crud.CertificateDefaultvalue_Save"

            Connect()
            oraCmd.Connection = Connection
            For Each defaultValue As CertificationDefaultField In p_certDeftValues
                ParametersHelper.AddParametersToCommand("pi_FieldvalueId", ParameterDirection.Input, OracleType.Number, defaultValue.ID, oraCmd)
                ParametersHelper.AddParametersToCommand("pi_CertificationTypeID", ParameterDirection.Input, OracleType.Number, defaultValue.CertificateTypeId, oraCmd)
                ParametersHelper.AddParametersToCommand("ps_FieldValue", ParameterDirection.Input, OracleType.VarChar, defaultValue.Value, oraCmd)
                ParametersHelper.AddParametersToCommand("ps_CertificateNumber", ParameterDirection.Input, OracleType.VarChar, p_certificateNo, oraCmd)

                Dim rowsaffected As Integer = oraCmd.ExecuteNonQuery()
                blnSaved = (rowsaffected = 1)
                If Not blnSaved Then Exit For

                oraCmd.Parameters.Clear()
            Next
        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw exp
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
        End Try

        Return blnSaved

    End Function

    ''' <summary>
    ''' Save certification type default values
    ''' </summary>
    ''' <param name="p_certDeftValues"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CertificationTypeDefaultValueSave(ByVal p_certDeftValues As List(Of CertificationDefaultField)) As Boolean

        Dim blnSaved As Boolean = False
        Dim oraCmd As New OracleCommand

        Try
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "certification_crud.CertificTypeDefaultValue_Save"

            Connect()
            oraCmd.Connection = Connection
            For Each defaultValue As CertificationDefaultField In p_certDeftValues
                ParametersHelper.AddParametersToCommand("pi_FieldvalueId", ParameterDirection.Input, OracleType.Number, defaultValue.ID, oraCmd)
                ParametersHelper.AddParametersToCommand("pi_CertificationTypeID", ParameterDirection.Input, OracleType.Number, defaultValue.CertificateTypeId, oraCmd)
                ParametersHelper.AddParametersToCommand("ps_FieldValue", ParameterDirection.Input, OracleType.VarChar, defaultValue.Value, oraCmd)

                Dim rowsaffected As Integer = oraCmd.ExecuteNonQuery()
                blnSaved = (rowsaffected = 1)
                If Not blnSaved Then Exit For

                oraCmd.Parameters.Clear()
            Next
        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw exp
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
        End Try

        Return blnSaved

    End Function

    ''' <summary>
    ''' Get certificate or type default values
    ''' </summary>
    ''' <param name="p_strCertificateType"></param>
    ''' <param name="p_strCertificateNumber"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDefaultValues(ByVal p_strCertificateType As String, _
                                     ByVal p_intCertificateNumberID As Integer, _
                                     ByRef p_strCertificateNumber As String) As DataSet

        Dim dstResults As New DataSet
        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter

        Try
            oraCmd.Parameters.Clear()

            'pc_retCursor out retCursor,
            ParametersHelper.AddParametersToCommand("pc_retCursor", ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'ps_Number out CERTIFICATE.CERTIFICATENUMBER%TYPE,
            ParametersHelper.AddParametersToCommand("ps_Number", ParameterDirection.Output, OracleType.VarChar, 8000, Nothing, oraCmd)
            'ps_TypeName in varchar2
            ParametersHelper.AddParametersToCommand("ps_TypeName", ParameterDirection.Input, OracleType.VarChar, 8000, p_strCertificateType, oraCmd)
            'pi_NumberID in number
            ParametersHelper.AddParametersToCommand("pi_NumberID", ParameterDirection.Input, OracleType.Number, p_intCertificateNumberID, oraCmd)



            'configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = "certification_crud.GetDefaultValues"

            Connect()
            oraCmd.Connection = Connection

            'Get the data
            oraAdp.SelectCommand = oraCmd
            oraAdp.Fill(dstResults)

            If Not oraCmd.Parameters.Item("ps_Number").Value.Equals(DBNull.Value) Then
                p_strCertificateNumber = oraCmd.Parameters.Item("ps_Number").Value
            End If

        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw exp
        Finally
            ' Ensures connection is closed in the event of an exception
            Disconnect()
            oraCmd.Dispose()
            oraAdp.Dispose()
        End Try

        Return dstResults

    End Function

End Class
