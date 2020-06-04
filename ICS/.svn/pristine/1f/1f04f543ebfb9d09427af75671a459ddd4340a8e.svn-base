Imports CooperTire.ICS.DomainEntities
Imports CooperTire.ICS.Common
Imports CooperTire.ICS.DepositoryTender

''' <summary>
''' Imark certificate processing model
''' </summary>
''' <remarks></remarks>
Public Class ImarkCertificateModel

#Region "Members"

    '  Public Const ImarkCertNumber As String = "I033" 'jeseitz 4/14/2016 i033 no longer default

#End Region

#Region "Methods"

    Public Function RenewCertificate(ByVal p_intCertificateNumberId As Integer, ByRef p_intNewCertificateId As Integer) As Boolean

        Dim enumSaveResult As NameAid.SaveResult = NameAid.SaveResult.SaveError
        Dim strUsername As String = SecurityModel.GetUserName

        'Call stored procedure to update date submitted with current date for Material numbers that have already been certified (DateApproved)
        enumSaveResult = Depository.Current.RenewCertificate(p_intCertificateNumberId, p_intNewCertificateId, strUsername)

        Return enumSaveResult
    End Function
#End Region

End Class
