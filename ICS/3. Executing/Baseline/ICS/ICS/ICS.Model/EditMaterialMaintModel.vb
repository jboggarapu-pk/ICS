Imports CooperTire.ICS.DepositoryTender

''' <summary>
''' Edit Material Maint Model
''' </summary>
''' <remarks></remarks>
Public Class EditMaterialMaintModel

#Region "Methods"

    ''' <summary>
    ''' Get Material
    ''' </summary>
    ''' <param name="p_strMaterialNumber"></param>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    Public Function GetMaterial(ByVal p_strMaterialNumber As String) As DataTable
        Return Depository.Current.GetMaterial(p_strMaterialNumber)
    End Function

    ''' <summary>
    ''' Edit Material
    ''' </summary>
    ''' <param name="p_intSkuID"></param>
    ''' <param name="p_strSpeedrating"></param>
    ''' <returns>Boolean</returns>
    ''' <remarks></remarks>
    Public Function EditMaterial(ByVal p_intSkuID As Integer, ByVal p_strSpeedrating As String) As Boolean
        Return Depository.Current.EditMaterial(p_intSkuID, p_strSpeedrating)
    End Function

#End Region

End Class
