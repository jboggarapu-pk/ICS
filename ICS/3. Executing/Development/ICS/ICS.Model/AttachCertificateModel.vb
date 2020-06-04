Imports CooperTire.ICS.Common
Imports CooperTire.ICS.DepositoryTender

''' <summary>
''' Contains data access methods related to Attach Certification Model.
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
''' <para>09/25/2019</para>
''' <para>Implemented Coding Standards</para>
''' </description>
''' </item> 
''' </list>
''' </remarks> 

Public Class AttachCertificateModel

#Region "Methods"

    ''' <summary>
    '''  Method to check Certification number exists or not .
    ''' </summary>
    ''' <param name="p_strCertNum">Certificate Number</param>
    ''' <returns>Boolean</returns> 
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
    Public Function CheckCertificateNumberExists(ByVal p_strCertNum As String) As Boolean
        Try
            Return Depository.Current.CheckIfCertificateNumberExists(p_strCertNum)
        Catch
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method to Attach Certification.
    ''' </summary>
    ''' <param name="p_skuid">Sku Id</param>
    ''' <param name="p_strCertNum">Certificate Number</param>
    ''' <param name="p_strExtensionEn">Extension Number</param>
    ''' <param name="p_certificationTypeId">Certificate type id</param>
    ''' <returns>Indicates whether material copied or not as True (or) False</returns> 
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
    Public Function AttachCertification(ByVal p_skuid As Integer, _
                                        ByVal p_strCertNum As String, _
                                        ByVal p_strExtensionEn As String, _
                                        ByVal p_certificationTypeId As Integer) As String
        Try
            Return Depository.Current.AttachCertification(p_skuid, p_strCertNum, p_strExtensionEn, p_certificationTypeId)
        Catch ex As Exception
            Throw
        End Try

    End Function

#End Region

End Class

