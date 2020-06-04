Imports CooperTire.ICS.DomainEntities
Imports CooperTire.ICS.Common
Imports CooperTire.ICS.DepositoryTender

''' <summary>
''' Model to handle Certificate/Certificate Type default values
''' </summary>
''' <remarks></remarks>
Public Class DefaultFieldModel

    ''' <summary>
    ''' Updates the CertificateDefault values
    ''' </summary>
    ''' <param name="p_certDeftValues"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CertificateDefaultvalueSave(ByVal p_certDeftValues As List(Of CertificationDefaultField)) As Boolean

        Return Depository.Current.CertificateDefaultvalueSave(p_certDeftValues)

    End Function

    ''' <summary>
    ''' Updates the Certificate Values
    ''' </summary>
    ''' <param name="p_certDeftValues"></param>
    ''' <param name="p_certificateNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CertificateValueSave(ByVal p_certDeftValues As List(Of CertificationDefaultField), ByVal p_certificateNo As String) As Boolean

        Return Depository.Current.CertificateValueSave(p_certDeftValues, p_certificateNo)

    End Function

    ''' <summary>
    '''  Retrieve data from CertificateType default values if no ceritificate number ID is passed
    '''  If a certificate number ID is passed then certificate default values will be retrieved!
    ''' </summary>
    ''' <param name="p_strCertificateType"></param>
    ''' <param name="p_intCertificateNumberID"></param>
    ''' <param name="p_strCertificateNumber">output param</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetDefaultValues(ByVal p_strCertificateType As String, ByVal p_intCertificateNumberID As Integer, _
                                    ByRef p_strCertificateNumber As String) As List(Of CertificationDefaultField)

        Dim listCertDefaultFields As New List(Of CertificationDefaultField)
        Dim dstDefaultValues As DataSet = Depository.Current.GetDefaultValues(p_strCertificateType, p_intCertificateNumberID, p_strCertificateNumber)

        listCertDefaultFields = PopulateList(dstDefaultValues)

        If listCertDefaultFields.Count = 0 AndAlso p_strCertificateNumber.Length > 0 Then
            Dim strTemp As String = String.Empty ' To preserv previously obtained CertificateNumber
            dstDefaultValues = Depository.Current.GetDefaultValues(p_strCertificateType, 0, strTemp)
            listCertDefaultFields = PopulateList(dstDefaultValues)
        End If

        Return listCertDefaultFields

    End Function

    ''' <summary>
    ''' Populate default values list
    ''' </summary>
    ''' <param name="p_dstDefaultValues"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PopulateList(ByVal p_dstDefaultValues As DataSet) As List(Of CertificationDefaultField)

        Dim listCertificationDefaultField As New List(Of CertificationDefaultField)
        Dim objcertificationDefaultField As CertificationDefaultField = Nothing

        For Each row As DataRow In p_dstDefaultValues.Tables(0).Rows
            objcertificationDefaultField = New CertificationDefaultField
            objcertificationDefaultField.ID = row(NameAid.Column.FieldId)
            If row.IsNull(NameAid.Column.CertificateID) Then
                objcertificationDefaultField.CertificateId = 0
            Else
                objcertificationDefaultField.CertificateID = row(NameAid.Column.CertificateID)
            End If

            objcertificationDefaultField.CertificateTypeId = row(NameAid.Column.CertificationTypeId)
            objcertificationDefaultField.Text = row(NameAid.Column.FieldText)
            objcertificationDefaultField.Name = row(NameAid.Column.FieldName)

            If row.IsNull(NameAid.Column.ReportName) Then
                objcertificationDefaultField.Report = String.Empty
            Else
                objcertificationDefaultField.Report = row(NameAid.Column.ReportName)
            End If

            If row.IsNull(NameAid.Column.FieldValue) Then
                objcertificationDefaultField.Value = String.Empty
            Else
                objcertificationDefaultField.Value = row(NameAid.Column.FieldValue)
            End If

            listCertificationDefaultField.Add(objcertificationDefaultField)
        Next

        Return listCertificationDefaultField

    End Function
    Public Function GetCertificationName(ByVal p_intCertificateid As Integer) As String
        Return Depository.Current.GetCertificationNameByID(p_intCertificateid)
    End Function

End Class
