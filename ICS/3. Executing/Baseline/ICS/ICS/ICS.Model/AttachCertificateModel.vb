Imports CooperTire.ICS.Common
Imports CooperTire.ICS.DepositoryTender

Public Class AttachCertificateModel

#Region "Methods"

    ''' <summary>
    ''' Check if certificate number exists
    ''' </summary>
    ''' <param name="p_strCertNum"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CheckCertificateNumberExists(ByVal p_strCertNum As String) As Boolean
        Return Depository.Current.CheckIfCertificateNumberExists(p_strCertNum)
    End Function

    ''' <summary>
    ''' Attach Certification.
    ''' </summary>
    ''' <param name="p_skuid"></param>
    ''' <param name="p_strCertNum"></param>
    ''' <param name="p_strExtensionEn"></param>
    ''' <param name="p_certificationtypeid"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function AttachCertification(ByVal p_skuid As Integer, _
                                        ByVal p_strCertNum As String, _
                                        ByVal p_strExtensionEn As String, _
                                        ByVal p_certificationTypeId As Integer) As String
        Return Depository.Current.AttachCertification(p_skuid, p_strCertNum, p_strExtensionEn, p_certificationTypeId)
    End Function

#End Region

End Class

