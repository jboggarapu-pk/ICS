Imports System.Data
Imports System.Data.OracleClient
Imports System.Configuration
Imports CooperTire.ICS.Common
Imports CooperTire.ICS.DomainEntities


''' <summary>
''' Certificate default values data access methods
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
''' <para>08/16/2019</para>
''' <para>Original Code.</para>
''' </description>
''' </item>
''' </list>
''' </remarks>
Public Class DefaultValuesDalc
    Inherits CooperTire.ICS.CTS.DALC.CtsDalc

#Region "DefaultValuesDalc Variables"
    ''' <summary>
    ''' variable to hold the value 1
    ''' </summary>
    ''' <remarks></remarks>
    Private OneNum As Short = 1
#End Region
#Region "DefaultValuesDalc Methods"


    ''' <summary>
    ''' Saves certificate default values
    ''' </summary>
    ''' <param name="p_certDeftValues">Certificate default values</param>
    ''' <param name="p_certificateNo">Certificate number</param>
    ''' <returns>Returns true or false based on save operation </returns>
    ''' <exception cref="Exception">
    ''' Exception will be logged to ICS Log
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
    ''' <para>08/16/2019</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function CertificateDefaultValueSave(ByVal p_certDeftValues As List(Of CertificationDefaultField), ByVal p_certificateNo As String) As Boolean

        Dim blnSaved As Boolean = False
        Dim oraCmd As New OracleCommand
        Const CertificateDefValueSave As String = "certification_crud.CertificateDefaultvalue_Save"
        Const FieldValueId As String = "pi_FieldvalueId"
        Const CertificationTypeId As String = "pi_CertificationTypeID"
        Const FieldValue As String = "ps_FieldValue"
        Const CertificateNumber As String = "ps_CertificateNumber"

        Try
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = CertificateDefValueSave

            Connect()
            oraCmd.Connection = Connection
            For Each defaultValue As CertificationDefaultField In p_certDeftValues
                ParametersHelper.AddParametersToCommand(FieldValueId, ParameterDirection.Input, OracleType.Number, defaultValue.ID, oraCmd)
                ParametersHelper.AddParametersToCommand(CertificationTypeId, ParameterDirection.Input, OracleType.Number, defaultValue.CertificateTypeId, oraCmd)
                ParametersHelper.AddParametersToCommand(FieldValue, ParameterDirection.Input, OracleType.VarChar, defaultValue.Value, oraCmd)
                ParametersHelper.AddParametersToCommand(CertificateNumber, ParameterDirection.Input, OracleType.VarChar, p_certificateNo, oraCmd)

                Dim rowsaffected As Integer = oraCmd.ExecuteNonQuery()
                blnSaved = (rowsaffected = OneNum)
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
    ''' Saves certification type default values
    ''' </summary>
    ''' <param name="p_certDeftValues">Certificate default values</param>
    ''' <returns>Returns true or false based on save operation </returns>
    ''' <exception cref="Exception">
    ''' Exception will be logged to ICS Log
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
    ''' <para>08/16/2019</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function CertificationTypeDefaultValueSave(ByVal p_certDeftValues As List(Of CertificationDefaultField)) As Boolean

        Dim blnSaved As Boolean = False
        Dim oraCmd As New OracleCommand
        Const CertificTypeDefaultValueSave As String = "certification_crud.CertificTypeDefaultValue_Save"
        Const FieldValueId As String = "pi_FieldvalueId"
        Const CertificationTypeId As String = "pi_CertificationTypeID"
        Const FieldValue As String = "ps_FieldValue"

        Try
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = CertificTypeDefaultValueSave

            Connect()
            oraCmd.Connection = Connection
            For Each defaultValue As CertificationDefaultField In p_certDeftValues
                ParametersHelper.AddParametersToCommand(FieldValueId, ParameterDirection.Input, OracleType.Number, defaultValue.ID, oraCmd)
                ParametersHelper.AddParametersToCommand(CertificationTypeId, ParameterDirection.Input, OracleType.Number, defaultValue.CertificateTypeId, oraCmd)
                ParametersHelper.AddParametersToCommand(FieldValue, ParameterDirection.Input, OracleType.VarChar, defaultValue.Value, oraCmd)

                Dim rowsaffected As Integer = oraCmd.ExecuteNonQuery()
                blnSaved = (rowsaffected = OneNum)
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
    ''' Gets certificate or type default values
    ''' </summary>
    ''' <param name="p_strCertificateType">Certificate type</param>
    ''' <param name="p_intCertificateNumberID">Certificate number id</param>
    ''' <param name="p_strCertificateNumber">Certificate number</param>
    ''' <returns>Returns default values as data set </returns>
    ''' <exception cref="Exception">
    ''' Exception will be logged to ICS Log
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
    ''' <para>08/16/2019</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Function GetDefaultValues(ByVal p_strCertificateType As String, _
                                     ByVal p_intCertificateNumberID As Integer, _
                                     ByRef p_strCertificateNumber As String) As DataSet

        Dim dstResults As New DataSet
        Dim oraCmd As New OracleCommand
        Dim oraAdp As New OracleDataAdapter
        Const RetCursor As String = "pc_retCursor"
        Const PsNumber As String = "ps_Number"
        Const TypeName As String = "ps_TypeName"
        Const NumberId As String = "pi_NumberID"
        Const GetDefValues As String = "certification_crud.GetDefaultValues"

        Try
            oraCmd.Parameters.Clear()

            'pc_retCursor out retCursor,
            ParametersHelper.AddParametersToCommand(RetCursor, ParameterDirection.Output, OracleType.Cursor, Nothing, oraCmd)
            'ps_Number out CERTIFICATE.CERTIFICATENUMBER%TYPE,
            ParametersHelper.AddParametersToCommand(PsNumber, ParameterDirection.Output, OracleType.VarChar, 8000, Nothing, oraCmd)
            'ps_TypeName in varchar2
            ParametersHelper.AddParametersToCommand(TypeName, ParameterDirection.Input, OracleType.VarChar, 8000, p_strCertificateType, oraCmd)
            'pi_NumberID in number
            ParametersHelper.AddParametersToCommand(NumberId, ParameterDirection.Input, OracleType.Number, p_intCertificateNumberID, oraCmd)

            'configure the command object
            oraCmd.CommandType = CommandType.StoredProcedure
            oraCmd.CommandText = GetDefValues

            Connect()
            oraCmd.Connection = Connection

            'Get the data
            oraAdp.SelectCommand = oraCmd
            oraAdp.Fill(dstResults)

            If Not oraCmd.Parameters.Item(PsNumber).Value.Equals(DBNull.Value) Then
                p_strCertificateNumber = CStr(oraCmd.Parameters.Item(PsNumber).Value)
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
#End Region
End Class
