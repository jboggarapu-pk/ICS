Imports CooperTire.ICS.DepositoryTender

''' <summary>
'''  Class contains methods and properties related to Refresh Product Model.
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
''' <para>10/04/2019</para>
''' <para>Implemented Coding Standards</para>
''' </description>
''' </item> 
''' </list>
''' </remarks>
Public Class RefreshProductModel
#Region "Methods"

    ''' <summary>
    '''  Method to Gets refresh product.
    ''' </summary>
    ''' <returns>Integer</returns> 
    ''' <param name="p_strMaterialNumber">Material Number</param>
    ''' <param name="p_strErrorDesc">Error Description if failed</param>
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
    ''' <para>10/04/2019</para>
    ''' <para>Implemented Coding Standards</para>
    ''' </description>
    ''' </item> 
    ''' </list>
    ''' </remarks>
    Public Function RefreshProduct(ByVal p_strMaterialNumber As String, ByRef p_strErrorDesc As String) As Integer
        Try
            Return Depository.Current.RefreshProduct(p_strMaterialNumber, p_strErrorDesc)
        Catch ex As Exception
            Throw
        End Try
    End Function
#End Region

End Class
