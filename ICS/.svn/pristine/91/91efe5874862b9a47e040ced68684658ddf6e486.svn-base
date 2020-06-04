Imports CooperTire.ICS.Model
Imports CooperTire.ICS.Common

''' <summary>
''' Family maintenance interface to the Family Maintenance User control view
''' </summary>
''' 
Public Class FamilyMaintenancePresenter

#Region "Members"
    Private m_view As IFamilyMaintenanceView = Nothing
#End Region

#Region "Constructors"

    Public Sub New(ByVal p_view As IFamilyMaintenanceView)
        m_view = p_view
        SubscribeToEvents()
    End Sub

#End Region


#Region "Methods"

    ''' <summary>
    ''' Family maintenance presenter to view’s events.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SubscribeToEvents()
        AddHandler m_view.LoadView, AddressOf OnLoadView
        AddHandler m_view.ShowFamiles, AddressOf ShowFamiles 'jeseitz fix
        AddHandler m_view.DeleteFamily, AddressOf OnDelete
        AddHandler m_view.SaveFamily, AddressOf OnSaveFamily
        AddHandler m_view.AddFamily, AddressOf OnAdd
        AddHandler m_view.EditFamily, AddressOf OnEdit
        AddHandler m_view.ChangeCertificate, AddressOf OnChange
    End Sub

    ''' <summary>
    ''' Load data for the view
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub OnLoadView(ByVal sender As Object, ByVal e As EventArgs)
        Try
            If (Not m_view.IsPostBackView) Then 'jeseitz 4/12/16
                LoadViewData()
            End If
        Catch exp As Exception
            EventLogger.Enter(exp)
            m_view.ErrorText = "Error loading form data."
        End Try
    End Sub

    ''' <summary>
    ''' Load data from business process
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LoadViewData()
        m_view.AddTitle = "Family Maintenance"
        LoadImarkDD()
        'Dim i As Long = m_view.ImarkCertificateSelected
        ShowFamiles() 'jeseitz fix
    End Sub

    ''' <summary>
    ''' Display all the records from Imark_Family
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ShowFamiles()
        Dim dtResults As DataTable = Nothing
        Dim objFamilyMaintenanceModel As New FamilyMaintenanceModel

        m_view.SuccessText = String.Empty
        m_view.ErrorText = String.Empty

        Dim p_CertificateID As Long = m_view.ImarkCertificateSelected
        Try
            dtResults = objFamilyMaintenanceModel.GetFamilies(p_CertificateID)

            If dtResults IsNot Nothing AndAlso dtResults.Rows.Count > 0 Then
                m_view.Families = dtResults
                m_view.DataBindView()

                ' Checking whether the logged in user is in APP_ICS_QS_EMP Active Directory group 
                m_view.QualityUser = SecurityModel.IsUserAuthorized(SecurityModel.GetUserName())
            Else
                m_view.ErrorText = "No records(Families) exists."
            End If
        Catch exc As Exception
            EventLogger.Enter(exc)
            m_view.ErrorText = "Error fetching families."
        End Try
    End Sub

    Private Sub LoadImarkDD()

        Dim dtResults As DataTable = Nothing
        Dim objFamilyMaintenanceModel As New FamilyMaintenanceModel

        m_view.SuccessText = String.Empty
        m_view.ErrorText = String.Empty

        Try
            dtResults = objFamilyMaintenanceModel.GetImarkCertificates()

            If dtResults IsNot Nothing AndAlso dtResults.Rows.Count > 0 Then
                m_view.ImarkCertificates = dtResults
                m_view.DataBindView()

            Else
                m_view.ErrorText = "No Imark Certificates exist."
            End If
        Catch exc As Exception
            EventLogger.Enter(exc)
            m_view.ErrorText = "Error fetching certificates."
        End Try


    End Sub

    ''' <summary>
    ''' Clean up view to add new record
    ''' </summary>    
    Private Sub OnAdd()
        Try
            CleanView()
        Catch exc As Exception
            EventLogger.Enter(exc)
            m_view.ErrorText = "Error adding family."
        End Try
    End Sub


    ''' <summary>
    ''' Assigning the values to Textboxes in eidt mode
    ''' </summary>    
    Private Sub OnEdit(ByVal e As Object)
        Dim objFamilyMaintenanceModel As New FamilyMaintenanceModel
        Dim familyId As Integer = CInt(e)
        Dim dtResults As DataTable = Nothing

        Try
            CleanView()
            dtResults = objFamilyMaintenanceModel.GetFamilies(m_view.ImarkCertificateSelected)
            Dim rows As DataRow() = dtResults.Select(String.Format("FAMILY_ID = {0} ", familyId))

            If rows.Length > 0 Then
                Dim row As DataRow = rows(0)

                m_view.FamilyId = row.Item("FAMILY_ID")
                m_view.FamilyCode = ConvertNullToString(row.Item("FAMILY_CODE"))
                m_view.FamilyDesc = ConvertNullToString(row.Item("FAMILY_DESC"))
                m_view.ApplicationCat = ConvertNullToString(row.Item("APPLICATION_CAT"))
                m_view.ConstructionType = ConvertNullToString(row.Item("CONSTRUCTION_TYPE"))
                m_view.StructureType = ConvertNullToString(row.Item("STRUCTURE_TYPE"))
                m_view.MountingType = ConvertNullToString(row.Item("MOUNTING_TYPE"))
                m_view.AspectRatioCat = ConvertNullToString(row.Item("ASPECT_RATIO_CAT"))
                m_view.SpeedRatingCat = ConvertNullToString(row.Item("SPEED_RATING_CAT"))
                m_view.LoadIndexCat = ConvertNullToString(row.Item("LOAD_INDEX_CAT"))
            End If
        Catch exc As Exception
            EventLogger.Enter(exc)
            m_view.ErrorText = "Error editing family."
        End Try
    End Sub
    ''' <summary>
    ''' Clean up view to add new record
    ''' </summary>    
    Private Sub OnChange(ByVal e As Object)

        Dim p_certificateid As Long
        p_certificateid = CInt(e)

        Try
            CleanView()
            ShowFamiles()
        Catch exc As Exception
            EventLogger.Enter(exc)
            m_view.ErrorText = "Error choosing Certificate."
        End Try
    End Sub


    ''' <summary>
    ''' Convert null to string
    ''' </summary>
    ''' <param name="obj"></param>
    ''' <returns>String</returns>
    ''' <remarks></remarks>
    Private Function ConvertNullToString(ByVal obj As Object) As String
        If IsDBNull(obj) Then
            Return String.Empty
        Else
            Return CStr(obj)
        End If
    End Function

    ''' <summary>
    ''' Deletes the record from families
    ''' </summary>    
    Private Sub OnDelete(ByVal e As Object)
        Dim blnDeleted As Boolean = False
        Dim objFamilyMaintenanceModel As New FamilyMaintenanceModel

        Try
            blnDeleted = objFamilyMaintenanceModel.Deletefamily(m_view.ImarkCertificateSelected, CInt(e))
            ShowFamiles()

            If blnDeleted Then
                m_view.SuccessText = "Successfully deleted family."
                m_view.ErrorText = String.Empty
            Else
                m_view.SuccessText = String.Empty
                m_view.ErrorText = "Failed to delete family."
            End If
        Catch exc As Exception
            EventLogger.Enter(exc)
            m_view.ErrorText = "Error deleting family."
        End Try
    End Sub

    ''' <summary>
    ''' Clear all the values in addmode
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CleanView()
        m_view.FamilyId = 0
        m_view.FamilyCode = String.Empty
        m_view.FamilyDesc = String.Empty
        m_view.ApplicationCat = String.Empty
        m_view.ConstructionType = String.Empty
        m_view.StructureType = String.Empty
        m_view.MountingType = String.Empty
        m_view.AspectRatioCat = String.Empty
        m_view.SpeedRatingCat = String.Empty
        m_view.LoadIndexCat = String.Empty
        m_view.ErrorText = String.Empty
        m_view.SuccessText = String.Empty
    End Sub
    ''' <summary>
    ''' Updates the family records based on the user inputs
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub OnSaveFamily()
        m_view.SuccessText = String.Empty
        m_view.ErrorText = String.Empty

        Dim objFamilyMaintenanceModel As New FamilyMaintenanceModel
        Try
            Dim Result As Boolean = objFamilyMaintenanceModel.SaveFamily(m_view.ImarkCertificateSelected, m_view.FamilyId, m_view.FamilyCode, m_view.FamilyDesc, m_view.ApplicationCat, m_view.ConstructionType, m_view.StructureType, m_view.MountingType, m_view.AspectRatioCat, m_view.SpeedRatingCat, m_view.LoadIndexCat)

            If Result Then
                ShowFamiles()
                m_view.SuccessText = "Successfully saved family."
            Else
                m_view.ErrorText = "Failed to save family."
            End If

        Catch exc As Exception
            EventLogger.Enter(exc)
            m_view.ErrorText = "Error saving family."
        End Try
    End Sub



#End Region
End Class