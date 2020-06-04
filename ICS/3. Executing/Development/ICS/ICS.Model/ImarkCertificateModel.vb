Imports CooperTire.ICS.DomainEntities
Imports CooperTire.ICS.Common
Imports CooperTire.ICS.DepositoryTender

''' <summary>
'''  Class containing Imark certificate processing model.
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
Public Class ImarkCertificateModel

#Region "Methods"

    ''' <summary>
    '''  Method to renew Certificate
    ''' </summary>
    ''' <returns>Boolean</returns> 
    ''' <param name="p_intCertificateNumberId">Certificate Number Id</param>
    ''' <param name="p_intNewCertificateId">New Certificate Id</param>
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
    Public Function RenewCertificate(ByVal p_intCertificateNumberId As Integer, ByRef p_intNewCertificateId As Integer) As Boolean
        Try
            Dim enumSaveResult As NameAid.SaveResult = NameAid.SaveResult.SaveError
            Dim strUsername As String = SecurityModel.GetUserName

            'Call stored procedure to update date submitted with current date for Material numbers that have already been certified (DateApproved)
            enumSaveResult = Depository.Current.RenewCertificate(p_intCertificateNumberId, p_intNewCertificateId, strUsername)

            Return CBool(enumSaveResult)
        Catch ex As Exception
            Throw
        End Try
    End Function
#End Region

End Class
