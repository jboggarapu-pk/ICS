Imports CooperTire.ICS.Common
Imports CooperTire.ICS.DepositoryTender

''' <summary>
''' Contains data access methods related to Certification Search - business process model.
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
Public Class CopyCertificationModel

#Region "Methods"

    ''' <summary>
    ''' Method to Check if certificate number exists
    ''' </summary>
    ''' <param name="p_strMateNum">MateNum</param>
    ''' <returns>Boolean Value</returns> 
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
    Public Function CheckMaterialNumberExists(ByVal p_strMateNum As String) As Boolean
        Try
            Return Depository.Current.CheckMatlNumExists(p_strMateNum)
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    ''' Method to Copy Certification
    ''' </summary>
    ''' <param name="p_strMateNum">MateNum</param>
    ''' <returns>Boolean Value</returns> 
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
    Public Function CopyCertification(ByVal p_strMateNum As String) As Boolean
        Try
            Return Depository.Current.CopyCertification(p_strMateNum)
        Catch ex As Exception
            Throw
        End Try
    End Function
#End Region

End Class
