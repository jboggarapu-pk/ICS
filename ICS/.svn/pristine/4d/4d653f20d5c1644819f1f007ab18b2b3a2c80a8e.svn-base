Imports CooperTire.ICS.Common
Imports CooperTire.ICS.DepositoryTender

''' <summary>
''' Certification action model for actions "Rename", "Delete", "Detach/Move" and "Dup Correct"
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
''' </list>
''' </remarks> 
'''Added as per project 2706 technical specification
Public Class CertificationActionModel

#Region "Methods"

    ''' <summary>
    '''  Method to Gets the certified materials count.
    ''' </summary>
    ''' <param name="p_intCertificationTypeId">Certification Type Id</param>
    ''' <param name="p_strCertificateNumber">Certificate Number</param>
    ''' <param name="p_strCertificateExtension">Certificate Extension</param>
    ''' <returns>Certified materials count as integer</returns>
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
    ''' <para>09/25/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>     
    Public Function GetCertifiedMaterialCount(ByVal p_intCertificationTypeId As Integer, _
                                              ByVal p_strCertificateNumber As String, _
                                              ByVal p_strCertificateExtension As String) As Integer
        Try
            Return Depository.Current.GetCertifiedMaterialCount(p_intCertificationTypeId, _
                                                            p_strCertificateNumber, _
                                                            p_strCertificateExtension)
        Catch ex As Exception
            Throw
        End Try
    End Function


    ''' <summary>
    '''  Method to Renames the certificate.
    ''' </summary>
    ''' <param name="p_intCertificationTypeId">Certification Type Id</param>
    ''' <param name="p_strOldCertificateNumber">Old Certificate Number</param>
    ''' <param name="p_strOldCertificateExtension">Old Certificate Extension</param>
    ''' <param name="p_strNewCertificateNumber">New Certificate Number</param>
    ''' <param name="p_strNewCertificateExtension">New Certificate Extension</param>
    ''' <returns>Successfully renamed or not boolean value (True/False)</returns>
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
    ''' <para>09/25/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>   
    Public Function RenameCertificate(ByVal p_intCertificationTypeId As Integer, _
                                      ByVal p_strOldCertificateNumber As String, _
                                      ByVal p_strOldCertificateExtension As String, _
                                      ByVal p_strNewCertificateNumber As String, _
                                      ByVal p_strNewCertificateExtension As String) As Boolean
        Try
            Return Depository.Current.RenameCertificate(p_intCertificationTypeId, _
                                                    p_strOldCertificateNumber, _
                                                    p_strOldCertificateExtension, _
                                                    p_strNewCertificateNumber, _
                                                    p_strNewCertificateExtension, _
                                                    SecurityModel.GetUserName())
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method to Deletes the certificate.
    ''' </summary>
    ''' <param name="p_intCertificationTypeId">Certification Type Id</param>
    ''' <param name="p_strCertificateNumber">Certificate Number</param>
    ''' <param name="p_strCertificateExtension">Certificate Extension</param>
    ''' <returns>Successfully deleted or not boolean value (True/False).</returns>
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
    ''' <para>09/25/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>     
    Public Function DeleteCertificate(ByVal p_intCertificationTypeId As Integer, _
                                      ByVal p_strCertificateNumber As String, _
                                      ByVal p_strCertificateExtension As String) As Boolean
        Try
            Return Depository.Current.DeleteCertificate(p_intCertificationTypeId, _
                                                    p_strCertificateNumber, _
                                                    p_strCertificateExtension, _
                                                    SecurityModel.GetUserName())
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method to Gets the certificate materials.
    ''' </summary>
    ''' <param name="p_intCertificationTypeId">Certification Type Id</param>
    ''' <param name="p_strCertificateNumber">Certificate Number</param>
    ''' <param name="p_strCertificateExtension">Certificate Extension</param>
    ''' <returns>Certificate Materials as DataTable.</returns>
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
    ''' <para>09/25/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>    
    Public Function GetCertificateMaterials(ByVal p_intCertificationTypeId As Integer, _
                                            ByVal p_strCertificateNumber As String, _
                                            ByVal p_strCertificateExtension As String) As DataTable
        Try
            Return Depository.Current.GetCertificateMaterials(p_intCertificationTypeId, _
                                                          p_strCertificateNumber, _
                                                          p_strCertificateExtension)
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method to Detaches the certificate.
    ''' </summary>
    ''' <param name="p_intSkuId">Certification Id</param>
    ''' <param name="p_intCertificateId">Certificate Id</param>
    ''' <returns>Successfully detached or not boolean value (True/False).</returns>
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
    ''' <para>09/25/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>     
    Public Function DetachCertificate(ByVal p_intSkuId As Integer, _
                                      ByVal p_intCertificateId As Integer) As Boolean
        Try
            Return Depository.Current.DetachCertificate(p_intSkuId, _
                                                    p_intCertificateId)
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method to Moves the certificate.
    ''' </summary>
    ''' <param name="p_intCertificationTypeId">Certification Type Id</param>
    ''' <param name="p_strNewCertificateNumber">New Certificate Number</param>
    ''' <param name="p_strNewCertificateExtension">New Certificate Extension</param>
    ''' <returns>Successfully moved or not boolean value (True/False)</returns>
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
    ''' <para>09/25/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>  
    Public Function MoveCertificate(ByVal p_intCertificationTypeId As Integer, _
                                    ByVal p_strNewCertificateNumber As String, _
                                    ByVal p_strNewCertificateExtension As String, _
                                    ByVal p_intSkuId As Integer, _
                                    ByVal p_intCertificateId As Integer) As Boolean
        Try
            Return Depository.Current.MoveCertificate(p_intCertificationTypeId, _
                                                  p_strNewCertificateNumber, _
                                                  p_strNewCertificateExtension, _
                                                  p_intSkuId, _
                                                  p_intCertificateId, _
                                                  SecurityModel.GetUserName())
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method to Gets duplicate materials.
    ''' </summary>
    ''' <param name="p_strMaterialNumber">Material number</param>
    ''' <param name="p_strSpeedRating">Speed rating</param>
    ''' <returns>Duplicate correct materials as DataTable</returns>
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
    ''' <para>09/25/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>      
    Public Function GetDuplicateCertificates(ByVal p_strMaterialNumber As String, _
                                             ByVal p_strSpeedRating As String) As DataTable
        Try
            Return Depository.Current.GetDuplicateCertificates(p_strMaterialNumber, _
                                                           p_strSpeedRating)
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method to Delete duplicate materials.
    ''' </summary>
    ''' <param name="p_intSkuId">Sku id</param>
    ''' <returns>Indicates whether rows deleted or not as True (or) False</returns>
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
    ''' <para>09/25/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>    
    Public Function DeleteDuplicateCertificates(ByVal p_intSkuId As Integer) As Boolean
        Try
            Return Depository.Current.DeleteDuplicateCertificates(p_intSkuId)
        Catch ex As Exception
            Throw
        End Try
    End Function

#End Region

End Class
