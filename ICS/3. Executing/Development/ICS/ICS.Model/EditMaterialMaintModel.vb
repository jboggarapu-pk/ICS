Imports CooperTire.ICS.DepositoryTender

''' <summary>
'''  Class containing Edit Material Maint Model
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
Public Class EditMaterialMaintModel

#Region "Methods"

    ''' <summary>
    '''  Method to get Material.
    ''' </summary>
    ''' <returns>Datatable</returns> 
    ''' <param name="p_strMaterialNumber">Material Number</param>
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
    Public Function GetMaterial(ByVal p_strMaterialNumber As String) As DataTable
        Try
            Return Depository.Current.GetMaterial(p_strMaterialNumber)
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method to Edit Material
    ''' </summary>
    ''' <returns>Boolean</returns> 
    ''' <param name="p_intSkuID">Sku Id</param>
    ''' <param name="p_strSpeedrating">Speed Rating</param>
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
    Public Function EditMaterial(ByVal p_intSkuID As Integer, ByVal p_strSpeedrating As String) As Boolean
        Try
            Return Depository.Current.EditMaterial(p_intSkuID, p_strSpeedrating)
        Catch ex As Exception
            Throw
        End Try
    End Function

#End Region

End Class
