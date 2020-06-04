Imports CooperTire.ICS.DepositoryTender

''' <summary>
''' Family maintenance processing model
''' </summary>
''' <remarks></remarks>
Public Class FamilyMaintenanceModel

#Region "Methods"

    ''' <summary>
    ''' Retrieves the families
    ''' </summary>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    ''' 
    Public Function GetFamilies(ByVal pn_certificateid As Integer) As DataTable
        Dim dtable As DataTable
        dtable = Depository.Current.GetFamilies(pn_certificateid)
        Return dtable
    End Function

    ''' <summary>
    ''' Inserts/updates the record in Imark family table
    ''' </summary>
    ''' <param name="p_intFamilyID">FamilyID </param>
    ''' <param name="p_strFamilyCode">FamilyCode</param>
    ''' <param name="p_strFamilyDesc">FamilyDesc</param>
    ''' <param name="p_strApplicationCat">ApplicationCat</param>
    ''' <param name="p_strConstructionType">ConstructionType</param>
    ''' <param name="p_strStructureType">StructureType</param>
    ''' <param name="p_strMountingType">MountingType</param>
    ''' <param name="p_strAspectRatioCat">AspectRatioCat</param>
    ''' <param name="p_strSpeedRatingCat">SpeedRatingCat</param>
    ''' <param name="p_strLoadIndexCat">LoadIndexCat</param>    
    ''' <returns>boolean value</returns>
    ''' <remarks></remarks>
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
    End Function

    ''' <summary>
    ''' Deletes family record
    ''' </summary>
    ''' <param name="p_intFamilyId"></param>
    ''' <returns>Boolean value</returns>
    ''' <remarks></remarks>
    Public Function Deletefamily(ByVal p_intCertificateid As Integer, ByVal p_intFamilyId As Integer) As Boolean
        Return Depository.Current.Deletefamily(p_intCertificateid, p_intFamilyId)
    End Function


    Public Function GetImarkCertificates() As DataTable
        Return Depository.Current.GetCertificatesByType(4, "Y")
    End Function
#End Region

End Class