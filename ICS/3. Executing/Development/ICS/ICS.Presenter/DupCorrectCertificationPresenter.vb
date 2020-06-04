Imports CooperTire.ICS.Model
Imports CooperTire.ICS.Common
Imports CooperTire.ICS.DomainEntities

''' <summary>
''' Duplicate correct certification presenter
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
''' <para>10/16/2019</para>
''' <para>Implemented Coding Standards.</para>
''' </description>
''' </item>
''' </list>
''' </remarks>
Public Class DupCorrectCertificationPresenter

#Region "Members and Constants"
    ''' <summary>
    ''' Interface to the delete duplicate certificates user control view.
    ''' </summary>
    Private m_view As IDupCorrectCertificationView
    ''' <summary>
    '''  Constant to hold ORA text
    ''' </summary>
    Private Const ORAText As String = "ORA-20001"
    ''' <summary>
    '''  Constant to hold ORA text
    ''' </summary>
    Private Const ErrorFetchingText As String = "Error fetching materials."


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
    ''' <para>10/16/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Public Sub New(ByVal p_view As IDupCorrectCertificationView)
        Const ErrorCreatingText As String = "Error creating "
        Try
            m_view = p_view
            SubscribeToEvents()
        Catch exp As Exception
            EventLogger.Enter(exp)
            Throw New Exception(ErrorCreatingText + Me.ToString())
        End Try
    End Sub
#End Region

#Region "Methods"

    ''' <summary>
    ''' Duplicate correct certification presenter to view’s events.
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
    ''' <para>10/16/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub SubscribeToEvents()
        Try
            AddHandler m_view.LoadView, AddressOf OnLoadView
            AddHandler m_view.ReloadViewData, AddressOf OnReloadViewData
            AddHandler m_view.View, AddressOf OnView
            AddHandler m_view.Delete, AddressOf OnDelete
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
    ''' <para>10/16/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub OnLoadView(ByVal sender As Object, ByVal e As EventArgs)
        Const ErrorLoadFormData As String = "Error loading form data."
        Try
            LoadViewData()
        Catch exp As Exception
            EventLogger.Enter(exp)
            m_view.ErrorText = ErrorLoadFormData
        End Try
    End Sub

    ''' <summary>
    ''' Reload data for the view - start anew.
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
    ''' <para>10/16/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub OnReloadViewData()
        Const ErrorReloadFormData As String = "Error re-loading form data."
        Try
            ' Flush all properties:
            m_view.MaterialNumber = String.Empty
            m_view.SpeedRating = String.Empty
            m_view.CertId = 0
            m_view.SuccessText = String.Empty
            m_view.ErrorText = String.Empty
            m_view.CertificationName = String.Empty
            m_view.AddCertificationTitle = String.Empty

            m_view.DuplicateCertificates = Nothing
            m_view.DataBindView()

            LoadViewData()
        Catch exp As Exception
            EventLogger.Enter(exp)
            m_view.ErrorText = ErrorReloadFormData
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
    ''' <para>10/16/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub LoadViewData()
        Const TitleText As String = "Material Maint Certificate"
        Try
            m_view.AddCertificationTitle = TitleText
        Catch
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' Loads the data that has been queried on dup correct certification search.
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
    ''' <para>10/16/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>    
    Private Sub OnView()

        m_view.SuccessText = String.Empty
        m_view.ErrorText = String.Empty
        m_view.CertId = 0
        Const MaterialNumberErrorText As String = "Material Number can not be empty."
        Const NoMaterialText As String = "No material exists."
        Const MaterialText As String = "Material "
        Const NotExistText As String = " not exist."

        If String.IsNullOrEmpty(m_view.MaterialNumber) Then
            m_view.ErrorText = MaterialNumberErrorText
            Return
        End If

        Dim dtResults As DataTable
        Dim objCertificationActionModel As New CertificationActionModel
        Try
            dtResults = objCertificationActionModel.GetDuplicateCertificates(m_view.MaterialNumber, _
                                                                             m_view.SpeedRating)

            If dtResults IsNot Nothing AndAlso dtResults.Rows.Count > 0 Then
                m_view.DuplicateCertificates = dtResults
                m_view.DataBindView()
            Else
                m_view.ErrorText = NoMaterialText
            End If

        Catch exc As Exception
            EventLogger.Enter(exc)
            If InStr(exc.Message.ToString(), ORAText) > 0 Then
                m_view.ErrorText = MaterialText & m_view.MaterialNumber & NotExistText
            Else
                m_view.ErrorText = ErrorFetchingText
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Deletes the data that has been selected as duplicate material record from all certificates.
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
    ''' <para>10/16/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>    
    Private Sub OnDelete()
        Dim blnDeleted As Boolean
        Dim objCertificationActionModel As New CertificationActionModel
        Const SuccessDeleteMaterialText As String = "Successfully deleted materials(s)."
        Const FailDeleteMaterialText As String = "Failed to delete materials(s)."
        Const ErrorDeleteMaterialText As String = "Error deleting materials(s)."
        Try
            blnDeleted = objCertificationActionModel.DeleteDuplicateCertificates(m_view.CertId)

            If blnDeleted Then
                m_view.SuccessText = SuccessDeleteMaterialText
                m_view.ErrorText = String.Empty
            Else
                m_view.SuccessText = String.Empty
                m_view.ErrorText = FailDeleteMaterialText
            End If

            RePopulateDupCerts()

        Catch exc As Exception
            EventLogger.Enter(exc)
            m_view.ErrorText = ErrorDeleteMaterialText
        End Try
    End Sub

    ''' <summary>
    ''' Repopulates duplicate certificates.
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
    ''' <para>10/16/2019</para>
    ''' <para>Implemented Coding Standards.</para>
    ''' </description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Private Sub RePopulateDupCerts()

        Dim dtResults As New DataTable
        Dim objCertificationActionModel As New CertificationActionModel
        Try
            m_view.DuplicateCertificates = Nothing
            m_view.DataBindView()

            dtResults = objCertificationActionModel.GetDuplicateCertificates(m_view.MaterialNumber, _
                                                                             m_view.SpeedRating)

        Catch exc As Exception
            EventLogger.Enter(exc)
            If Not InStr(exc.Message.ToString(), ORAText) > 0 Then
                m_view.ErrorText = ErrorFetchingText
            End If
        End Try

        m_view.DuplicateCertificates = dtResults
        m_view.DataBindView()
    End Sub

#End Region

End Class
