Imports CooperTire.ICS.Model
Imports CooperTire.ICS.Common

''' <summary>
''' Family maintenance interface to the Family Maintenance User control view
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
''' <term>Jhansi</term>
''' <description>
''' <para>10/17/2019</para>
''' <para>Implemented Coding Standards.</para>
''' </description>
''' </item>
''' </list>
''' </remarks>
Public Class FamilyMaintenancePresenter

#Region "Members"
    ''' <summary>
    ''' Interface to the Family Maintenance User control view
    ''' </summary>
    Private m_view As IFamilyMaintenanceView = Nothing
#End Region

#Region "Constructors"
    ''' <summary>
    ''' Custom Constructor to initialize class members.
    ''' </summary>
    ''' <param name="p_view">View</param>
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
    ''' <term>Jhansi</term>
    ''' <description>
    ''' <para>10/17/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Sub New(ByVal p_view As IFamilyMaintenanceView)
        m_view = p_view
        SubscribeToEvents()
    End Sub

#End Region


#Region "Methods"

    ''' <summary>
    ''' Family maintenance presenter to view�s events.
    ''' </summary>
    ''' <exception cref="Exception">
    ''' Logs to windows event and throws the exception if any error occurs.
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
    ''' <term>Jhansi</term>
    ''' <description>
    ''' <para>10/17/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub SubscribeToEvents()
        Try
            AddHandler m_view.LoadView, AddressOf OnLoadView
            AddHandler m_view.ShowFamiles, AddressOf ShowFamiles 'jeseitz fix
            AddHandler m_view.DeleteFamily, AddressOf OnDelete
            AddHandler m_view.SaveFamily, AddressOf OnSaveFamily
            AddHandler m_view.AddFamily, AddressOf OnAdd
            AddHandler m_view.EditFamily, AddressOf OnEdit
            AddHandler m_view.ChangeCertificate, AddressOf OnChange
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Load data for the view.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <exception cref="Exception">
    ''' Logs to windows event and throws the exception if any error occurs.
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
    ''' <term>Jhansi</term>
    ''' <description>
    ''' <para>10/17/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub OnLoadView(ByVal sender As Object, ByVal e As EventArgs)
        Const ErrorLoadFormData As String = "Error loading form data."
        Try
            If (Not m_view.IsPostBackView) Then 'jeseitz 4/12/16
                LoadViewData()
            End If
        Catch exp As Exception
            EventLogger.Enter(exp)
            m_view.ErrorText = ErrorLoadFormData
        End Try
    End Sub

    ''' <summary>
    ''' Load data from business process.
    ''' </summary>
    ''' <exception cref="Exception">
    ''' Logs to windows event and throws the exception if any error occurs.
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
    ''' <term>Jhansi</term>
    ''' <description>
    ''' <para>10/17/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub LoadViewData()
        Const FamilyMaintText As String = "Family Maintenance"
        Try
            m_view.AddTitle = FamilyMaintText
            LoadImarkDD()
            ShowFamiles() 'jeseitz fix
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Display all the records from Imark_Family.
    ''' </summary>
    ''' <exception cref="Exception">
    ''' Logs to windows event and throws the exception if any error occurs.
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
    ''' <term>Jhansi</term>
    ''' <description>
    ''' <para>10/17/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub ShowFamiles()
        Const NoRecordsText As String = "No records(Families) exists."
        Const ErrorFetchFamily As String = "Error fetching families."
        Dim dtResults As DataTable = Nothing
        Dim objFamilyMaintenanceModel As New FamilyMaintenanceModel

        m_view.SuccessText = String.Empty
        m_view.ErrorText = String.Empty

        Dim p_CertificateID As Long = m_view.ImarkCertificateSelected
        Try
            dtResults = objFamilyMaintenanceModel.GetFamilies(CInt(p_CertificateID))

            If dtResults IsNot Nothing AndAlso dtResults.Rows.Count > 0 Then
                m_view.Families = dtResults
                m_view.DataBindView()

                ' Checking whether the logged in user is in APP_ICS_QS_EMP Active Directory group 
                m_view.QualityUser = SecurityModel.IsUserAuthorized(SecurityModel.GetUserName())
            Else
                m_view.ErrorText = NoRecordsText
            End If
        Catch exc As Exception
            EventLogger.Enter(exc)
            m_view.ErrorText = ErrorFetchFamily
        End Try
    End Sub

    ''' <summary>
    ''' Load IMark DD.
    ''' </summary>
    ''' <exception cref="Exception">
    ''' Logs to windows event and throws the exception if any error occurs.
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
    ''' <term>Jhansi</term>
    ''' <description>
    ''' <para>10/17/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub LoadImarkDD()
        Const NoImarkText As String = "No Imark Certificates exist."
        Const ErrorFetchCertText As String = "Error fetching certificates."

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
                m_view.ErrorText = NoImarkText
            End If
        Catch exc As Exception
            EventLogger.Enter(exc)
            m_view.ErrorText = ErrorFetchCertText
        End Try
    End Sub

    ''' <summary>
    ''' Clean up view to add new record.
    ''' </summary>
    ''' <exception cref="Exception">
    ''' Logs to windows event and throws the exception if any error occurs.
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
    ''' <term>Jhansi</term>
    ''' <description>
    ''' <para>10/17/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>    
    Private Sub OnAdd()
        Const ErrorAddFamily As String = "Error adding family."
        Try
            CleanView()
        Catch exc As Exception
            EventLogger.Enter(exc)
            m_view.ErrorText = ErrorAddFamily
        End Try
    End Sub

    ''' <summary>
    ''' Assigning the values to Textboxes in edit mode.
    ''' </summary>
    ''' <param name="e"></param>
    ''' <exception cref="Exception">
    ''' Logs to windows event and throws the exception if any error occurs.
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
    ''' <term>Jhansi</term>
    ''' <description>
    ''' <para>10/17/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>    
    Private Sub OnEdit(ByVal e As Object)
        Dim objFamilyMaintenanceModel As New FamilyMaintenanceModel
        Dim familyId As Integer = CInt(e)
        Dim dtResults As DataTable = Nothing
        Const ErrorEditFamily As String = "Error editing family."
        Const FamilyIdText As String = "FAMILY_ID"
        Const FamilyCode As String = "FAMILY_CODE"
        Const FamilyDesc As String = "FAMILY_DESC"
        Const ApplicationCat As String = "APPLICATION_CAT"
        Const ConstructionType As String = "CONSTRUCTION_TYPE"
        Const StructureType As String = "STRUCTURE_TYPE"
        Const MountingType As String = "MOUNTING_TYPE"
        Const AspectRatioCat As String = "ASPECT_RATIO_CAT"
        Const SpeedRatingCat As String = "SPEED_RATING_CAT"
        Const LoadIndexCat As String = "LOAD_INDEX_CAT"

        Try
            CleanView()
            dtResults = objFamilyMaintenanceModel.GetFamilies(CInt(m_view.ImarkCertificateSelected))
            Dim rows As DataRow() = dtResults.Select(String.Format("FAMILY_ID = {0} ", familyId))

            If rows.Length > 0 Then
                Dim row As DataRow = rows(0)

                m_view.FamilyId = CInt(row.Item(FamilyIdText))
                m_view.FamilyCode = ConvertNullToString(row.Item(FamilyCode))
                m_view.FamilyDesc = ConvertNullToString(row.Item(FamilyDesc))
                m_view.ApplicationCat = ConvertNullToString(row.Item(ApplicationCat))
                m_view.ConstructionType = ConvertNullToString(row.Item(ConstructionType))
                m_view.StructureType = ConvertNullToString(row.Item(StructureType))
                m_view.MountingType = ConvertNullToString(row.Item(MountingType))
                m_view.AspectRatioCat = ConvertNullToString(row.Item(AspectRatioCat))
                m_view.SpeedRatingCat = ConvertNullToString(row.Item(SpeedRatingCat))
                m_view.LoadIndexCat = ConvertNullToString(row.Item(LoadIndexCat))
            End If
        Catch exc As Exception
            EventLogger.Enter(exc)
            m_view.ErrorText = ErrorEditFamily
        End Try
    End Sub

    ''' <summary>
    ''' Clean up view to add new record.
    ''' </summary>
    ''' <param name="e"></param>
    ''' <exception cref="Exception">
    ''' Logs to windows event and throws the exception if any error occurs.
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
    ''' <term>Jhansi</term>
    ''' <description>
    ''' <para>10/17/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>    
    Private Sub OnChange(ByVal e As Object)
        Dim p_certificateid As Long
        p_certificateid = CInt(e)
        Const ErrorChooseCertificateText As String = "Error choosing Certificate."
        Try
            CleanView()
            ShowFamiles()
        Catch exc As Exception
            EventLogger.Enter(exc)
            m_view.ErrorText = ErrorChooseCertificateText
        End Try
    End Sub

    ''' <summary>
    ''' Convert null to string.
    ''' </summary>
    ''' <param name="obj">Object</param>
    ''' <exception cref="Exception">
    ''' Logs to windows event and throws the exception if any error occurs.
    ''' </exception>
    ''' <returns>String</returns>
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
    ''' <term>Jhansi</term>
    ''' <description>
    ''' <para>10/17/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Function ConvertNullToString(ByVal obj As Object) As String
        Try
            If IsDBNull(obj) Then
                Return String.Empty
            Else
                Return CStr(obj)
            End If
        Catch
            Throw
        End Try
    End Function

    ''' <summary>
    ''' Deletes the record from families.
    ''' </summary>
    ''' <param name="e"></param>
    ''' <exception cref="Exception">
    ''' Logs to windows event and throws the exception if any error occurs.
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
    ''' <term>Jhansi</term>
    ''' <description>
    ''' <para>10/17/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>    
    Private Sub OnDelete(ByVal e As Object)
        Dim blnDeleted As Boolean = False
        Dim objFamilyMaintenanceModel As New FamilyMaintenanceModel
        Const SuccessDeleteFamilyText As String = "Successfully deleted family."
        Const FailDeleteFamilyText As String = "Failed to delete family."
        Const ErrorDeleteFamilyText As String = "Error deleting family."
        Try
            blnDeleted = objFamilyMaintenanceModel.Deletefamily(CInt(m_view.ImarkCertificateSelected), CInt(e))
            ShowFamiles()

            If blnDeleted Then
                m_view.SuccessText = SuccessDeleteFamilyText
                m_view.ErrorText = String.Empty
            Else
                m_view.SuccessText = String.Empty
                m_view.ErrorText = FailDeleteFamilyText
            End If
        Catch exc As Exception
            EventLogger.Enter(exc)
            m_view.ErrorText = ErrorDeleteFamilyText
        End Try
    End Sub

    ''' <summary>
    ''' Clear all the values in addmode.
    ''' </summary>
    ''' <exception cref="Exception">
    ''' Logs to windows event and throws the exception if any error occurs.
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
    ''' <term>Jhansi</term>
    ''' <description>
    ''' <para>10/17/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub CleanView()
        Try
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
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Updates the family records based on the user inputs.
    ''' </summary>
    ''' <exception cref="Exception">
    ''' Logs to windows event and throws the exception if any error occurs.
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
    ''' <term>Jhansi</term>
    ''' <description>
    ''' <para>10/17/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub OnSaveFamily()
        m_view.SuccessText = String.Empty
        m_view.ErrorText = String.Empty
        Const SuccessSaveFamilyText As String = "Successfully saved family."
        Const FailSaveFamilyText As String = "Failed to save family."
        Const ErrorSaveFamilyText As String = "Error saving family."

        Dim objFamilyMaintenanceModel As New FamilyMaintenanceModel
        Try
            Dim Result As Boolean = objFamilyMaintenanceModel.SaveFamily(CInt(m_view.ImarkCertificateSelected),
                                                                         m_view.FamilyId,
                                                                         m_view.FamilyCode,
                                                                         m_view.FamilyDesc,
                                                                         m_view.ApplicationCat,
                                                                         m_view.ConstructionType,
                                                                         m_view.StructureType,
                                                                         m_view.MountingType,
                                                                         m_view.AspectRatioCat,
                                                                         m_view.SpeedRatingCat,
                                                                         m_view.LoadIndexCat)

            If Result Then
                ShowFamiles()
                m_view.SuccessText = SuccessSaveFamilyText
            Else
                m_view.ErrorText = FailSaveFamilyText
            End If

        Catch exc As Exception
            EventLogger.Enter(exc)
            m_view.ErrorText = ErrorSaveFamilyText
        End Try
    End Sub

#End Region
End Class