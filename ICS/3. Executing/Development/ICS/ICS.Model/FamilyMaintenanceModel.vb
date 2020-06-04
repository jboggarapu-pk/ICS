Imports CooperTire.ICS.DepositoryTender

''' <summary>
'''  Class containing Family maintenance processing model
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
Public Class FamilyMaintenanceModel

#Region "Methods"

    ''' <summary>
    '''  Method to Retrieve families.
    ''' </summary>
    ''' <returns>Datatable</returns> 
    ''' <param name="pn_certificateid">Certificate Id</param>
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
    Public Function GetFamilies(ByVal pn_certificateid As Integer) As DataTable
        Try
            Dim dtable As DataTable
            dtable = Depository.Current.GetFamilies(pn_certificateid)
            Return dtable
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method to Inserts/updates the record in Imark family table
    ''' </summary>
    ''' <returns>Boolean</returns> 
    ''' <param name="p_intFamilyID">FamilyID</param>
    ''' <param name="p_strFamilyCode">FamilyCode</param>
    ''' <param name="p_strFamilyDesc">FamilyDesc</param>
    ''' <param name="p_strApplicationCat">ApplicationCat</param>
    ''' <param name="p_strConstructionType">ConstructionType</param>
    ''' <param name="p_strStructureType">StructureType</param>
    ''' <param name="p_strMountingType">MountingType</param>
    ''' <param name="p_strAspectRatioCat">AspectRatioCat</param>
    ''' <param name="p_strSpeedRatingCat">SpeedRatingCat</param>
    ''' <param name="p_strLoadIndexCat">LoadIndexCat</param>    
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
    Public Function SaveFamily(ByVal p_intCertificateid As Integer, ByVal p_intFamilyID As Integer, _
                                    ByVal p_strFamilyCode As String, _
                                    ByVal p_strFamilyDesc As String, _
                                    ByVal p_strApplicationCat As String, _
                                    ByVal p_strConstructionType As String, _
                                    ByVal p_strStructureType As String, _
                                    ByVal p_strMountingType As String, _
                                    ByVal p_strAspectRatioCat As String, _
                                    ByVal p_strSpeedRatingCat As String, _
                                    ByVal p_strLoadIndexCat As String) As System.Boolean
        Try
            Return Depository.Current.SaveFamily(p_intCertificateid, p_intFamilyID, _
                                                 p_strFamilyCode, _
                                                 p_strFamilyDesc, _
                                                 p_strApplicationCat, _
                                                 p_strConstructionType, _
                                                 p_strStructureType, _
                                                 p_strMountingType, _
                                                 p_strAspectRatioCat, _
                                                 p_strSpeedRatingCat, _
                                                 p_strLoadIndexCat, _
                                                 SecurityModel.GetUserName)
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method to Deletes family record
    ''' </summary>
    ''' <returns>Boolean</returns> 
    ''' <param name="p_intCertificateid">Certificate Id</param>
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
    Public Function Deletefamily(ByVal p_intCertificateid As Integer, ByVal p_intFamilyId As Integer) As Boolean
        Try
            Return Depository.Current.Deletefamily(p_intCertificateid, p_intFamilyId)
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    '''  Method to get Imack Certificates
    ''' </summary>
    ''' <returns>Datatable</returns> 
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
    Public Function GetImarkCertificates() As DataTable
        Try
            Return Depository.Current.GetCertificatesByType(4, "Y")
        Catch ex As Exception
            Throw
        End Try
    End Function
#End Region

End Class