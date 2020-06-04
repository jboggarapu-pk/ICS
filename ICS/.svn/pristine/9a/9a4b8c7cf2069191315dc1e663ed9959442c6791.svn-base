Imports CooperTire.ICS.Common
Imports CooperTire.ICS.DepositoryTender

Public Class CopyCertificationModel

#Region "Methods"

    ''' <summary>
    ''' Check if certificate number exists
    ''' </summary>
    ''' <param name="p_strMateNum"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CheckMaterialNumberExists(ByVal p_strMateNum As String) As Boolean
        Return Depository.Current.CheckMatlNumExists(p_strMateNum)
    End Function

    ''' <summary>
    ''' Copy Certification
    ''' </summary>
    ''' <param name="p_strMateNum"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CopyCertification(ByVal p_strMateNum As String) As Boolean
        Return Depository.Current.CopyCertification(p_strMateNum)
    End Function
#End Region

End Class
