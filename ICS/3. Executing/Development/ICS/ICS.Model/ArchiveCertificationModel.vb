Imports CooperTire.ICS.Common
Imports CooperTire.ICS.DepositoryTender
 

''' <summary>
''' Contains data access methods related to Archive certification .
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

Public Class ArchiveCertificationModel

#Region "Methods"

    ''' <summary>
    '''  Method to Check if certificate number exists.
    ''' </summary>
    ''' <param name="p_strCertNum">Certificate Number</param>
    ''' <returns>Boolean for Material number existence.</returns>
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
    '''  Method to Archive certification
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
    Public Function ArchiveCertification(ByVal p_strCertNum As String) As Boolean
        Try
            Dim blnSaved As Boolean = False
            blnSaved = Depository.Current.ArchiveCertification(p_strCertNum, SecurityModel.GetUserName)
            Return blnSaved
        Catch
            Throw
        End Try

    End Function

#End Region

End Class
