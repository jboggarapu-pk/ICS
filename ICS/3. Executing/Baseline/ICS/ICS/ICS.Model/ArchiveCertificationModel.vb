Imports CooperTire.ICS.Common
Imports CooperTire.ICS.DepositoryTender

''' <summary>
''' Archive certification model
''' </summary>
''' <remarks></remarks>
Public Class ArchiveCertificationModel

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
    ''' Archive certification
    ''' </summary>
    ''' <param name="p_strCertNum"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ArchiveCertification(ByVal p_strCertNum As String) As Boolean

        Dim blnSaved As Boolean = False
        blnSaved = Depository.Current.ArchiveCertification(p_strCertNum, SecurityModel.GetUserName)
        Return blnSaved

    End Function

#End Region

End Class
