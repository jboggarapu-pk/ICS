Imports CooperTire.ICS.DomainEntities
Imports CooperTire.ICS.Common
Imports CooperTire.ICS.DepositoryTender


''' <summary>
'''  Class containing Model to handle Certificate/Certificate Type default values
''' </summary>
''' <remarks>
''' <list type="table">
''' <listheader>
''' <term>Author</term>
''' <description>Description</description>
''' </listheader>
''' <item>
''' <term>N/A</term>
''' <description>
''' <para>N/A</para>
''' <para>Original Code.</para>
''' </description>
''' </item>
''' <item>
''' <term>Sujitha</term>
''' <description>
''' <para>09/26/2019</para>
''' <para>Implemented Coding Standards</para>
''' </description>
''' </item> 
''' </list>
''' </remarks> 
Public Class DefaultFieldModel

    ''' <summary>
    '''  Method to Updates the CertificateDefault values
    ''' </summary>
    ''' <returns>Boolean</returns> 
    ''' <param name="p_certDeftValues">Certificate Default Value</param>
    ''' <exception cref="Exception">
    ''' Throws exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>09/26/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>  
    Public Function CertificateDefaultvalueSave(ByVal p_certDeftValues As List(Of CertificationDefaultField)) As Boolean
        Try
            Return Depository.Current.CertificateDefaultvalueSave(p_certDeftValues)
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method to Updates the Certificate Values
    ''' </summary>
    ''' <returns>NameAid.SaveResult</returns> 
    ''' <param name="p_certDeftValues">Certificate Default Values</param>
    ''' <param name="p_certificateNo">Certificate Number</param>
    ''' <exception cref="Exception">
    ''' Throws exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>09/26/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Public Function CertificateValueSave(ByVal p_certDeftValues As List(Of CertificationDefaultField), ByVal p_certificateNo As String) As Boolean
        Try
            Return Depository.Current.CertificateValueSave(p_certDeftValues, p_certificateNo)
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method to Retrieve data from CertificateType default values if no certificate number ID is passed
    '''  If a certificate number ID is passed then certificate default values will be retrieved!
    ''' </summary>
    ''' <returns>NameAid.SaveResult</returns> 
    ''' <param name="p_strCertificateType"></param>
    ''' <param name="p_intCertificateNumberID"></param>
    ''' <param name="p_strCertificateNumber">output parameter</param>
    ''' <exception cref="Exception">
    ''' Throws exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>09/26/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Public Function GetDefaultValues(ByVal p_strCertificateType As String, ByVal p_intCertificateNumberID As Integer, _
                                    ByRef p_strCertificateNumber As String) As List(Of CertificationDefaultField)
        Try
            Dim listCertDefaultFields As New List(Of CertificationDefaultField)
            Dim dstDefaultValues As DataSet = Depository.Current.GetDefaultValues(p_strCertificateType, p_intCertificateNumberID, p_strCertificateNumber)

            listCertDefaultFields = PopulateList(dstDefaultValues)

            If listCertDefaultFields.Count = 0 AndAlso p_strCertificateNumber.Length > 0 Then
                Dim strTemp As String = String.Empty ' To preserve previously obtained CertificateNumber
                dstDefaultValues = Depository.Current.GetDefaultValues(p_strCertificateType, 0, strTemp)
                listCertDefaultFields = PopulateList(dstDefaultValues)
            End If

            Return listCertDefaultFields
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method to Populate default values list
    ''' </summary>
    ''' <returns>NameAid.SaveResult</returns> 
    ''' <param name="p_dstDefaultValues">Default Values</param>
    ''' <exception cref="Exception">
    ''' Throws exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>09/26/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Private Function PopulateList(ByVal p_dstDefaultValues As DataSet) As List(Of CertificationDefaultField)
        Try
            Dim listCertificationDefaultField As New List(Of CertificationDefaultField)
            Dim objcertificationDefaultField As CertificationDefaultField = Nothing

            For Each row As DataRow In p_dstDefaultValues.Tables(0).Rows
                objcertificationDefaultField = New CertificationDefaultField
                objcertificationDefaultField.ID = CInt(row(NameAid.Column.FieldId))
                If row.IsNull(NameAid.Column.CertificateID) Then
                    objcertificationDefaultField.CertificateId = 0
                Else
                    objcertificationDefaultField.CertificateId = CInt(row(NameAid.Column.CertificateID))
                End If

                objcertificationDefaultField.CertificateTypeId = CInt(row(NameAid.Column.CertificationTypeId))
                objcertificationDefaultField.Text = CStr(row(NameAid.Column.FieldText))
                objcertificationDefaultField.Name = CStr(row(NameAid.Column.FieldName))

                If row.IsNull(NameAid.Column.ReportName) Then
                    objcertificationDefaultField.Report = String.Empty
                Else
                    objcertificationDefaultField.Report = CStr(row(NameAid.Column.ReportName))
                End If

                If row.IsNull(NameAid.Column.FieldValue) Then
                    objcertificationDefaultField.Value = String.Empty
                Else
                    objcertificationDefaultField.Value = CStr(row(NameAid.Column.FieldValue))
                End If

                listCertificationDefaultField.Add(objcertificationDefaultField)
            Next

            Return listCertificationDefaultField
        Catch ex As Exception
            Throw
        End Try
    End Function
    ''' <summary>
    '''  Method to get Certification Name.
    ''' </summary>
    ''' <returns>NameAid.SaveResult</returns> 
    ''' <param name="p_intCertificateid">CertificateId.</param>
    ''' <exception cref="Exception">
    ''' Throws exception if any error occurs.
    ''' </exception>
    ''' <remarks>
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Author</term>
    ''' <description>Description</description>
    ''' </listheader>
    ''' <item>
    ''' <term>N/A</term>
    ''' <description>
    ''' <para>N/A</para>
    ''' <para>Original Code.</para>
    ''' </description>
    ''' </item>
    ''' <item>
    ''' <term>Sujitha</term>
    ''' <description>
    ''' <para>09/26/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks> 
    Public Function GetCertificationName(ByVal p_intCertificateid As Integer) As String
        Try
            Return Depository.Current.GetCertificationNameByID(p_intCertificateid)
        Catch ex As Exception
            Throw
        End Try
    End Function

End Class
