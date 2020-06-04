Imports CooperTire.ICS.DepositoryTender

Public Class RefreshProductModel
#Region "Methods"

    ''' <summary>
    ''' Refresh Product Data.
    ''' </summary>
    ''' <param name="p_strMaterialNumber">Material Number</param>
    ''' <param name="p_strErrorDesc">Error Description if failed</param>
    ''' <returns>Returns 1 if success or 0 if failed.</returns>
    ''' <remarks></remarks>
    Public Function RefreshProduct(ByVal p_strMaterialNumber As String, ByRef p_strErrorDesc As String) As Integer
        Return Depository.Current.RefreshProduct(p_strMaterialNumber, p_strErrorDesc)
    End Function
#End Region

End Class
